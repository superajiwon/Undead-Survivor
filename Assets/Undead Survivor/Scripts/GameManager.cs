using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //장면이 하나라서 굳이 싱글톤 안써도 됨
    //static은 유니티상에 안 뜸
    public static GameManager instance;
    public Player player;

    private void Awake()
    {
        instance = this;
    }

}
