using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //����� �ϳ��� ���� �̱��� �Ƚᵵ ��
    //static�� ����Ƽ�� �� ��
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime;

    [Header("# Player Info")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public PoolManager poolManager;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;

    void Awake()
    {
        instance = this;

        maxGameTime = 2f * 10f;
    }

    public void GameStart()
    {
        Resume();
        health = maxHealth;
        uiLevelUp.Select(0);
        isLive = true;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return new WaitForSeconds(1.0f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();

        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();

        Stop();
    }

    public void GameRetry()
    {
        Resume();
        SceneManager.LoadScene(0); // Scene �̸� �־ �� (��Ȯ��)
    }

    void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp()
    {
        if (!isLive)
            return;

        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0; //����Ƽ�� �ð� �ӵ� (����)
    }

    public void Resume()
    {
        isLive= true;
        Time.timeScale = 1; //����Ƽ�� �ð� �ӵ� (����)
    }
}
