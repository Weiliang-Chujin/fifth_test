using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/*
 * 奖励控制类，生成奖励列表和点击按钮领取奖励操作
 */
public class RewardController : MonoBehaviour
{
    public GameObject content; //滚动视图的content，奖励列表的父对象
    public RewardPrefab rewardPrefab; //奖励的预制体
    public PlayerController playerController; //玩家控制类
    public GameController gameController; //游戏控制类

    public List<RewardPrefab> rewardObjects = new List<RewardPrefab>(); //存入所有生成的奖励对象的list

    void Start()
    {
        content.transform.localPosition = new Vector3(0, 0, 0);
        CreateReward(playerController.score);
    }

    //根据玩家分数生成奖励列表
    private void CreateReward(int playerSocre)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < gameController.rewardCount; i++)
        {
            
            RewardPrefab rewardObject = Instantiate(rewardPrefab, content.transform);

            //修改该奖励所需分数
            rewardObject.rewardscore = gameController.minScore + i * gameController.segmentScore;
            rewardObject.ModifyScoreText();
            
            //显示奖励金币文字
            if (rewardObject.rewardscore % gameController.levelScore != 0)
            {
                rewardObject.rewardCoin = 100;
                rewardObject.isReward = true;
                rewardObject.ModiyRewardText();
                
                //修改领取状态为可领取奖励
                rewardObject.receiveState = 0;
                rewardObject.ModifyRewardState();
                rewardObject.receiveButton.onClick.AddListener(()=> { ReceiveReward(rewardObject); });
            }
            //1000分设置大段位，显示段位文字
            else 
            {
                rewardObject.level = (rewardObject.rewardscore - gameController.minScore) / gameController.levelScore + 1;
                rewardObject.isReward = false;
                rewardObject.ModiyRewardText();
                
                //隐藏奖励按钮和奖励文字
                rewardObject.receiveState = 2;
                rewardObject.ModifyRewardState();
            }
            
            //根据玩家分数修改奖励遮罩
            if (playerSocre >= rewardObject.rewardscore)
            {
                rewardObject.maskState = 0;
            }
            else
            {
                rewardObject.maskState = 1;
            }
            rewardObject.OperateRewardMask();
            
            rewardObjects.Add(rewardObject);
        }
    }
    
    //点击领取奖励按钮领取奖励
    private void ReceiveReward(RewardPrefab rewardobj)
    {
        rewardobj.receiveState = 1;
        rewardobj.ModifyRewardState();
        playerController.ModifyPlayerInfo(0, rewardobj.rewardCoin);
    }
}
