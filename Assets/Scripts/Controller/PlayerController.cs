using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/*
 * 玩家控制类，修改玩家的分数、段位和金币
 */
public class PlayerController : MonoBehaviour
{
    public GameController gameController; //游戏控制类
    public Text totalScore; //玩家分数显示文字
    public Text totalCoin; //玩家金币数显示文字
    public Text levelText; //玩家段位文字
    
    public int score; //玩家分数
    public int coinNum; //玩家金币数
    public int level; //玩家段位

    void Start()
    {
        score = 3600;
        coinNum = 0;
        ModifyPlayerInfo(0, 0);
    }

    //修改玩家分数、段位和金币显示
    public void ModifyPlayerInfo(int modifyScore, int modifycoinNum)
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        //修改金币数
        coinNum += modifycoinNum;
        totalCoin.text = coinNum.ToString();

        //修改分数，玩家分数超过最大分数，修改为最大分数
        score += modifyScore;
        if (score >= gameController.maxScore)
        {
            score = gameController.maxScore;
        }
        totalScore.text = score.ToString();

        //修改段位，玩家分数小于4000时没有段位
        if (score < gameController.minScore)
        {
            levelText.text = "无段位";
        }
        else
        {
            level = (score - gameController.minScore) / gameController.levelScore + 1;
            stringBuilder.Append("大段位");
            stringBuilder.Append(level);
            levelText.text = stringBuilder.ToString();
        }
    }
}
