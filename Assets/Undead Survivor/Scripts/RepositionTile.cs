using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionTile : MonoBehaviour
{
    Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
	{
		if (!collision.CompareTag("Area"))
			return;

		Vector3 playerPos = GameManager.instance.player.transform.position;
		Vector3 playerDir = GameManager.instance.player.inputVec;
		Vector3 myPos = transform.position;

        //���� ����Ż
		/*
        //������ ��������ؼ� ���밪���� ����
		float diffX = Mathf.Abs(playerPos.x - myPos.x);
		float diffY = Mathf.Abs(playerPos.y - myPos.y);

		//Player Input System
		float dirX = playerDir.x < 0 ? -1 : 1;
		float dirY = playerDir.y < 0 ? -1 : 1;
        */

        //������ �ڵ�
        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        float diffX = Mathf.Abs(dirX);
        float diffY = Mathf.Abs(dirY);

        dirX = dirX > 0 ? 1 : -1;
        dirY = dirY > 0 ? 1 : -1;

		switch (transform.tag)
		{
			case "Ground":

                if (Mathf.Abs(diffX - diffY) <= 0.1f)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                    transform.Translate(Vector3.up * dirY * 40);
                }
				else if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }

                break;

			case "Enemy":
                // �÷��̾�� �־����� �̵��� �� �ֵ���

                if (col.enabled)
                {
                    transform.Translate(playerDir * 20 + 
                        new Vector3(Random.Range(-3f, 3f), Random.Range(-3, 3f), 0f));
                }

				break;
		}
	}
}
