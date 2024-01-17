using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    //Àå¸éÀÌ ÇÏ³ª¶ó¼­ ±»ÀÌ ½Ì±ÛÅæ ¾È½áµµ µÊ
    //staticÀº À¯´ÏÆ¼»ó¿¡ ¾È ¶ä
    public static GameManager instance;

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime;

    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public PoolManager poolManager;
    public Player player;


    void Awake()
    {
        instance = this;

        maxGameTime = 2f * 10f;
    }

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
