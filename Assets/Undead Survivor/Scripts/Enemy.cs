using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;

    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D col;
    Animator anim;
    SpriteRenderer spriter;

    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col= GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        if (!GameManager.instance.isLive)
            return;
        if (!isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        col.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }
    
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            // Live -> hit action
            anim.SetTrigger("Hit");
        }
        else
        {
            // Dead
            isLive = false;
            col.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);

            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        //yield return null; // 1������ ����
        //yield return new WaitForSeconds(2f); // 2�� ����
        yield return wait; // ���� �ϳ��� ���� ������ ������

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); // �������� �� : Impulse
    }

    void Dead()
    {
        // ������Ʈ Ǯ�� �ѰŶ� �ı��ϸ� �ȵ�
        gameObject.SetActive(false);
    }
}
