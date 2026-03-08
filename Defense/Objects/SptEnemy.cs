using UnityEngine;
using UnityEngine.UI;

public class SptEnemy : MonoBehaviour
{
    public int enemyLevel;
    public ETowerType bodyType;
    public float curHp;
    public float maxHp;
    public float speed;
    public int armor;
    public int dropGold;
    public RectTransform textPos;

    public Transform spriteTran;
    public float roSpeed;

    public int targetID = 0;                          // 목적지 id값
    public Vector3 target00;
    public Vector3 target01;
    public Vector3 target02;
    public Vector3 target03;
    public Vector3 targetPos;                         // 목적지
    public Vector2 moveDir;                         
    // 이동 방향
    public Vector3 nextPos;                           // 한 프레임 뒤의 이동 위치

    public bool isDead = false;

    private void Update()
    {
        spriteTran.Rotate(0f, 0f, roSpeed);

        if (targetPos == null) return;

        // 다음 프레임에 이동할 장소
        nextPos = transform.position + (Vector3)(moveDir * speed * Time.deltaTime);

        // 이동할 장소가 목표를 넘었는지 체크
        bool isGoal = CheckPos(nextPos);

        if (isGoal)
        {
            // 목표 위치로 이동
            transform.position = targetPos;

            // 목표 위치, 이동 방향 변경
            ChangeDir(targetID);
        }
        else
        {
            // 이동
            transform.position = nextPos;
        }
        // 회전 방향 조정
        //transform.up = moveDir;
    }

    public void TextCall01()
    {
        Debug.Log("hello");
    }

    public bool CheckPos(Vector2 nextPos)
    {
        switch (targetID)
        {
            // 다음 이동 위치가 목표 위치보다 넘어서면 도착
            case 0:
                if (nextPos.x <= targetPos.x)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case 1:
                if (nextPos.y <= targetPos.y)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case 2:
                if (nextPos.x >= targetPos.x)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case 3:
                if (nextPos.y >= targetPos.y)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                Debug.Log($"targetID : {targetID}");
                return false;
        }
    }

    public void ChangeDir(int ID)
    {
        switch (ID)
        {
            case 0:
                targetID++;
                moveDir = Vector2.down; // 아래 방향
                targetPos = target01;
                break;
            case 1:
                targetID++;
                moveDir = Vector2.right; // 오른쪽 방향
                targetPos = target02;
                break;
            case 2:
                targetID++;
                moveDir = Vector2.up; // 위 방향
                targetPos = target03;
                break;
            case 3:
                targetID = 0;
                moveDir = Vector2.left; // 왼쪽 방향
                targetPos = target00;
                break;
        }
    }

    public void Setting(Vector2 corner0, Vector2 corner1, Vector2 corner2, Vector2 corner3)
    {
        curHp = maxHp;

        target00 = new Vector3 (corner0.x, corner0.y, transform.position.z);
        target01 = new Vector3(corner1.x, corner1.y, transform.position.z);
        target02 = new Vector3(corner2.x, corner2.y, transform.position.z);
        target03 = new Vector3(corner3.x, corner3.y, transform.position.z);

        targetPos = target00;
        transform.position = targetPos;
    }

    public void TakeDamage(float damage , ETowerType damageType)
    {
        // 피격 효과음
        SptSoundManager.instance.PlaySFX(ESfx.Hit);

        float totalDamage = damage;

        switch (bodyType)
        {
            case ETowerType.Red:
                if(damageType == ETowerType.Green) totalDamage *= 1.5f;
                break;

            case ETowerType.Blue:
                if (damageType == ETowerType.Red) totalDamage *= 1.5f;
                    break;

            case ETowerType.Green:
                if (damageType == ETowerType.Blue) totalDamage *= 1.5f;
                break;
        }

        totalDamage = Mathf.Max(totalDamage - armor,1);

        curHp -= totalDamage;

        SptGameManager.instance.defenceUI.ShowDamageText($"{totalDamage}", textPos, Color.darkRed);

        //Debug.Log($"남은 체력 : {curHp}");
        //Debug.Log($"데미지 : {totalDamage}");
        if (curHp <= 0)
        {
            Dead();
        }
    }
    public void Dead()
    {
        if (isDead) return;

        //Debug.Log("적 죽음");

        isDead = true;

        // 재화 드롭
        SptGameManager.instance.defenceUI.GetGoldToEnemy(dropGold);

        // 살아있는 적 감소
        SptGameManager.instance.spawner.aliveEnemyCount--;

        // UI 업데이트 함수 호출
        SptGameManager.instance.defenceUI.UpdateEnemyUI(SptGameManager.instance.spawner.aliveEnemyCount);

        //Debug.Log($"EnemyCount : {SptSpawner.aliveEnemyCount}");

        Destroy(gameObject);
    }
}
