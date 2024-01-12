using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹들을 보관할 변수
    public GameObject[] prefabs;
    
    // 풀 담당을 하는 리스트 
    List<GameObject>[] pools;

    void Awake()
    {
        // 풀을 담는 리스트 배열 초기화
        pools = new List<GameObject>[prefabs.Length];

        // 배열 안 리스트 초기화
        for (int i = 0; i < pools.Length; i++)
        { 
            pools[i] = new List<GameObject>(); 
        }

        //Debug.Log(pools.Length);
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀에 놀고 (비활성화 된) 있는 게임 오브젝트 접근
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                // 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);

                break;
            }
        }

        // 못 찾으면?
        if (!select)
        {
            // 새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
