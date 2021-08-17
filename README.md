### 1.整体框架  

 玩家初始分数设为3600，滚动视图坐标为4000分处   
 4000分以下，从0-4000不断上涨，赛季刷新时不会掉分，且没有奖励   
 点击领取时，玩家4000分以上时，每200分可领取一次奖励，奖励处变为已领取奖励，玩家金币数加100奖励金币，但整千时不能领取奖励，且每1000分就达到一个大段位，上限为6000分   
 点击刷新赛季，赛季数加一，玩家4000分以上时，4000分以上分数会被砍掉一半，新的赛季可重新爬取段位，奖励重新打开，可重新领取，玩家金币数不变。    
 点击加分时，根据当前分数大于该奖励分段分数，将该奖励打开，玩家可领取该奖励   
 点击查看时，滚动视图定位到玩家目前分数达到最高的奖励段的坐标处，玩家分数和段位数在界面已显示。 

### 2.界面结构     

 SampleScene：包括ControlEvent、ScrollView、BottomPanel。    

 ControlEvent：空物体，用来绑定脚本。  
 ScrollView：content中生成奖励列表  
 BottomPanel：TotalScore（玩家分数）、LevelText（玩家段位）、TotalCoin（玩家总金币数）、SeasonText（赛季数）、LookButton（查看按钮）、AddButton（加分按钮）、RefreshButton（刷新按钮）。

 预制体只有1个，RewardPanel（一个奖励item）包括ScoreText（所需分数）、RewardText（奖励金币数）、ReceiveText（已领取文字）、ReceiveButton（领取按钮）、MaskPanel（遮罩，显示未到达）
			    
### 3.代码结构

| 类名             | 功能                                                       | 调用关系                                         |
| ---------------- | ---------------------------------------------------------- | ------------------------------------------------ |
| PlayerData       | 管理玩家的分数、段位和金币                                 | 被PlayerController类调用                         |
| RewardPrefab     | 保存奖励预制体的各子物体                                   | 被RewardController类调用                         |
| PlayerController | 加分、查看、刷新按钮的操作                                 | 被RewardController类调用，调用RewardController类 |
| RewardController | 生成奖励列表，修改奖励状态，开放奖励和点击按钮领取奖励操作 | 被PlayerController类调用，调用PlayerController类 |


### 4.流程图

![flowPath](https://github.com/89trillion-hehuan/fifth_test/blob/main/FlowChart.png)
