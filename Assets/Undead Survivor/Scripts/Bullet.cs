using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    //public bool isAlways;

    float tempDamage;
    float lowerDamage;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!gameObject.activeSelf)
            return;

        //if (!isAlways && Vector2.Distance(transform.parent.position, transform.position) > 100f)
        //{
        //    rigid.velocity = Vector2.zero;
        //    gameObject.SetActive(false);
        //}
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;
        //this.isAlways = isAlways;

        tempDamage = this.damage;
        lowerDamage = damage / 3;

        if (per > -1)
        {
            rigid.velocity = dir * 15f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1)
            return;

        per--;
        //관통 시 데미지 줄도록
        //damage -= lowerDamage; 

        if (per == -1 || damage <= 0)
        {
            damage = tempDamage;
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
