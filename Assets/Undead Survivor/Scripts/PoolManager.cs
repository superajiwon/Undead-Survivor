using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // �����յ��� ������ ����
    public GameObject[] prefabs;
    
    // Ǯ ����� �ϴ� ����Ʈ 
    List<GameObject>[] pools;

    void Awake()
    {
        // Ǯ�� ��� ����Ʈ �迭 �ʱ�ȭ
        pools = new List<GameObject>[prefabs.Length];

        // �迭 �� ����Ʈ �ʱ�ȭ
        for (int i = 0; i < pools.Length; i++)
        { 
            pools[i] = new List<GameObject>(); 
        }

        //Debug.Log(pools.Length);
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ������ Ǯ�� ��� (��Ȱ��ȭ ��) �ִ� ���� ������Ʈ ����
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                // �߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);

                break;
            }
        }

        // �� ã����?
        if (!select)
        {
            // ���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}