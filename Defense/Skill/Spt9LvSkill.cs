using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spt9LvSkill : SptSkillBase
{
    public GameObject castEffect_Prf;
    public GameObject fireEffect_Prf;

    public GameObject hitEffect_Prf;
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
        List<Vector2> tarPosList = SetEffectPosition();

        GameObject go;
        SptSkillEffect eff;

        foreach (var tarPos in tarPosList)
        {
            go = Instantiate(fireEffect_Prf, tarPos, Quaternion.identity);
            eff = go.GetComponent<SptSkillEffect>();

            eff.Init(
                onApply: null,
                //onEnd: SpawnFinEffect     // 메인 애니 끝나면 FinEffect 생성
                onEnd: () => { Destroy(gameObject); } // 전체 스킬 종료
            );
        }

        SpawnDamageBox();  // 이펙트 생성 직후 데미지

        /*eff.Init(
            onApply: SpawnDamageBox,      // 메인 애니의 Hit 프레임에서 데미지 박스 생성
            onEnd: SpawnFinEffect         // 메인 애니 끝나면 FinEffect 생성
        );*/
    }

    private List<Vector2> SetEffectPosition()
    {
        List<Vector2> tarVectorArr = new();

        Vector2 cor0 = SptGameManager.instance.spawner.corner_00.position;
        Vector2 cor1 = SptGameManager.instance.spawner.corner_01.position;
        Vector2 cor2 = SptGameManager.instance.spawner.corner_02.position;
        Vector2 cor3 = SptGameManager.instance.spawner.corner_03.position;

        Vector2 a1 = Vector2.Lerp(cor0, cor1, 1f / 5f);
        Vector2 a2 = Vector2.Lerp(cor0, cor1, 2f / 5f);
        Vector2 a3 = Vector2.Lerp(cor0, cor1, 3f / 5f);
        Vector2 a4 = Vector2.Lerp(cor0, cor1, 4f / 5f);

        Vector2 b1 = Vector2.Lerp(cor1, cor2, 1f / 5f);
        Vector2 b2 = Vector2.Lerp(cor1, cor2, 2f / 5f);
        Vector2 b3 = Vector2.Lerp(cor1, cor2, 3f / 5f);
        Vector2 b4 = Vector2.Lerp(cor1, cor2, 4f / 5f);

        Vector2 c1 = Vector2.Lerp(cor2, cor3, 1f / 5f);
        Vector2 c2 = Vector2.Lerp(cor2, cor3, 2f / 5f);
        Vector2 c3 = Vector2.Lerp(cor2, cor3, 3f / 5f);
        Vector2 c4 = Vector2.Lerp(cor2, cor3, 4f / 5f);

        Vector2 d1 = Vector2.Lerp(cor0, cor3, 1f / 5f);
        Vector2 d2 = Vector2.Lerp(cor0, cor3, 2f / 5f);
        Vector2 d3 = Vector2.Lerp(cor0, cor3, 3f / 5f);
        Vector2 d4 = Vector2.Lerp(cor0, cor3, 4f / 5f);

        tarVectorArr.Add(cor0);
        tarVectorArr.Add(cor1);
        tarVectorArr.Add(cor2);
        tarVectorArr.Add(cor3);
        tarVectorArr.Add(a1);
        tarVectorArr.Add(a2);
        tarVectorArr.Add(a3);
        tarVectorArr.Add(a4);
        tarVectorArr.Add(b1);
        tarVectorArr.Add(b2);
        tarVectorArr.Add(b3);
        tarVectorArr.Add(b4);
        tarVectorArr.Add(c1);
        tarVectorArr.Add(c2);
        tarVectorArr.Add(c3);
        tarVectorArr.Add(c4);
        tarVectorArr.Add(d1);
        tarVectorArr.Add(d2);
        tarVectorArr.Add(d3);
        tarVectorArr.Add(d4);

        return tarVectorArr;
    }

    public void SpawnDamageBox()
    {
        GameObject dmgBoxOjt = Instantiate(damageBox_Prf, Vector3.zero, Quaternion.identity, transform);
        dmgBoxOjt.GetComponent<SptDamageBox>().SetDamageBox(skillType, skillDamage, new Vector2(skillRange, skillRange), hitEffect_Prf);
    }
}
