using System.Collections;
using UnityEngine;

public class Spt7LvSkill : SptSkillBase
{
    public GameObject castEffect_Prf;
    public GameObject fireEffect_Prf;

    public GameObject projectile_Prf;
    public GameObject hitEffect_Prf;

    public override void OnActive(ETowerType type, float damage, float range)
    {
        skillType = type;
        skillDamage = damage;
        skillRange = range;

        SpawnCastEffect();
    }
    private void SpawnCastEffect()
    {
        var go = Instantiate(castEffect_Prf, transform.position, Quaternion.identity);
        var eff = go.GetComponent<SptSkillEffect>();

        eff.Init(
            onApply: null,              // 캐스트 중간에 특별히 할 일 없으면 null
            onEnd: SpawnFireEffect    // 캐스트 끝나면 SpawnFireEffect 실행
        );
    }

    public void SpawnFireEffect()
    {
        var go = Instantiate(fireEffect_Prf, transform.position, Quaternion.identity);
        var eff = go.GetComponent<SptSkillEffect>();

        eff.Init(
            onApply: Init8Projectile,              // 스킬 시작시 Init8Projectile 생성
            onEnd: null                            // 이펙트 파괴
        );
    }

    public void Init8Projectile()
    {
        for (int i = 0; i < 8; i++)
        {
            Vector2 dir;
            GameObject Ojt;

            if (i == 0) { dir = Vector2.up; }
            else if (i == 1) { dir = new Vector2(1f, 1f).normalized; }
            else if (i == 2) { dir = Vector2.right; }
            else if (i == 3) { dir = new Vector2(1f, -1f).normalized; }
            else if (i == 4) { dir = Vector2.down; }
            else if (i == 5) { dir = new Vector2(-1f, 1f).normalized; }
            else if (i == 6) { dir = Vector2.left; }
            else if (i == 7) { dir = new Vector2(-1f, -1f).normalized; }
            else { dir = Vector2.zero; }

            Ojt = Instantiate(projectile_Prf, transform.position, Quaternion.identity);
            Ojt.GetComponent<SptSkillProjectile>().SetProjectile(dir, skillRange, skillType, skillDamage, hitEffect_Prf);
        }

        Destroy(gameObject);
    }
}
