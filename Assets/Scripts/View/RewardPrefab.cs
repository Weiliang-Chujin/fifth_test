using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/*
 * 奖励预制体类，保存奖励预制体的各子物体,修改奖励列表显示的信息
 */
public class RewardPrefab : MonoBehaviour
{
    public GameObject rewardobj; //奖励预制体
    public Text scoreText; //分数，领取这个奖励所要的分数
    public Text rewardText; //奖励文字，第几个奖励或者第几个大段
    public Button receiveButton; //领取按钮
    public GameObject receiveButtonObj; //领取按钮对象
    public GameObject receiveTextObj; //已领取文字对象
    public GameObject maskPanel; //玩家没达到该奖励分数的遮盖容器，显示未达到

    public int rewardscore; //该奖励所需分数
    public int rewardCoin; //奖励金币数
    public int level; //该奖励列表处显示的大段位
    public bool isReward; //判断是否是奖励
    public int maskState; //是否开放奖励，0开放，1不开放
    public int receiveState; //领取状态，0为未领取，1为已领取,2为大段位，不设置领取奖励

    //修改显示的该奖励所需分数
    public void ModifyScoreText()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("所需分数:");
        stringBuilder.Append(rewardscore);
        scoreText.text = stringBuilder.ToString();
        stringBuilder.Clear();
    }
    
    //如果是奖励，给出奖励金币数文字，如果不是，改为显示该处段位文字
    public void ModiyRewardText()
    {
        StringBuilder stringBuilder = new StringBuilder();
        if (isReward)
        {
            stringBuilder.Append("奖励:");
            stringBuilder.Append(rewardCoin);
            stringBuilder.Append("金币");
            rewardText.text = stringBuilder.ToString();
            stringBuilder.Clear();
        }
        else
        {
            stringBuilder.Append("大段位");
            stringBuilder.Append(level);
            rewardText.text = stringBuilder.ToString();
            stringBuilder.Clear();
        }
    }
    
    //修改领取奖励状态
    public void ModifyRewardState()
    {
        if (receiveState == 0)
        {
            receiveButtonObj.SetActive(true);
            receiveTextObj.SetActive(false);
        }
        else if (receiveState == 1)
        {
            receiveButtonObj.SetActive(false);
            receiveTextObj.SetActive(true);
        }
        else
        {
            receiveButtonObj.SetActive(false);
            receiveTextObj.SetActive(false);
        }
    }

    //如果玩家分数大于等于该奖励所需分数，则开放奖励，将奖励上的遮罩去除,否则关闭奖励，加上奖励遮罩
    public void OperateRewardMask()
    {
        if (maskState == 0)
        {
            maskPanel.SetActive(false);
        }
        else
        {
            maskPanel.SetActive(true);
        }
    }

}
