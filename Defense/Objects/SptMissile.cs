using System.Collections;
using UnityEngine;

public class SptMissile : MonoBehaviour
{
    public Transform target;
    public Vector2 tarPos;
    public Vector2 nextPos;
    public float damage;
    public ETowerType damageType;
    public float speed;
    public LayerMask layer;

    public bool isMultiple;
    public bool isHit = false;
    public bool isMove = false;

    public GameObject hitEffect;

    private void Update()
    {
        if (isHit || !isMove) return;

        // 방향 찾기
        Vector2 dir = (tarPos - (Vector2)transform.position).normalized;
        float disToTar = Vector2.Distance((Vector2)(transform.position), tarPos);

        //transform.position += (Vector3)(dir * speed * Time.deltaTime);

        // 다음 위치가 타겟을 넘는지 체크
        if (disToTar < speed * Time.deltaTime)
        {
            // 타겟을 넘어가면 타겟 위치로 이동
            transform.position = tarPos;
            OnHit();
        }
        else
        {
            // 계속 이동
            transform.position += (Vector3)(dir * speed * Time.deltaTime);
        }
        // 회전 방향 조정
        transform.up = dir;
    }

    public void OnHit()
    {
        //Debug.Log("목표 적중!");
        //Debug.Log($"{damageType}");
        isHit = true;

        if (isMultiple)
        {
            // 범위 탐색
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(tarPos, 2f, layer);

            // 탐색으로 나온 개체들의 거리 비교
            foreach (Collider2D collider in hitColliders)
            {
                //데미지 함수 호출
                collider.GetComponent<SptEnemy>().TakeDamage(damage, damageType);
            }
        }
        else
        {
            if (target != null)
            {
                //Debug.Log("데미지 호출!");
                target.GetComponent<SptEnemy>().TakeDamage(damage, damageType);
            }
        }

        // 폭발 이펙트 호출
        GameObject effectOjt = Instantiate(hitEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
