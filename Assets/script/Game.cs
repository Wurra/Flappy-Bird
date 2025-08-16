using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
    public enum Game_Status
    {
        Ready,
        InGame,
        GameOver
    }
    private Game_Status status;//这行代码的作用是为 Game 类定义了一个私有字段，用于存储游戏的当前状态，类型是枚举 Game_Status。它是游戏状态管理的基础，后续可以通过方法或逻辑来修改和读取这个字段的值。
    public GameObject panelReady;
    public GameObject panelInGame;
    public GameObject panelGameOver;
    public PipelineManager pipelineManager; // 添加一个 PipelineManager 的引用，用于管理管道生成
    public Player player; // 添加一个 Player 的引用，用于控制玩家行为
    public int score; // 添加一个整数 score，用于记录玩家得分
    public TMP_Text scoreText; // 添加一个 Text 组件 scoreText，用于在 UI 上显示得分,我服了，要创建这个TMP_Text
    public TMP_Text scoreText2;

    public int Score
    {
        get { return score; } // 这是一个属性，用于获取当前得分
        set
        {
            this.score = value; // 设置得分
            this.scoreText.text =this.score.ToString(); // 更新 UI 上的得分显示
            this.scoreText2.text = this.score.ToString(); // 更新 UI 上的得分显示
        }
    }

    void Start()
    {
        this.panelReady.SetActive(true);
        this.status = Game_Status.Ready; // 设置游戏状态为 Ready  
        UpdateUi();
        this.player.onDeath += Player_OnDeath; // 创建一个Player_OnDeath方法订阅玩家死亡事件，当玩家死亡时调用 GameOver 方法
        this.player.OnScore = OnPlayerScore; // 订阅玩家得分事件，当玩家得分时调用 OnPlayerScore 方法
    }
    void OnPlayerScore(int score)
    {
      this.Score += score; // 当玩家得分时，更新总得分
    }

    public void StartGame()
    {
        this.status = Game_Status.InGame; // 设置游戏状态为 InGame  
        UpdateUi(); // 调用 UpdateUi 方法来更新 UI 面板的显示  
        pipelineManager.StartRun(); // 启动管道生成  
        player.Fly(); // 设置小鸟的动画为 fly    
    }

    private void Player_OnDeath()
    {
        this.status = Game_Status.GameOver; // 设置游戏状态为 GameOver
        UpdateUi(); // 更新 UI 面板显示
        this.pipelineManager.Stop(); // 停止管道生成
        
    }

    public void UpdateUi()
    {
        // 根据当前游戏状态更新 UI 面板的显示
        this.panelReady.SetActive(this.status == Game_Status.Ready);
        this.panelInGame.SetActive(this.status == Game_Status.InGame);
        this.panelGameOver.SetActive(this.status == Game_Status.GameOver);
    }
    public void Restart()
    {
        this.status = Game_Status.Ready; // 重置游戏状态为 Ready  
        this.UpdateUi(); // 更新 UI 面板显示
        this.pipelineManager.Init(); // 初始化管道管理器，清除之前的管道
        this.player.Init(); // 初始化玩家位置和状态
        this.Score = 0; // 重置得分为 0
    }
}
