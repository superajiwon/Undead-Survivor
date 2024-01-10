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

        // ����
        Vector2 dirVec = target.position - rigid.position;
        // ���� ��ġ
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        // �����̱� (�÷��̾��� Ű �Է°��� ���� �̵� = ������ ���� ���� ���� �̵�)
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; // �з����� �ʵ���
    }

    void LateUpdate()
    {
        if (!isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x;
    }
}
