using UnityEngine;

public class SptTower : MonoBehaviour
{
    [SerializeField] private int slotID;

    public ETowerType towerType;
    public int towerLevel;
    public string towerName;
    public SpriteRenderer towerIcon;
    public string towerDescription;
    public LayerMask layer;

    public float totalDamage;
    public float attackDamage;
    public float attackRange;
    public float attackSpeed;
    public float attackTimer;
    public float attackCount;
    public int saleGold;
    public bool isSplash = false;

    public Transform target;

    public GameObject pjtPrefab;
    public GameObject skillPrefab;

    public SptDefenceUI ui;

    public bool saleWait = false;

    private void Update()
    {
        attackTimer = attackTimer + Time.deltaTime;

        if (attackTimer >= attackSpeed)
        {
            if (OnAttack())
            {
                attackTimer = 0;
            }
            else
            {
                //최적화로직?
            }
        }
    }

    public bool OnAttack()
    {
        //Debug.Log("OnAttack!");

        // 범위 탐색
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, layer);

        if (enemies.Length > 0)
        {
            // 가장 가까운 거리 = 초기 값(최대치)
            float minDist = float.MaxValue;

            // 탐색으로 나온 개체들의 거리 비교
            foreach (Collider2D collider in enemies)
            {

                // 개체와의 거리 계산
                float dist = Vector2.Distance(transform.position, collider.transform.position);

                // enemy태그를 가지고, 이전 개체의 거리보다 작은 거리를 가진 개체와 거리를 저장
                if (dist < minDist)
                {
                    minDist = dist;
                    target = collider.transform;
                }
                
            }

            // 타겟이 있고 중심부가 사거리 안으로 들어왔으면 공격, 아니면 쿨타임 초기화 방지
            if (target != null && minDist <= attackRange)
            {
                Debug.DrawLine(transform.position, target.position, Color.red);

                InitMissile(towerType, totalDamage, target, isSplash); // 투사체 생성
                CountingAttack();

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            target = null;

            return false;
        }
    }
    
    public void GetAttackDamage()
    {
        int enhanceLevel = 0;
        switch (towerType)
        {
            case ETowerType.Red:
                enhanceLevel = ui.redEnhanceLevel;
                break;
            case ETowerType.Blue:
                enhanceLevel = ui.blueEnhanceLevel;
                break;
            case ETowerType.Green:
                enhanceLevel = ui.greenEnhanceLevel;
                break;
        }
        totalDamage = attackDamage + (attackDamage * enhanceLevel);
    }

    public void InitMissile(ETowerType type, float damage, Transform target, bool isSplash)
    {
        //Debug.Log("투사체 생성");
        SptMissile pjt = Instantiate(pjtPrefab, new Vector3(transform.position.x, transform.position.y, pjtPrefab.transform.position.z), Quaternion.identity).GetComponent<SptMissile>();
        pjt.transform.up = ((Vector2)target.position - (Vector2)transform.position).normalized;
        pjt.target = target;
        pjt.tarPos = target.position;
        pjt.damageType = type;
        pjt.damage = damage;
        pjt.isMultiple = isSplash;
        pjt.isMove = true;
    }

    void OnDrawGizmos()
    {
        // 공격 범위를 원으로 시각화
        Gizmos.color = Color.red; // 색상 설정
        Gizmos.DrawWireSphere(transform.position, attackRange); // 원 그리기
    }

    public void SetSlotNumber(int num)
    {
        slotID = num;
    }
    public int SlotNumber()
    {
        return slotID;
    }

    public void CountingAttack()
    {
        if (skillPrefab == null) return;

        int applySkillCount;
        attackCount++;

        switch (towerLevel)
        {
            case 6: applySkillCount = 10; break;
            case 7: applySkillCount = 8; break;
            case 8: applySkillCount = 6; break;
            case 9: applySkillCount = 4; break;
            default:applySkillCount = 0; break;
        }

        if (attackCount == applySkillCount)
        {
            InitSkill();
            attackCount = 0;
        }
    }
    public void InitSkill()
    {
        Vector2 initPos = transform.position;
        GameObject skillPrf = Instantiate(skillPrefab, initPos, Quaternion.identity);
        skillPrf.GetComponent<SptSkillBase>().OnActive(towerType, totalDamage, attackRange);
    }

    public void DestroyTower()
    {
        // 파괴 이펙트 표시
        Destroy(gameObject);
    }

    public void UpgradeTower()
    {
        // 업그레이드 이펙트 표시
        Destroy(gameObject);
    }
}
