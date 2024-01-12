using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    //Àå¸éÀÌ ÇÏ³ª¶ó¼­ ±»ÀÌ ½Ì±ÛÅæ ¾È½áµµ µÊ
    //staticÀº À¯´ÏÆ¼»ó¿¡ ¾È ¶ä
    public static GameManager instance;

    public float gameTime;
    public float maxGameTime;

    public PoolManager poolManager;

    public Player player;


    void Awake()
    {
        instance = this;

        gameTime = 0;
        maxGameTime = 2f * 10f;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
