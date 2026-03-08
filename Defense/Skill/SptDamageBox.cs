using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SptDamageBox : MonoBehaviour
{
    public List<SptEnemy> targetList;
    public ETowerType attackType;
    public float attackDamage;
    public Collider2D col;
    public GameObject hitEffect;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<SptEnemy>(out SptEnemy enemy))
        {
            bool isHit = true;

            foreach (SptEnemy target in targetList)
            {
                if (target == enemy)
                {
                    isHit = false;
                    break;
                }
            }

            if (isHit)
            {
                Debug.Log($"damage : {attackDamage}");

                targetList.Add(enemy);
                enemy.TakeDamage(attackDamage, attackType);
                GameObject hitEft = Instantiate(hitEffect);
                hitEft.transform.position = enemy.transform.position;
                hitEft.GetComponent<SptSkillEffect>().Init();
            }

        }
    }

    public void SetDamageBox(ETowerType type, float damage, Vector2 size, GameObject effect)
    {
        //Debug.Log("setdamage");

        attackType = type;
        attackDamage = damage;
        col.GetComponent<BoxCollider2D>().size = size;

        hitEffect = effect;
    }
}
