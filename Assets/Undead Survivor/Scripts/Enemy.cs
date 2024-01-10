using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        
        isLive = true;
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;

        // 방향
        Vector2 dirVec = target.position - rigid.position;
        // 다음 위치
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        // 움직이기 (플레이어의 키 입력값을 더한 이동 = 몬스터의 방향 값을 더한 이동)
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; // 밀려나지 않도록
    }

    void LateUpdate()
    {
        if (!isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x;
    }
}
