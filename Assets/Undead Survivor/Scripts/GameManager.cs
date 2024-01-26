using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //장면이 하나라서 굳이 싱글톤 안써도 됨
    //static은 유니티상에 안 뜸
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime;

    [Header("# Player Info")]
    public int playerID;
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

    public void GameStart(int id)
    {
        playerID = id;
        health = maxHealth;

        player.gameObject.SetActive(true);

        uiLevelUp.Select(playerID % 2); // 기본 무기 지급을 위한 함수 호출에서 인자값을 캐릭터 ID로 변경

        Resume();
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
        SceneManager.LoadScene(0); // Scene 이름 넣어도 됨 (정확히)
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
        Time.timeScale = 0; //유니티의 시간 속도 (배율)
    }

    public void Resume()
    {
        isLive= true;
        Time.timeScale = 1; //유니티의 시간 속도 (배율)
    }
}
