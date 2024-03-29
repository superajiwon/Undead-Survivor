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
    public RuntimeAnimatorController[] animCon;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator animator;

    void Awake() // 초기화작업
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true); // 비활성화 되어있어도 읽어라
    }

    void OnEnable()
    {
        speed *= Character.Speed;
        animator.runtimeAnimatorController = animCon[GameManager.instance.playerID];
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        // 기본 움직임
        // 그냥 GetAxis는 자연스럽게 값을 변경
        // 정확한 타이밍에 움직여야한다면 GetAxisRaw
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    } 

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        // 1. 힘을 준다 
        //rigid.AddForce(inputVec);

        // 2. 속도 제어
        //rigid.velocity = inputVec;

        // 3. 위치 이동 : 월드 좌표에서 이동해야 하니까
        //    리지드에 normalized * speed * 고정된 프레임 한 방향을 더해서 위치 이동
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        // Input Value 로 만들 때는 설정에서 미리 Normalizing 할 수 있어서 뺌
        // Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime; 
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate()
    {
        // magnitude : 벡터의 크기 값 가져옴
        animator.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0; // 값이 true false 로 나오니까
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
