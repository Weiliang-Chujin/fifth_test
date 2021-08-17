using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/*
 * 游戏控制类，包括加分、查看、刷新按钮的操作
 */
public class GameController : MonoBehaviour
{
    public GameObject content; //滚动视图的content对象
    public Button addButton; //加分按钮
    public Button lookButton; //查看按钮
    public Button refreshButton; //刷新按钮
    public Text seasonText; //赛季文字
    
    public RewardController rewardController; //奖励控制类
    public PlayerController playerController; //玩家控制类
    
    public int minScore; //领取奖励最低分
    public int maxScore; //领取奖励最高分
    public int addScore; //每次增加的分数
    public int levelScore; //一个大段分数
    public int segmentScore; //领取奖励的分段分数
    public int rewardCoin; //每次奖励金币
    public int rewardCount; //奖励列表个数
    public int rewardStep; //一个大段中奖励的个数
    
    private float height; //每个奖励的高度
    private int seasonCount; //赛季个数
    StringBuilder stringBuilder; //用于连接字符串
    
    
    void Start()
    {
        minScore = 4000;
        maxScore = 6000;
        segmentScore = 200;
        rewardCount = (maxScore - minScore) / segmentScore + 1;
        addScore = 100;
        levelScore = 1000;
        rewardCoin = 100;
        rewardStep = levelScore / segmentScore;
        height = 200;
        seasonCount = 1;
        
        stringBuilder = new StringBuilder();
        stringBuilder.Append("第");
        stringBuilder.Append(seasonCount);
        stringBuilder.Append("赛季");
        seasonText.text = stringBuilder.ToString();
        stringBuilder.Clear();
        
        addButton.onClick.AddListener(AddScore);
        lookButton.onClick.AddListener(Look);
        refreshButton.onClick.AddListener(Refresh);
    }

    //增加分数函数,每次增加100分,分数上限6000,增加一次分数，判断分数是否能开启下一个奖励，改变该奖励的遮罩
    private void AddScore()
    {
        int index; //查找在奖励列表的索引
        
        playerController.ModifyPlayerInfo(addScore, 0);
        if (playerController.score >= minScore && playerController.score <= maxScore)
        {
            index = (playerController.score - minScore) / segmentScore;
            rewardController.OperateRewardMask(rewardController.rewardObjects[index], 
                playerController.score, minScore + index * segmentScore);
        }
    }
    
    //查看当前段位情况函数,滚动视图自动滚动到当前达到的最高分数段位置
    private void Look()
    {
        int index; //查找在奖励列表的索引
        
        if (playerController.score >= minScore)
        {
            index = (playerController.score - minScore) / segmentScore;
            
            //调整视窗坐标
            content.transform.localPosition = new Vector3(0, - height * index, 0);
        }
    }
    
    //刷新到下一赛季函数
    private void Refresh()
    {
        int nextScore; //下赛季分数
        
        //修改赛季显示
        seasonCount++;
        stringBuilder.Append("第");
        stringBuilder.Append(seasonCount);
        stringBuilder.Append("赛季");
        seasonText.text = stringBuilder.ToString();
        stringBuilder.Clear();
        
        if (playerController.score >= minScore)
        {
            //根据上赛季分数，低于4000的不做变换，超过4000的，将超过4000的部分砍掉一半，修改玩家分数
            nextScore = (playerController.score - minScore) / 2 + minScore;
            playerController.ModifyPlayerInfo(nextScore - playerController.score, 0);
            
            //将奖励全部刷新为可领取，根据新赛季分数打开所有奖励的遮罩
            for (int i = 0; i < rewardCount; i++)
            {
                if (i % rewardStep != 0)
                {
                    rewardController.ModifyRewardState(0, rewardController.rewardObjects[i]);
                }
                rewardController.OperateRewardMask(rewardController.rewardObjects[i], 
                    nextScore, minScore + i * segmentScore);
            }
        }
    }
}
