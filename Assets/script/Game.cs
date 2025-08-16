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
    private Game_Status status;//���д����������Ϊ Game �ඨ����һ��˽���ֶΣ����ڴ洢��Ϸ�ĵ�ǰ״̬��������ö�� Game_Status��������Ϸ״̬����Ļ�������������ͨ���������߼����޸ĺͶ�ȡ����ֶε�ֵ��
    public GameObject panelReady;
    public GameObject panelInGame;
    public GameObject panelGameOver;
    public PipelineManager pipelineManager; // ���һ�� PipelineManager �����ã����ڹ���ܵ�����
    public Player player; // ���һ�� Player �����ã����ڿ��������Ϊ
    public int score; // ���һ������ score�����ڼ�¼��ҵ÷�
    public TMP_Text scoreText; // ���һ�� Text ��� scoreText�������� UI ����ʾ�÷�,�ҷ��ˣ�Ҫ�������TMP_Text
    public TMP_Text scoreText2;

    public int Score
    {
        get { return score; } // ����һ�����ԣ����ڻ�ȡ��ǰ�÷�
        set
        {
            this.score = value; // ���õ÷�
            this.scoreText.text =this.score.ToString(); // ���� UI �ϵĵ÷���ʾ
            this.scoreText2.text = this.score.ToString(); // ���� UI �ϵĵ÷���ʾ
        }
    }

    void Start()
    {
        this.panelReady.SetActive(true);
        this.status = Game_Status.Ready; // ������Ϸ״̬Ϊ Ready  
        UpdateUi();
        this.player.onDeath += Player_OnDeath; // ����һ��Player_OnDeath����������������¼������������ʱ���� GameOver ����
        this.player.OnScore = OnPlayerScore; // ������ҵ÷��¼�������ҵ÷�ʱ���� OnPlayerScore ����
    }
    void OnPlayerScore(int score)
    {
      this.Score += score; // ����ҵ÷�ʱ�������ܵ÷�
    }

    public void StartGame()
    {
        this.status = Game_Status.InGame; // ������Ϸ״̬Ϊ InGame  
        UpdateUi(); // ���� UpdateUi ���������� UI ������ʾ  
        pipelineManager.StartRun(); // �����ܵ�����  
        player.Fly(); // ����С��Ķ���Ϊ fly    
    }

    private void Player_OnDeath()
    {
        this.status = Game_Status.GameOver; // ������Ϸ״̬Ϊ GameOver
        UpdateUi(); // ���� UI �����ʾ
        this.pipelineManager.Stop(); // ֹͣ�ܵ�����
        
    }

    public void UpdateUi()
    {
        // ���ݵ�ǰ��Ϸ״̬���� UI ������ʾ
        this.panelReady.SetActive(this.status == Game_Status.Ready);
        this.panelInGame.SetActive(this.status == Game_Status.InGame);
        this.panelGameOver.SetActive(this.status == Game_Status.GameOver);
    }
    public void Restart()
    {
        this.status = Game_Status.Ready; // ������Ϸ״̬Ϊ Ready  
        this.UpdateUi(); // ���� UI �����ʾ
        this.pipelineManager.Init(); // ��ʼ���ܵ������������֮ǰ�Ĺܵ�
        this.player.Init(); // ��ʼ�����λ�ú�״̬
        this.Score = 0; // ���õ÷�Ϊ 0
    }
}
