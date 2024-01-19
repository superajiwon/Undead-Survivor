using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer playerSpriter;

    Vector3 rightPos = new Vector3(.35f, -.15f, 0f);
    Vector3 rightPosReverse = new Vector3(-.35f, -.15f, 0f);
    Quaternion leftRot = Quaternion.Euler(0f, 0f, -35f);
    Quaternion leftRotReverse = Quaternion.Euler(0f, 0f, -135f);

    void Awake()
    {
        playerSpriter = GetComponentsInParent<SpriteRenderer>()[1];
    }

    void LateUpdate()
    {
        bool isReverse = playerSpriter.flipX;

        if (isLeft) 
        {   // 근접 무기
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else
        {   // 원거리 무기
            transform.localPosition = isReverse ? rightPosReverse : rightPos; 
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
