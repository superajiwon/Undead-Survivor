using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //����� �ϳ��� ���� �̱��� �Ƚᵵ ��
    //static�� ����Ƽ�� �� ��
    public static GameManager instance;
    public Player player;

    private void Awake()
    {
        instance = this;
    }

}
