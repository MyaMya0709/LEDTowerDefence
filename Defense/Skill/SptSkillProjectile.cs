using UnityEngine;

public class SptSkillProjectile : MonoBehaviour
{
    public GameObject damageBox_Prf;
    public float moveSpeed;
    public float damage;
    public ETowerType damageType;

    public Vector2 moveDirection;
    public float moveDistance;
    public Vector2 tarPos;

    public GameObject hitEffect_Prf;
    public bool isStart= false;

    private void Update()
    {
        if (!isStart) return;

        float disToTar = Vector2.Distance((Vector2)(transform.position), tarPos);

        // 다음 위치가 타겟을 넘는지 체크
        if (disToTar < moveSpeed * Time.deltaTime)
        {
            // 타겟을 넘어가면 타겟 위치로 이동
            transform.position = tarPos;
            // 파괴
            Destroy(gameObject);
        }
        else
        {
            // 계속 이동
            transform.position += (Vector3)(moveDirection * moveSpeed * Time.deltaTime);
        }
        // 회전 방향 조정
        transform.up = moveDirection;
    }

    public void SetProjectile(Vector2 dir, float dis, ETowerType type, float damage, GameObject hitEft_Prf)
    {
        transform.up = dir;

        moveDirection = dir;
        moveDistance = dis;

        hitEffect_Prf = hitEft_Prf;

        tarPos = transform.position + (Vector3)(dir * dis);

        SptDamageBox dmgBox = damageBox_Prf.GetComponent<SptDamageBox>();
        dmgBox.SetDamageBox(type, damage, new Vector2(0.1f, 0.2f), hitEffect_Prf);

        isStart = true;
    }
}
