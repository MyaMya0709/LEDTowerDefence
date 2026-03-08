using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Spt8LvSkill : SptSkillBase
{
    public GameObject castEffect_Prf;
    public GameObject mainEffect_Prf;
    public GameObject hitEffect_Prf;

    public GameObject projectile_Prf;
    public GameObject damageBox_Prf;



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
            onEnd: SpawnFireEffect     // 캐스트 끝나면 MainEffect 생성
        );
    }

    private void SpawnFireEffect()
    {
        var go = Instantiate(mainEffect_Prf, transform.position, Quaternion.identity);
        var eff = go.GetComponent<SptSkillEffect>();

        eff.Init(
            onApply: InitProjectile,   // 발사 애니의 프레임에서 투사체 생성
            onEnd: null                // 이펙트 파괴
        );
    }

    public void InitProjectile()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector2 dir;
            Vector2 initPos;
            GameObject Ojt;
            float angle;

            if (i == 0)
            {
                dir = Vector2.up;
                initPos = new Vector2() {x = -3, y = -4 };
                angle = 0;
            }
            else if (i == 1)
            {
                dir = Vector2.right;
                initPos = new Vector2() { x = -4, y = 3 };
                angle = -90;
            }
            else if (i == 2)
            {
                dir = Vector2.down;
                initPos = new Vector2() { x = 3, y = 4 };
                angle = 180;
            }
            else if (i == 3)
            {
                dir = Vector2.left;
                initPos = new Vector2() { x = 4, y = -3 };
                angle = 90;
            }
            else { dir = Vector2.zero; initPos = Vector2.zero; angle = 0; }

            Ojt = Instantiate(projectile_Prf, initPos, Quaternion.Euler(0,0, angle));
            Ojt.GetComponent<SptSkillProjectile>().SetProjectile(dir, 8f, skillType, skillDamage, hitEffect_Prf);
        }

        Destroy(gameObject);
    }
}
