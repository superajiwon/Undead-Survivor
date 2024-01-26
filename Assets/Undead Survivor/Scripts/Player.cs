using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator animator;

    void Awake() // �ʱ�ȭ�۾�
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true); // ��Ȱ��ȭ �Ǿ��־ �о��
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        // �⺻ ������
        // �׳� GetAxis�� �ڿ������� ���� ����
        // ��Ȯ�� Ÿ�ֿ̹� ���������Ѵٸ� GetAxisRaw
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    } 

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        // 1. ���� �ش� 
        //rigid.AddForce(inputVec);

        // 2. �ӵ� ����
        //rigid.velocity = inputVec;

        // 3. ��ġ �̵� : ���� ��ǥ���� �̵��ؾ� �ϴϱ�
        //    �����忡 normalized * speed * ������ ������ �� ������ ���ؼ� ��ġ �̵�
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        // Input Value �� ���� ���� �������� �̸� Normalizing �� �� �־ ��
        // Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime; 
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate()
    {
        // magnitude : ������ ũ�� �� ������
        animator.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0; // ���� true false �� �����ϱ�
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        GameManager.instance.health -= Time.deltaTime * 10;

        if (GameManager.instance.health <= 0)
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }

    }

    //void OnMove(InputValue value)
    //{
    //    inputVec = value.Get<Vector2>();
    //}
}
