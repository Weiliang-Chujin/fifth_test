using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 奖励预制体类，保存奖励预制体的各子物体
 */
public class RewardPrefab : MonoBehaviour
{
    public GameObject rewardobj; //奖励预制体
    public Text scoreText; //分数，领取这个奖励所要的分数
    public Text rewardText; //奖励文字，第几个奖励或者第几个大段
    public Button receiveButton; //领取按钮
    public GameObject receiveButtonObj; //领取按钮对象
    public GameObject receiveTextObj; //已领取文字对象
    public GameObject maskPanel; //玩家没达到该奖励分数的遮盖容器

}
