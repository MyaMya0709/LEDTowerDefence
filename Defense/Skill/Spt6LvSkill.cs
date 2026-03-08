using System.Collections;
using UnityEngine;

public class Spt6LvSkill : SptSkillBase
{
    public GameObject castEffect_Prf;
    public GameObject fireEffect_Prf;
    public GameObject projectile_Prf;
    public GameObject hitEffect_Prf;

    public float interval;
    public bool isWay = true;

    public override void OnActive(ETowerType type, float damage, float range)
    {
        skillType = type;
        skillDamage = damage;
        skillRange = range;

        SpawnCastEffect();
    }

    public void OnSkill()
    {
        StartCoroutine(Skill());
    }

    public IEnumerator Skill()
    {
        SpawnFireEffect();
        yield return new WaitForSeconds(interval);
        SpawnFireEffect();
    }

    private void SpawnCastEffect()
    {
        var go = Instantiate(castEffect_Prf, transform.position, Quaternion.identity);
        var eff = go.GetComponent<SptSkillEffect>();

        eff.Init(
            onApply: null,              // 캐스트 중간에 특별히 할 일 없으면 null
            onEnd: OnSkill              // 캐스트 끝나면 OnSkill 실행
        );
    }

    public void SpawnFireEffect()
    {
        var go = Instantiate(fireEffect_Prf, transform.position, Quaternion.identity);
        var eff = go.GetComponent<SptSkillEffect>();

        Debug.Log("Lv6Fire");

        eff.Init(
            onApply: Init4Projectile,              // 스킬 시작시 Init4Projectile 생성
            onEnd: null                            // 끝나고 로직 없으면 null (이펙트는 삭제됨)
        );

        if(!isWay) Destroy(gameObject);
    }

    public void Init4Projectile()
    {
        Vector2 way1;
        Vector2 way2;
        Vector2 way3;
        Vector2 way4;

        if (isWay)
        {
            way1 = Vector2.up;
            way2 = Vector2.right;
            way3 = Vector2.down;
            way4 = Vector2.left;
        }
        else
        {
            way1 = new Vector2 (1f,1f).normalized;
            way2 = new Vector2 (1f,-1f).normalized;
            way3 = new Vector2 (-1f,1f).normalized;
            way4 = new Vector2 (-1f,-1f).normalized;
        }

        GameObject pjt1 = Instantiate(projectile_Prf, transform.position, Quaternion.identity);
        GameObject pjt2 = Instantiate(projectile_Prf, transform.position, Quaternion.identity);
        GameObject pjt3 = Instantiate(projectile_Prf, transform.position, Quaternion.identity);
        GameObject pjt4 = Instantiate(projectile_Prf, transform.position, Quaternion.identity);

        pjt1.GetComponent<SptSkillProjectile>().SetProjectile(way1, skillRange, skillType, skillDamage, hitEffect_Prf);
        pjt2.GetComponent<SptSkillProjectile>().SetProjectile(way2, skillRange, skillType, skillDamage, hitEffect_Prf);
        pjt3.GetComponent<SptSkillProjectile>().SetProjectile(way3, skillRange, skillType, skillDamage, hitEffect_Prf);
        pjt4.GetComponent<SptSkillProjectile>().SetProjectile(way4, skillRange, skillType, skillDamage, hitEffect_Prf);

        isWay = !isWay;
    }
}
