using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/*
 * 奖励控制类，生成奖励列表，修改奖励状态，开放奖励和点击按钮领取奖励操作
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

            //rewardscore为该奖励需要的分数
            int rewardscore = gameController.minScore + i * gameController.segmentScore;
            stringBuilder.Append("所需分数:");
            stringBuilder.Append(rewardscore);
            rewardObject.scoreText.text = stringBuilder.ToString();
            stringBuilder.Clear();
            
            if (rewardscore % gameController.levelScore != 0)
            {
                stringBuilder.Append("奖励:");
                stringBuilder.Append(gameController.rewardCoin);
                stringBuilder.Append("金币");
                rewardObject.rewardText.text = stringBuilder.ToString();
                stringBuilder.Clear();
                
                ModifyRewardState(0, rewardObject);
                rewardObject.receiveButton.onClick.AddListener(()=> { ReceiveReward(rewardObject); });
            }
            //1000分设置大段位
            else 
            {
                int level = (rewardscore - gameController.minScore) / gameController.levelScore + 1;
                stringBuilder.Append("大段位");
                stringBuilder.Append(level);
                rewardObject.rewardText.text = stringBuilder.ToString();
                stringBuilder.Clear();
                
                rewardObject.receiveButtonObj.SetActive(false);
                rewardObject.receiveTextObj.SetActive(false);
            }
            
            OperateRewardMask(rewardObject, playerSocre, rewardscore);
            rewardObjects.Add(rewardObject);
        }
    }
    
    //修改领取奖励状态，0为未领取，1为已领取
    public void ModifyRewardState(int state, RewardPrefab rewardobj)
    {
        if (state == 0)
        {
            rewardobj.receiveButtonObj.SetActive(true);
            rewardobj.receiveTextObj.SetActive(false);
        }
        else
        {
            rewardobj.receiveButtonObj.SetActive(false);
            rewardobj.receiveTextObj.SetActive(true);
        }
    }
    
    //如果玩家分数大于等于该奖励所需分数，则开放奖励，将奖励上的遮罩去除,否则关闭奖励，加上奖励遮罩
    public void OperateRewardMask(RewardPrefab rewardobj, int playerSocre, int rewardscore)
    {
        if (playerSocre >= rewardscore)
        {
            rewardobj.maskPanel.SetActive(false);
        }
        else
        {
            rewardobj.maskPanel.SetActive(true);
        }
    }
    
    //点击领取奖励按钮领取奖励
    private void ReceiveReward(RewardPrefab rewardobj)
    {
        ModifyRewardState(1, rewardobj);
        playerController.ModifyPlayerInfo(0, gameController.rewardCoin);
    }
}
