using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public int count;
    public float damage;
    public float speed;

    float timer;

    Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                // ������ �ؾ� �ð�������� �� (Vector3.forward * -speed / Vector3.back * speed)
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            default:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        // test
        if (Input.GetButtonDown("Jump"))
            LevelUp(10, 1); 
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Collocate();
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;

                Collocate();

                break;

            default:
                speed = .3f;

                break;
        }
    }

    void Collocate()
    {
        // ī��Ʈ ���� ������ �����
        for (int i = 0; i < count; i++)
        {   // Transform �� ������ �θ� �ٲٱ� ����
            Transform bullet;

            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.poolManager.Get(prefabId).transform;
                bullet.parent = transform;
            }

            //��ġ �ʱ�ȭ
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec); //���� ���� ����
            bullet.Translate(bullet.up * 1.5f, Space.World); //���� �������� �ؾ���

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 is Infinity Per.
        }
    }

    void Fire()
    {
        // ���� ���� �ִ��� Ȯ�� ����
        if (!player.scanner.nearestTarget)
            return;

        // �Ѿ� ���� ����
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        // ��ġ, ȸ��Ȯ�� �� ����
        Transform bullet = GameManager.instance.poolManager.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
