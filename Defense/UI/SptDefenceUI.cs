using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SptDefenceUI : MonoBehaviour
{
    #region ItemCount
    public SptItemCountData itemCountData;
    #endregion

    [Header("Gamble")]
    public List<GameObject> redTowerPrefabs;                  // 전체 레드타워의 프리펩
    public List<GameObject> blueTowerPrefabs;                 // 전체 블루타워의 프리펩
    public List<GameObject> greenTowerPrefabs;                // 전체 그린타워의 프리펩

    public List<SptTowerSlot> slotList;                       // 타워 슬롯 리스트
    public Dictionary<int, Vector2> spawnPositions = new();   // 타워 생성 위치 리스트
    public List<SptTower> spawnTowers;                        // 이미 생성한 타워들
    public Transform towers;                                  // 타워 정리용 부모 오브젝트

    public ETowerType spawnType;                               // 생성 타워 타입
    public int spawnLevel;                                    // 생성 타워 레벨
    public GameObject spawnTowerPrf;                          // 생성할 타워

    [Header("Cost")]
    public int gambleTowerBaseCost = 50;
    public int gambleCoreBaseCost = 50;
    public int gambleBoostBaseCost1 = 100;
    public int gambleBoostBaseCost2 = 150;

    public int gambleCostDiscount = 2;       // 뽑기 금액 감소 아이템 개당 2
    public int enhanceCostDiscount = 4;      // 강화 비용 감소 아이템 개당 4
    public int saleCostIncrease = 2;         // 판매금 상승 아이템 개당 2%
    public int boostGold = 50;

    public int gambleTowerCost;
    public int gambleCoreCost;
    public int gambleBoostCost1;
    public int gambleBoostCost2;
    public int curGold;
    public int curCore;
    public bool redBoost1 = false;
    public bool blueBoost1 = false;
    public bool greenBoost1 = false;
    public bool redBoost2 = false;
    public bool blueBoost2 = false;
    public bool greenBoost2 = false;

    [Header("Movement")]
    public SptTower selecTower;
    public SptTower targetTower;
    public SpriteRenderer rangeCircle;
    public bool isTowerMove = false;

    [Header("TowerPopup")]
    public GameObject towerSelecPopup;
    public GameObject towerSelecPanel;
    public GameObject towerInfoPopup;
    public GameObject towerMovePopup;
    public GameObject skillPanel;
    public TMP_Text towerType;
    public TMP_Text towerLevel;
    public Image towerIcon;
    public TMP_Text attack;
    public TMP_Text attackRange;
    public TMP_Text attackSpeed;
    public TMP_Text attackType;
    public TMP_Text saleGold;
    public Image skillIcon;
    public TMP_Text skillName;
    public TMP_Text skillDescription;

    [Header("EnhancePopup")]
    public GameObject enhancePopup;

    public TMP_Text redEnhanceLevel_TMP;
    public TMP_Text blueEnhanceLevel_TMP;
    public TMP_Text greenEnhanceLevel_TMP;

    public TMP_Text redEnhanceCost_TMP;
    public TMP_Text blueEnhanceCost_TMP;
    public TMP_Text greenEnhanceCost_TMP;

    public int baseCost = 50;
    public int maxLevel = 40;
    public int redEnhanceLevel;
    public int blueEnhanceLevel;
    public int greenEnhanceLevel;

    [Header("Sale")]
    public GameObject salePopup;
    public GameObject selecTypePanel;
    public GameObject redSelecLevelPanel;
    public GameObject blueSelecLevelPanel;
    public GameObject greenSelecLevelPanel;

    private List<SptTower> spawnTowersSale = new List<SptTower>();
    public SptTower saleTower;
    public ETowerType saleType;
    public int saleLevel;

    public Button AutoSaleLv1_Btn;
    public Button AutoSaleLv2_Btn;
    public Button AutoSaleLv3_Btn;
    public Button AutoSaleLv4_Btn;

    public bool isAutoSaleLv1 = false;
    public bool isAutoSaleLv2 = false;
    public bool isAutoSaleLv3 = false;
    public bool isAutoSaleLv4 = false;

    public Sprite autoSaleOn_img;
    public Sprite autoSaleOff_img;

    public Button redLv1_Btn;
    public Button redLv2_Btn;
    public Button redLv3_Btn;
    public Button redLv4_Btn;
    public Button redLv5_Btn;
    public Button redLv6_Btn;
    public Button redLv7_Btn;
    public Button redLv8_Btn;
    public Button redLv9_Btn;

    public Button blueLv1_Btn;
    public Button blueLv2_Btn;
    public Button blueLv3_Btn;
    public Button blueLv4_Btn;
    public Button blueLv5_Btn;
    public Button blueLv6_Btn;
    public Button blueLv7_Btn;
    public Button blueLv8_Btn;
    public Button blueLv9_Btn;

    public Button greenLv1_Btn;
    public Button greenLv2_Btn;
    public Button greenLv3_Btn;
    public Button greenLv4_Btn;
    public Button greenLv5_Btn;
    public Button greenLv6_Btn;
    public Button greenLv7_Btn;
    public Button greenLv8_Btn;
    public Button greenLv9_Btn;

    [Header("RecordUI")]
    public GameObject recordPopup;
    public GameObject defenceRecord;
    public SptTotalRecordSet totalRecord;

    #region DefenceRecord
    public TMP_Text gambleTowerCount_TMP;

    public TMP_Text redLv1SpawnCount_TMP;
    public TMP_Text redLv2SpawnCount_TMP;
    public TMP_Text redLv3SpawnCount_TMP;
    public TMP_Text redLv4SpawnCount_TMP;
    public TMP_Text redLv5SpawnCount_TMP;
    public TMP_Text redLv6SpawnCount_TMP;
    public TMP_Text redLv7SpawnCount_TMP;
    public TMP_Text redLv8SpawnCount_TMP;
    public TMP_Text redLv9SpawnCount_TMP;

    public TMP_Text blueLv1SpawnCount_TMP;
    public TMP_Text blueLv2SpawnCount_TMP;
    public TMP_Text blueLv3SpawnCount_TMP;
    public TMP_Text blueLv4SpawnCount_TMP;
    public TMP_Text blueLv5SpawnCount_TMP;
    public TMP_Text blueLv6SpawnCount_TMP;
    public TMP_Text blueLv7SpawnCount_TMP;
    public TMP_Text blueLv8SpawnCount_TMP;
    public TMP_Text blueLv9SpawnCount_TMP;

    public TMP_Text greenLv1SpawnCount_TMP;
    public TMP_Text greenLv2SpawnCount_TMP;
    public TMP_Text greenLv3SpawnCount_TMP;
    public TMP_Text greenLv4SpawnCount_TMP;
    public TMP_Text greenLv5SpawnCount_TMP;
    public TMP_Text greenLv6SpawnCount_TMP;
    public TMP_Text greenLv7SpawnCount_TMP;
    public TMP_Text greenLv8SpawnCount_TMP;
    public TMP_Text greenLv9SpawnCount_TMP;

    public TMP_Text redEnhanceLv_TMP;
    public TMP_Text blueEnhanceLv_TMP;
    public TMP_Text greenEnhanceLv_TMP;

    public TMP_Text getGold_TMP;
    public TMP_Text useGold_TMP;

    public TMP_Text tryBoostCount_TMP;
    public TMP_Text getRedBoostCount_TMP;
    public TMP_Text getBlueBoostCount_TMP;
    public TMP_Text getGreenBoostCount_TMP;

    public TMP_Text tryCoreCount_TMP;
    public TMP_Text getCoreCount_TMP;
    public TMP_Text useCoreCount_TMP;
    #endregion

    [Header("FinishPopup")]
    public GameObject finishPopup;
    public TMP_Text finishTitle_TMP;
    public TMP_Text goalWave_TMP;
    public TMP_Text getDia_TMP;
    public Button exitGame_Btn;
    public Button viewAds_Btn;
    public GameObject adsDiaPanel;
    public TMP_Text adsGetDia_TMP;

    [Header("LoadingPopup")]
    public GameObject loadingPopup;

    [Header("NoticePopup")]
    public GameObject noticePopups;

    public GameObject roundPopupPrf;
    public GameObject gambleTowerPopupPrf;
    public GameObject gambleCorePopupPrf;
    public GameObject gambleBoostPopupPrf;
    public GameObject gameNoticePopupPrf;

    public List<GameObject> noticeList;
    public List<GameObject> roundNoticeList;
    public bool isPopup = false;

    [Header("LogPopup")]
    public SptLogManager logManager;

    [Header("SettingUI")]
    public SptSettingPopup settingPopup;

    [Header("UtilUI")]
    public TMP_Text enemies_TMP;
    public TMP_Text round_TMP;
    public TMP_Text timer_TMP;
    public TMP_Text gold_TMP;
    public TMP_Text core_TMP;

    public GameObject redBoostCheck1;
    public GameObject blueBoostCheck1;
    public GameObject greenBoostCheck1;
    public GameObject redBoostCheck2;
    public GameObject blueBoostCheck2;
    public GameObject greenBoostCheck2;

    public TMP_Text gambleTowerCost_TMP;
    public TMP_Text gambleCoreCost_TMP;
    public TMP_Text gambleBoost1Cost_TMP;
    public TMP_Text gambleBoost2Cost_TMP;

    public GameObject popupText;                 // 모든 textPopup 오브젝트의 부모
    public GameObject costPopupPrf;
    public GameObject damagePopupPrf;

    #region TowerMove
    public void OnClickSlot(SptTowerSlot slot)
    {
        // 선택 타워가 비어있을 때
        if (selecTower == null)
        {
            // 스폰타워가 0이면 선택할 타워가 없음
            if (spawnTowers.Count == 0)
            {
                selecTower = null;
                return;
            }
            else
            {
                (bool, SptTower) slotA = SlotCheck(slot);
                if (slotA.Item1)
                {
                    // 선택 초기화
                    TowerUnselec();
                    // 슬롯이 채워져 있으면 타워 참조 및 팝업창 열기
                    selecTower = slotA.Item2;
                    // OPNE towerSelecPopup
                    OnTowerSelec();
                    // 사거리 표시
                    ShowRange(selecTower.attackRange);
                }
                else
                {
                    selecTower = null;
                    return;
                }
            }
        }
        // 선택 타워가 있을 때
        else
        {
            (bool, SptTower) slotB = SlotCheck(slot);

            if (isTowerMove)
            {
                if (slotB.Item1)
                {
                    targetTower = slotB.Item2;
                    ChangeTower();
                    OnCloseMove();
                }
                else
                {
                    MoveTower(slot.slotID);
                    OnCloseMove();
                }
            }
            else
            {
                if (slotB.Item1)
                {
                    // 선택 초기화
                    TowerUnselec();
                    // 슬롯이 채워져 있으면 타워 참조 및 팝업창 열기
                    selecTower = slotB.Item2;
                    // OPNE towerSelecPopup
                    OnTowerSelec();
                    // 사거리 표시
                    ShowRange(selecTower.attackRange);
                }
                else
                {
                    TowerUnselec();
                    return;
                }
            }
        }
    }
    public (bool, SptTower) SlotCheck(SptTowerSlot slot)
    {
        // 슬롯에 타워가 있는지 없는지 체크
        for (int i = 0; i < spawnTowers.Count; i++)
        {
            if (spawnTowers[i].SlotNumber() == slot.slotID)
            {
                // 있으면 정보 세팅, 타워 참조
                return (true, spawnTowers[i]);
            }
        }

        // 없으면 아무일도 일어나지 않음
        return (false, null);
    }

    public void OnTowerSelec() // OPNE towerSelecPopup
    {
        towerSelecPopup.SetActive(true);
        // 위치 이동
        towerSelecPanel.transform.position = selecTower.transform.position + new Vector3(0, 0, 0.1f);
    }
    public void OffTowerSelec() // CLOSE towerSelecPopup
    {
        HideRange();
        towerSelecPopup.SetActive(false);
    }
    public void TowerUnselec() // CLOSE towerSelecPopup + 타워 선택 취소
    {
        OffTowerSelec();
        selecTower = null;
    }

    public void OnTowerInfo() // OPEN towerInfoPopup
    {
        OffTowerSelec();
        towerInfoPopup.SetActive(true);
        SetTowerInfo(selecTower);

        if (selecTower.skillPrefab == null) skillPanel.SetActive(false);
        else
        {
            SptSkillBase skill = selecTower.skillPrefab.GetComponent<SptSkillBase>();
            skillPanel.SetActive(true);
            SetSkillInfo(skill);
        }
    }
    public void SetTowerInfo(SptTower tower)
    {
        // Popup창에 타워 정보 세팅
        towerType.text = $"{tower.towerType}";
        towerLevel.text = $"Lv{tower.towerLevel:00}";
        towerIcon.sprite = tower.towerIcon.sprite;
        attack.text = $"{tower.attackDamage}(+{tower.totalDamage - tower.attackDamage})";
        attackRange.text = $"{tower.attackRange}";
        attackSpeed.text = $"{tower.attackSpeed}";

        if (tower.isSplash) attackType.text = "Splash";
        else attackType.text = "Normal";

        saleGold.text = $"{GetSaleGold(tower)}";
    }
    public void SetSkillInfo(SptSkillBase skill)
    {
        skillIcon.sprite = skill.skillIcon;
        skillName.text = skill.skillName;
        skillDescription.text = skill.skillDescription;
    }
    public void OnExitTowerInfo() // CLOSE towerInfoPopup
    {
        // 창 닫기, moveTowerA 삭제
        towerInfoPopup.SetActive(false);
        selecTower = null;
    }

    public void OnSelecMove() // OPNE towerMovePopup
    {
        // 타워 선택창 닫기
        OffTowerSelec();

        // 이동창 열기
        towerMovePopup.SetActive(true);

        // 이동 활성화
        isTowerMove = true;
    }
    public void OnCloseMove() // CLOSE towerMovePopup
    {
        // 이동창 닫기
        towerMovePopup.SetActive(false);
        // 사거리 표시 숨김
        HideRange();
        // 타워 선택 취소
        selecTower = null;
        // 이동 비활성화
        isTowerMove = false;
    }

    public void MoveTower(int id)
    {
        // 타워 이동
        selecTower.SetSlotNumber(id);
        SetTowerLocation(selecTower.gameObject);

        // 선택 타워 제거
        selecTower = null;
    }
    public void ChangeTower()
    {
        // 타워 id 교체
        int idA = selecTower.SlotNumber();
        int idB = targetTower.SlotNumber();
        selecTower.SetSlotNumber(idB);
        targetTower.SetSlotNumber(idA);

        // 타워 교체
        SetTowerLocation(selecTower.gameObject);
        SetTowerLocation(targetTower.gameObject);

        // 선택 타워 제거
        selecTower = null;
        targetTower = null;
    }

    public void ShowRange(float range)
    {
        // 사거리 오브젝트 이동 및 표시
        rangeCircle.gameObject.SetActive(true);
        rangeCircle.transform.position = selecTower.transform.position;
        rangeCircle.transform.position += Vector3.back * 0.01f;
        // 사거리 표시 동적인 변경
        rangeCircle.transform.localScale
        = new Vector3(range * 2, range * 2, 1f);
        // 선택 타워 강조
        selecTower.transform.position -= new Vector3(0, 0, 0.1f);
    }
    public void HideRange()
    {
        // 사거리 오브젝트 숨김
        rangeCircle.gameObject.SetActive(false);
        // 선택 타워 강조 해제
        if (selecTower != null) selecTower.transform.position += new Vector3(0, 0, 0.1f);
    }
    #endregion

    #region TowerSpawn
    public void OnTowerGamble()
    {
        // 금액이 적으면 봅기 불가
        if (curGold < gambleTowerCost || curGold <= 0)
        {
            Debug.Log($"curGold : {curGold}");
            ShowTextLog(3, -1);
            return;
        }

        AllPopupClose();

        SptRandomNumber gambler = GetComponent<SptRandomNumber>();

        var intType = gambler.GetLevelAndType();

        // 스폰할 타워늬 타입과 레벨 지정
        spawnType = (ETowerType)intType.type;
        spawnLevel = intType.level;

        // 빈자리가 있으면
        if (spawnTowers.Count < spawnPositions.Count)
        {
            // 뽑기 비용 지불
            UseGold(gambleTowerCost);

            // 스폰할 타워의 등급을 기록
            SptRecordManager.instance.GambleTowerRecord(spawnType, spawnLevel);
        }
        // 빈자리 없으면
        else
        {
            Debug.Log($"빈자리 없음");
            return;
        }

        FindTowerPrf(spawnType, spawnLevel);
        SpawnTower();

        // Lv5 이상의 타워뽑기 알람
        //if (spawnLevel >= 5) StartCoroutine(ShowGambleTowerPopupRoutine(spawnLevel));
        if (spawnLevel >= 5) ShowGambleTowerNotice(spawnLevel);
        ShowTextLog(0, spawnLevel);
    }
    public void FindTowerPrf(ETowerType type, int level)
    {
        List<GameObject> findPrefabs = new();

        if (type < 0) Debug.Log($"잘못된 타입 : {type}");

        // 타입에 따라 찾을 리스트 선택
        switch (type)
        {
            case ETowerType.Red:
                findPrefabs = redTowerPrefabs;
                break;
            case ETowerType.Blue:
                findPrefabs = blueTowerPrefabs;
                break;
            case ETowerType.Green:
                findPrefabs = greenTowerPrefabs;
                break;
        }

        GameObject towerOjt = null;

        // 생성할 타워 찾기
        foreach (var towerPrf in findPrefabs)
        {
            SptTower towerSpt = towerPrf.GetComponent<SptTower>();

            // 레벨, 타입의 동일 여부 체크
            if (towerSpt.towerLevel == level && towerSpt.towerType == type)
            {
                towerOjt = towerPrf;
                //Debug.Log($"{towerOjt.name}");
                break;
            }
        }

        //Debug.Log($"{towerOjt == null}");

        spawnTowerPrf = towerOjt;
    }
    public GameObject SpawnTower()//매개변수ni에 int아이디값
    {
        GameObject spawnTower;
        if (spawnTowers.Count < spawnPositions.Count)  //spawnTowers가 spawnPositions.Count보다 작으면 빈자리 있음
        {
            // 타워 스폰 및 세팅
            spawnTower = Instantiate(spawnTowerPrf, spawnTowerPrf.transform.position, Quaternion.identity, towers);
            Debug.Log($"{spawnTower.transform.position}");
            spawnTower.GetComponent<SptTower>().ui = this;
            spawnTower.GetComponent<SptTower>().GetAttackDamage();

            //if로 ni가 0>인지 체크
            //true면 setSlotNum... 이거 실행
            //else는 아래 실행
            // 스폰할 슬롯ID 세팅
            SetSlotID(spawnTower);

            // 위치이동
            SetTowerLocation(spawnTower);

            // 자동 판매 체크
            TowerSaleCheck(spawnTower);
        }
        else
        {
            Debug.Log("자리 없음");
            spawnTower = null;
        }
        return spawnTower;
    }

    void SetSlotID(GameObject spawnTower)
    {
        if (spawnTowers == null)  //spawnTowers가 0이면 가운데 자리 비어있음
        {
            Debug.Log("자리 있음");
            spawnTower.GetComponent<SptTower>().SetSlotNumber(0);  // slotID = 0 저장
        }
        else  //spawnTowers가 0이 아니면 채워져 있을 수 도 있음
        {
            int mT = spawnTowers.Count;
            for (int i = 0; i < spawnPositions.Count; i++)          // slot의 수 만큼 반복
            {
                bool validSlot = true;            // true == 자리가 비어있음
                for (int t = 0; t < mT; t++)      // spawnTowers.Count만큼 반복
                {
                    if (spawnTowers[t].SlotNumber() == i && !spawnTowers[t].saleWait)  // slotID 체크
                    {
                        validSlot = false;        // 같으면 자리에 타워 있음
                        break;
                    }
                }
                if (validSlot)
                {
                    spawnTower.GetComponent<SptTower>().SetSlotNumber(i); // slotID 저장
                    spawnTowers.Add(spawnTower.GetComponent<SptTower>()); // spawnTowers에 저장
                    break;
                }
            }
        }
    }

    void SetTowerLocation(GameObject newTower)
    {
        // slotID에 맞는 위치 찾기
        Vector3 targetLocalPos = spawnPositions[newTower.GetComponent<SptTower>().SlotNumber()];
        targetLocalPos.z = newTower.transform.localPosition.z;

        newTower.transform.localPosition = targetLocalPos;
        //spawnTower = null;
        //Debug.Log(targetLocalPos);
    }
    void TowerSaleCheck(GameObject newTower)
    {
        SptTower sTower = newTower.GetComponent<SptTower>();

        if (isAutoSaleLv1)
        {
            if (sTower.towerLevel == 1)
            {
                CheckAutoSaleTower(sTower);
                return;
            }
        }
        if (isAutoSaleLv2)
        {
            if (sTower.towerLevel == 2)
            {
                CheckAutoSaleTower(sTower);
                return;
            }
        }
        if (isAutoSaleLv3)
        {
            if (sTower.towerLevel == 3)
            {
                CheckAutoSaleTower(sTower);
                return;
            }
        }
        if (isAutoSaleLv4)
        {
            if (sTower.towerLevel == 4)
            {
                CheckAutoSaleTower(sTower);
                return;
            }
        }
    }
    void CheckAutoSaleTower(SptTower sTower)
    {
        sTower.saleWait = true;

        StartCoroutine(SaleTowerAuto(sTower));
    }
    IEnumerator SaleTowerAuto(SptTower sTower)
    {
        yield return new WaitForSeconds(0.1f);

        // 판매금 저장
        int saleGold = sTower.saleGold;
        // 타워 제거
        SaleRemoveTower(sTower);
        // 판매금 정산 로직
        GetGoldToSale(saleGold);
    }

    public void AllTowerDestroy()
    {
        if (spawnTowers.Count <= 0) return;

        for (int i = 0; i < spawnTowers.Count; i++)
        {
            SptTower GO = spawnTowers[i];
            if (GO != null) GO.DestroyTower();
        }

        spawnTowers = null;
    }
    #endregion

    #region TowerEnhance
    public void OnEnhance(int type)
    {
        int cost = EnhanceGold(type);

        switch (type)
        {
            case 0:
                if (curGold < cost)
                {
                    ShowTextLog(3, -1);
                    return;
                }
                if (redEnhanceLevel >= maxLevel)
                {
                    ShowTextLog(4, -1);
                    return;
                }
                redEnhanceLevel++;
                Debug.Log($"redEnhanceLevel : {redEnhanceLevel}");
                break;
            case 1:
                if (curGold < cost)
                {
                    ShowTextLog(3, -1);
                    return;
                }
                if (blueEnhanceLevel >= maxLevel)
                {
                    ShowTextLog(4, -1);
                    return;
                }
                blueEnhanceLevel++;
                Debug.Log($"blueEnhanceLevel : {blueEnhanceLevel}");
                break;
            case 2:
                if (curGold < cost)
                {
                    ShowTextLog(3, -1);
                    return;
                }
                if (greenEnhanceLevel >= maxLevel)
                {
                    ShowTextLog(4, -1);
                    return;
                }
                greenEnhanceLevel++;
                Debug.Log($"greenEnhanceLevel : {greenEnhanceLevel}");
                break;
        }

        UseGold(cost);

        // 강화 타입에 따라 스폰된 타워 검색 후 데미지 강화 적용
        List<SptTower> tarTowerList = new();
        foreach (SptTower tower in spawnTowers)
        {
            if (tower.towerType == (ETowerType)type)
            {
                tower.GetAttackDamage();
            }
        }

        // 레벨 상승 표기
        SetEnhancePopup();
    }

    public int EnhanceGold(int type)
    {
        int costGold = 0;

        switch (type)
        {
            case 0:
                costGold = (int)((baseCost + (redEnhanceLevel * baseCost)) / (1 + (itemCountData.item_101 * enhanceCostDiscount / 100f)));
                Debug.Log($"red{redEnhanceLevel}LvCost : {costGold}");
                break;
            case 1:
                costGold = (int)((baseCost + (blueEnhanceLevel * baseCost)) / (1 + (itemCountData.item_102 * enhanceCostDiscount / 100f)));
                Debug.Log($"blue{blueEnhanceLevel}Lv Cost : {costGold}");
                break;
            case 2:
                costGold = (int)((baseCost + (greenEnhanceLevel * baseCost)) / (1 + (itemCountData.item_103 * enhanceCostDiscount / 100f)));
                Debug.Log($"green{greenEnhanceLevel}Lv Cost : {costGold}");
                break;
        }

        return costGold;
    }
    public void OnEnhancePopup()
    {
        if (enhancePopup.activeSelf)
        {
            OffEnhancePopup();
        }
        else
        {
            AllPopupClose();

            enhancePopup.SetActive(true);
            SetEnhancePopup();
        }

    }
    public void OffEnhancePopup()
    {
        enhancePopup.SetActive(false);
    }
    public void SetEnhancePopup()
    {
        redEnhanceLevel_TMP.text = $"Lv{redEnhanceLevel}";
        blueEnhanceLevel_TMP.text = $"Lv{blueEnhanceLevel}";
        greenEnhanceLevel_TMP.text = $"Lv{greenEnhanceLevel}";

        redEnhanceCost_TMP.text = $"{EnhanceGold(0)}G";
        blueEnhanceCost_TMP.text = $"{EnhanceGold(1)}G";
        greenEnhanceCost_TMP.text = $"{EnhanceGold(2)}G";
    }
    #endregion

    #region TowerSale
    public void OnSaleTypeRed()
    {
        saleType = ETowerType.Red;

        OnLevelPanel();
    }
    public void OnSaleTypeBlue()
    {
        saleType = ETowerType.Blue;

        OnLevelPanel();
    }
    public void OnSaleTypeGreen()
    {
        saleType = ETowerType.Green;

        OnLevelPanel();
    }

    public void OnSaleLevelLv1()
    {
        saleLevel = 1;

        SaleTower(saleType, saleLevel);
        UpdateSaleButtonUI();
    }
    public void OnSaleLevelLv2()
    {
        saleLevel = 2;

        SaleTower(saleType, saleLevel);
        UpdateSaleButtonUI();
    }
    public void OnSaleLevelLv3()
    {
        saleLevel = 3;

        SaleTower(saleType, saleLevel);
        UpdateSaleButtonUI();
    }
    public void OnSaleLevelLv4()
    {
        saleLevel = 4;

        SaleTower(saleType, saleLevel);
        UpdateSaleButtonUI();
    }
    public void OnSaleLevelLv5()
    {
        saleLevel = 5;

        SaleTower(saleType, saleLevel);
        UpdateSaleButtonUI();
    }
    public void OnSaleLevelLv6()
    {
        saleLevel = 6;

        SaleTower(saleType, saleLevel);
        UpdateSaleButtonUI();
    }
    public void OnSaleLevelLv7()
    {
        saleLevel = 7;

        SaleTower(saleType, saleLevel);
        UpdateSaleButtonUI();
    }
    public void OnSaleLevelLv8()
    {
        saleLevel = 8;

        SaleTower(saleType, saleLevel);
        UpdateSaleButtonUI();
    }
    public void OnSaleLevelLv9()
    {
        saleLevel = 9;

        SaleTower(saleType, saleLevel);
        UpdateSaleButtonUI();
    }

    public void UpdateSaleButtonUI()
    {
        // 현재 타워 갯수
        int redLv1 = 0;
        int redLv2 = 0;
        int redLv3 = 0;
        int redLv4 = 0;
        int redLv5 = 0;
        int redLv6 = 0;
        int redLv7 = 0;
        int redLv8 = 0;
        int redLv9 = 0;

        int blueLv1 = 0;
        int blueLv2 = 0;
        int blueLv3 = 0;
        int blueLv4 = 0;
        int blueLv5 = 0;
        int blueLv6 = 0;
        int blueLv7 = 0;
        int blueLv8 = 0;
        int blueLv9 = 0;

        int greenLv1 = 0;
        int greenLv2 = 0;
        int greenLv3 = 0;
        int greenLv4 = 0;
        int greenLv5 = 0;
        int greenLv6 = 0;
        int greenLv7 = 0;
        int greenLv8 = 0;
        int greenLv9 = 0;

        // 타워 카운팅
        foreach (SptTower tower in spawnTowers)
        {
            if (tower.towerType == ETowerType.Red)
            {
                switch (tower.towerLevel)
                {
                    case 1: redLv1++; break;
                    case 2: redLv2++; break;
                    case 3: redLv3++; break;
                    case 4: redLv4++; break;
                    case 5: redLv5++; break;
                    case 6: redLv6++; break;
                    case 7: redLv7++; break;
                    case 8: redLv8++; break;
                    case 9: redLv9++; break;
                }
            }
            else if (tower.towerType == ETowerType.Blue)
            {
                switch (tower.towerLevel)
                {
                    case 1: blueLv1++; break;
                    case 2: blueLv2++; break;
                    case 3: blueLv3++; break;
                    case 4: blueLv4++; break;
                    case 5: blueLv5++; break;
                    case 6: blueLv6++; break;
                    case 7: blueLv7++; break;
                    case 8: blueLv8++; break;
                    case 9: blueLv9++; break;
                }
            }
            else if (tower.towerType == ETowerType.Green)
            {
                switch (tower.towerLevel)
                {
                    case 1: greenLv1++; break;
                    case 2: greenLv2++; break;
                    case 3: greenLv3++; break;
                    case 4: greenLv4++; break;
                    case 5: greenLv5++; break;
                    case 6: greenLv6++; break;
                    case 7: greenLv7++; break;
                    case 8: greenLv8++; break;
                    case 9: greenLv9++; break;
                }
            }
        }

        // 레드 타워 카운팅 적용 및 카운트 == 0이면 비활성화
        if (redLv1 > 0) { redLv1_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.1\nx{redLv1}"; redLv1_Btn.GetComponent<Image>().color = Color.white; }
        else redLv1_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.1"; redLv1_Btn.GetComponent<Image>().color = Color.gray;
        
        if (redLv2 > 0) { redLv2_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.2\nx{redLv2}"; redLv2_Btn.GetComponent<Image>().color = Color.white; }
        else redLv2_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.2"; redLv2_Btn.GetComponent<Image>().color = Color.gray;
        
        if (redLv3 > 0) { redLv3_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.3\nx{redLv3}"; redLv3_Btn.GetComponent<Image>().color = Color.white; }
        else redLv3_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.3"; redLv3_Btn.GetComponent<Image>().color = Color.gray;
        
        if (redLv4 > 0) { redLv4_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.4\nx{redLv4}"; redLv4_Btn.GetComponent<Image>().color = Color.white; }
        else redLv4_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.4"; redLv4_Btn.GetComponent<Image>().color = Color.gray;
        
        if (redLv5 > 0) { redLv5_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.5\nx{redLv5}"; redLv5_Btn.GetComponent<Image>().color = Color.white; }
        else redLv5_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.5"; redLv5_Btn.GetComponent<Image>().color = Color.gray;
        
        if (redLv6 > 0) { redLv6_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.6\nx{redLv6}"; redLv6_Btn.GetComponent<Image>().color = Color.white; }
        else redLv6_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.6"; redLv6_Btn.GetComponent<Image>().color = Color.gray;
        
        if (redLv7 > 0) { redLv7_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.7\nx{redLv7}"; redLv7_Btn.GetComponent<Image>().color = Color.white; }
        else redLv7_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.7"; redLv7_Btn.GetComponent<Image>().color = Color.gray;
        
        if (redLv8 > 0) { redLv8_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.8\nx{redLv8}"; redLv8_Btn.GetComponent<Image>().color = Color.white; }
        else redLv8_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.8"; redLv8_Btn.GetComponent<Image>().color = Color.gray;
        
        if (redLv9 > 0) { redLv9_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.9\nx{redLv9}"; redLv9_Btn.GetComponent<Image>().color = Color.white; }
        else redLv9_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.9"; redLv9_Btn.GetComponent<Image>().color = Color.gray;


        // 블루 타워 카운팅 적용 및 카운트 == 0이면 비활성화
        if (blueLv1 > 0) { blueLv1_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.1\nx{blueLv1}"; blueLv1_Btn.GetComponent<Image>().color = Color.white; }
        else blueLv1_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.1"; blueLv1_Btn.GetComponent<Image>().color = Color.gray;
        
        if (blueLv2 > 0) { blueLv2_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.2\nx{blueLv2}"; blueLv2_Btn.GetComponent<Image>().color = Color.white; }
        else blueLv2_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.2"; blueLv2_Btn.GetComponent<Image>().color = Color.gray;
        
        if (blueLv3 > 0) { blueLv3_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.3\nx{blueLv3}"; blueLv3_Btn.GetComponent<Image>().color = Color.white; }
        else blueLv3_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.3"; blueLv3_Btn.GetComponent<Image>().color = Color.gray;
        
        if (blueLv4 > 0) { blueLv4_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.4\nx{blueLv4}"; blueLv4_Btn.GetComponent<Image>().color = Color.white; }
        else blueLv4_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.4"; blueLv4_Btn.GetComponent<Image>().color = Color.gray;
        
        if (blueLv5 > 0) { blueLv5_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.5\nx{blueLv5}"; blueLv5_Btn.GetComponent<Image>().color = Color.white; }
        else blueLv5_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.5"; blueLv5_Btn.GetComponent<Image>().color = Color.gray;
        
        if (blueLv6 > 0) { blueLv6_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.6\nx{blueLv6}"; blueLv6_Btn.GetComponent<Image>().color = Color.white; }
        else blueLv6_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.6"; blueLv6_Btn.GetComponent<Image>().color = Color.gray;
        
        if (blueLv7 > 0) { blueLv7_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.7\nx{blueLv7}"; blueLv7_Btn.GetComponent<Image>().color = Color.white; }
        else blueLv7_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.7"; blueLv7_Btn.GetComponent<Image>().color = Color.gray;
        
        if (blueLv8 > 0) { blueLv8_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.8\nx{blueLv8}"; blueLv8_Btn.GetComponent<Image>().color = Color.white; }
        else blueLv8_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.8"; blueLv8_Btn.GetComponent<Image>().color = Color.gray;
        
        if (blueLv9 > 0) { blueLv9_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.9\nx{blueLv9}"; blueLv9_Btn.GetComponent<Image>().color = Color.white; }
        else blueLv9_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.9"; blueLv9_Btn.GetComponent<Image>().color = Color.gray;


        // 그린 타워 카운팅 적용 및 카운트 == 0이면 비활성화
        if (greenLv1 > 0) { greenLv1_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.1\nx{greenLv1}"; greenLv1_Btn.GetComponent<Image>().color = Color.white; }
        else greenLv1_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.1\nx{greenLv1}"; greenLv1_Btn.GetComponent<Image>().color = Color.gray;
        
        if (greenLv2 > 0) { greenLv2_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.2\nx{greenLv2}"; greenLv2_Btn.GetComponent<Image>().color = Color.white; }
        else greenLv2_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.2"; greenLv2_Btn.GetComponent<Image>().color = Color.gray;
        
        if (greenLv3 > 0) { greenLv3_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.3\nx{greenLv3}"; greenLv3_Btn.GetComponent<Image>().color = Color.white; }
        else greenLv3_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.3"; greenLv3_Btn.GetComponent<Image>().color = Color.gray;
        
        if (greenLv4 > 0) { greenLv4_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.4\nx{greenLv4}"; greenLv4_Btn.GetComponent<Image>().color = Color.white; }
        else greenLv4_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.4"; greenLv4_Btn.GetComponent<Image>().color = Color.gray;
        
        if (greenLv5 > 0) { greenLv5_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.5\nx{greenLv5}"; greenLv5_Btn.GetComponent<Image>().color = Color.white; }
        else greenLv5_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.5"; greenLv5_Btn.GetComponent<Image>().color = Color.gray;
        
        if (greenLv6 > 0) { greenLv6_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.6\nx{greenLv6}"; greenLv6_Btn.GetComponent<Image>().color = Color.white; }
        else greenLv6_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.6"; greenLv6_Btn.GetComponent<Image>().color = Color.gray;
        
        if (greenLv7 > 0) { greenLv7_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.7\nx{greenLv7}"; greenLv7_Btn.GetComponent<Image>().color = Color.white; }
        else greenLv7_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.7"; greenLv7_Btn.GetComponent<Image>().color = Color.gray;
        
        if (greenLv8 > 0) { greenLv8_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.8\nx{greenLv8}"; greenLv8_Btn.GetComponent<Image>().color = Color.white; }
        else greenLv8_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.8"; greenLv8_Btn.GetComponent<Image>().color = Color.gray;
        
        if (greenLv9 > 0) { greenLv9_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.9\nx{greenLv9}"; greenLv9_Btn.GetComponent<Image>().color = Color.white; }
        else greenLv9_Btn.GetComponentInChildren<TMP_Text>().text = $"Lv.9"; greenLv9_Btn.GetComponent<Image>().color = Color.gray;
    }

    public void SaleTower(ETowerType towerType, int towerLevel)
    {
        saleTower = SearchSaleTower(towerType, towerLevel);

        if (saleTower == null)
        {
            Debug.Log("팔 수 있는 타워가 없음");
            return;
        }

        // 판매금 저장
        int saleGold = GetSaleGold(saleTower);
        // 타워 제거
        SaleRemoveTower(saleTower);
        // 판매금 정산 로직
        GetGoldToSale(saleGold);
    }
    public SptTower SearchSaleTower(ETowerType towerType, int towerLevel)
    {
        SptTower tower = null;
        int spawnTowerCount = spawnTowers.Count;
        for (int i = 0; i <= spawnPositions.Count; i++)          // slot의 수 만큼 반복
        {
            bool validSlot = true;            // true == 자리가 비어있음
            SptTower selectower = null;

            for (int t = 0; t < spawnTowerCount; t++)      // spawnTowers.Count만큼 반복
            {
                if (spawnTowers[t].SlotNumber() == i && !spawnTowers[t].saleWait)  // slotID 체크
                {
                    validSlot = false;        // 같으면 자리에 타워 있음
                    selectower = spawnTowers[t];
                    break;
                }
            }

            if (!validSlot)
            {
                if (selectower.towerType == towerType && selectower.towerLevel == towerLevel)
                {
                    tower = selectower;
                    break;
                }
            }
        }
        return tower;
    }
    public void SaleRemoveTower(SptTower tower)
    {
        for (int i = 0; i < spawnTowers.Count; i++)
        {
            if (spawnTowers[i] == tower)
            {
                spawnTowers.RemoveAt(i);
            }
        }

        tower.DestroyTower();
    }

    public void OnSaleToTowerInfo() // TowerInfo창에서 판매
    {
        saleTower = selecTower;

        if (saleTower == null)
        {
            Debug.Log("팔 수 있는 타워가 없음");
            return;
        }

        // 판매금 저장
        int saleGold = GetSaleGold(saleTower);
        // 타워 제거
        SaleRemoveTower(saleTower);
        // 판매금 정산 로직
        GetGoldToSale(saleGold);
        // 타워 정보 창 닫기
        OnExitTowerInfo();
    }
    public int GetSaleGold(SptTower tower)
    {
        int baseSaleGold = tower.saleGold;
        int totalSaleGold;

        switch (tower.towerType, tower.towerLevel)
        {
            // 1Lv Cost
            case (ETowerType.Red, 1):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_104 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Blue, 1):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_105 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Green, 1):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_106 * saleCostIncrease / 100f));
                break;

            // 2-3Lv Cost
            case (ETowerType.Red, 2):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_201 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Blue, 2):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_202 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Green, 2):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_203 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Red, 3):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_204 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Blue, 3):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_205 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Green, 3):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_206 * saleCostIncrease / 100f));
                break;

            // 4-9Lv Cost
            case (ETowerType.Red, 4):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_300 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Blue, 4):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_301 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Green, 4):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_302 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Red, 5):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_303 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Blue, 5):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_304 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Green, 5):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_305 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Red, 6):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_306 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Blue, 6):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_307 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Green, 6):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_308 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Red, 7):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_309 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Blue, 7):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_310 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Green, 7):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_311 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Red, 8):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_312 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Blue, 8):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_313 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Green, 8):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_314 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Red, 9):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_315 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Blue, 9):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_316 * saleCostIncrease / 100f));
                break;
            case (ETowerType.Green, 9):
                totalSaleGold = (int)(baseSaleGold + (baseSaleGold * itemCountData.item_317 * saleCostIncrease / 100f));
                break;

            default:
                totalSaleGold = 0;
                break;
        }

        return totalSaleGold;
    }

    public void OnLevelPanel()
    {
        if (selecTypePanel.activeSelf)
        {
            selecTypePanel.SetActive(false);

            switch (saleType)
            {
                case ETowerType.Red:
                    redSelecLevelPanel.SetActive(true);
                    break;

                case ETowerType.Blue:
                    blueSelecLevelPanel.SetActive(true);
                    break;

                case ETowerType.Green:
                    greenSelecLevelPanel.SetActive(true);
                    break;
            }
        }
        else
        {
            selecTypePanel.SetActive(true);

            switch (saleType)
            {
                case ETowerType.Red:
                    redSelecLevelPanel.SetActive(false);
                    break;

                case ETowerType.Blue:
                    blueSelecLevelPanel.SetActive(false);
                    break;

                case ETowerType.Green:
                    greenSelecLevelPanel.SetActive(false);
                    break;
            }
        }

        UpdateSaleButtonUI();
    }
    public void OnSalePopup()
    {
        if (salePopup.activeSelf)
        {
            OffSalePopup();
        }
        else
        {
            AllPopupClose();

            salePopup.SetActive(true);
            selecTypePanel.SetActive(true);
            redSelecLevelPanel.SetActive(false);
            blueSelecLevelPanel.SetActive(false);
            greenSelecLevelPanel.SetActive(false);
        }
    }
    public void OffSalePopup()
    {
        if (selecTypePanel.activeSelf || redSelecLevelPanel.activeSelf || blueSelecLevelPanel.activeSelf || greenSelecLevelPanel.activeSelf)
        {
            salePopup.SetActive(false);
        }
    }
    #endregion

    #region TowerLvUp
    public void OnTryLvUp()
    {
        if (curCore <= 0)
        {
            ShowTextLog(3, -1);
            return;
        }

        Debug.Log("OnLvUpGamble");

        UseCore(1);

        int randomNum = GetComponent<SptRandomNumber>().GetRandomNumber();
        //int randomNum = 1;
        int towerLv = selecTower.towerLevel;

        float levelUpNum;
        switch (towerLv)
        {
            case 1:
                levelUpNum = 9000;
                break;
            case 2:
                levelUpNum = 6000;
                break;
            case 3:
                levelUpNum = 3000;
                break;
            case 4:
                levelUpNum = 1000;
                break;
            case 5:
                levelUpNum = 300;
                break;
            case 6:
                levelUpNum = 100;
                break;
            case 7:
                levelUpNum = 30;
                break;
            case 8:
                levelUpNum = 10;
                break;

            default:
                levelUpNum = 0;
                break;
        }

        Debug.Log($"{levelUpNum}");

        if (randomNum < levelUpNum)
        {
            TowerLvUp(selecTower);

            if (towerLv + 1 >= 5) ShowGambleTowerNotice(towerLv + 1);
            ShowTextLog(0, towerLv + 1);
        }
        else
        {
            //ShowGambleFailPopup();
            ShowTextLog(-1, -1);
        }
    }
    public void TowerLvUp(SptTower selecTower)
    {
        ETowerType type = selecTower.towerType;
        int level = selecTower.towerLevel + 1;
        int slotID = selecTower.SlotNumber();

        // 타워 정보창 닫기
        OnExitTowerInfo();

        //Debug.Log(selecTower);

        // 먼저 파괴
        UpgradeRemoveTower(selecTower);

        // 다음 단계 타워의 프리펩 검색
        FindTowerPrf(type, level);

        // 스폰된 타워 참조;
        GameObject spawnTower = SpawnTower();

        // 타워 슬롯 아이디를 변경하는 함수
        spawnTower.GetComponent<SptTower>().SetSlotNumber(slotID);

        // 위치이동
        SetTowerLocation(spawnTower);
    }
    public void UpgradeRemoveTower(SptTower tower)
    {
        for (int i = 0; i < spawnTowers.Count; i++)
        {
            if (spawnTowers[i] == tower)
            {
                spawnTowers.RemoveAt(i);
            }
        }

        tower.UpgradeTower();
    }
    #endregion

    #region Gamble
    public void OnCoreGamble()
    {
        if (curGold < gambleCoreCost)
        {
            ShowTextLog(3, -1);
            return;
        }

        Debug.Log("OnCoreGamble");

        UseGold(gambleCoreCost);
        SptRecordManager.instance.TryCoreRecord();
        float randomNumber = Random.value;
        float randomValue = Random.value;

        if (randomNumber < 0.25)
        {
            int value = 0;
            if (randomValue > 0.99) // 1%
            {
                value = 10;
            }
            else if (randomValue > 0.955) // 3.5%
            {
                value = 8;
            }
            else if (randomValue > 0.9) // 5.5%
            {
                value = 6;
            }
            else if (randomValue > 0.75) // 15%
            {
                value = 4;
            }
            else if (randomValue > 0.5) // 25%
            {
                value = 2;
            }
            else if (randomValue >= 0) // 50%
            {
                value = 1;
            }

            GetCore(value);
        }
        else
        {
            //ShowGambleFailPopup();
            ShowTextLog(-1, -1);
        }

    }
    public void OnBoostGamble1()
    {
        if (curGold < gambleBoostCost1)
        {
            ShowTextLog(3, -1);
            return;
        }

        // 부스트 3개 전부 있는지 if로 체크
        if (redBoost1 && blueBoost1 && greenBoost1)
        {
            ShowTextLog(5, -1);
            return;
        }

        Debug.Log("OnBoostGamble1");

        UseGold(gambleBoostCost1);
        SptRecordManager.instance.TryBoostRecord();

        float randomNumber = Random.value;
        float randamTypeNum = Random.value;
        ETowerType randamType = ETowerType.Normal;


        if (randomNumber > 0.9)
        {
            // 셋다 없으면
            if (!redBoost1 && !blueBoost1 && !greenBoost1)
            {
                if (randamTypeNum > 0.666) randamType = ETowerType.Red;
                else if (randamTypeNum > 0.333) randamType = ETowerType.Blue;
                else if (randamTypeNum > 0) randamType = ETowerType.Green;
            }
            // greenBoost 있으면
            else if (!redBoost1 && !blueBoost1 && greenBoost1)
            {
                if (randamTypeNum > 0.5) randamType = ETowerType.Red;
                else randamType = ETowerType.Blue;
            }
            // blueBoost 있으면
            else if (!redBoost1 && blueBoost1 && !greenBoost1)
            {
                if (randamTypeNum > 0.5) randamType = ETowerType.Red;
                else randamType = ETowerType.Green;
            }
            // redBoost 있으면
            else if (redBoost1 && !blueBoost1 && !greenBoost1)
            {
                if (randamTypeNum > 0.5) randamType = ETowerType.Blue;
                else randamType = ETowerType.Green;
            }
            // 단일 부스트 미보유 시
            else if (!redBoost1 && blueBoost1 && greenBoost1) randamType = ETowerType.Red;
            else if (redBoost1 && !blueBoost1 && greenBoost1) randamType = ETowerType.Blue;
            else if (redBoost1 && blueBoost1 && !greenBoost1) randamType = ETowerType.Green;

            if (randamType == ETowerType.Red) Debug.Log("GetBoostRed");
            else if (randamType == ETowerType.Blue) Debug.Log("GetBoostBlue");
            else Debug.Log("GetBoostGreen");

            GetBoost(1, randamType);
        }
        else
        {
            //ShowGambleFailPopup();
            ShowTextLog(-1, -1);
            return;
        }

        // 강화 타입에 따라 스폰된 타워 검색 후 데미지 강화 적용
        foreach (SptTower tower in spawnTowers)
        {
            if (tower.towerType == randamType)
            {
                tower.GetAttackDamage();
            }
        }
    }
    public void OnBoostGamble2()
    {
        if (curGold < gambleBoostCost2)
        {
            ShowTextLog(3, -1);
            return;
        }

        // 부스트 3개 전부 있는지 if로 체크
        if (redBoost2 && blueBoost2 && greenBoost2)
        {
            ShowTextLog(5, -1);
            return;
        }

        Debug.Log("OnBoostGamble2");

        UseGold(gambleBoostCost2);
        SptRecordManager.instance.TryBoostRecord();

        float randomNumber = Random.value;
        float randamTypeNum = Random.value;
        ETowerType randamType = ETowerType.Normal;

        if (randomNumber > 0.9)
        {
            // 셋다 없으면
            if (!redBoost2 && !blueBoost2 && !greenBoost2)
            {
                if (randamTypeNum > 0.666) randamType = ETowerType.Red;
                else if (randamTypeNum > 0.333) randamType = ETowerType.Blue;
                else if (randamTypeNum > 0) randamType = ETowerType.Green;
            }
            // greenBoost 있으면
            else if (!redBoost2 && !blueBoost2 && greenBoost2)
            {
                if (randamTypeNum > 0.5) randamType = ETowerType.Red;
                else randamType = ETowerType.Blue;
            }
            // blueBoost 있으면
            else if (!redBoost2 && blueBoost2 && !greenBoost2)
            {
                if (randamTypeNum > 0.5) randamType = ETowerType.Red;
                else randamType = ETowerType.Green;
            }
            // redBoost 있으면
            else if (redBoost2 && !blueBoost2 && !greenBoost2)
            {
                if (randamTypeNum > 0.5) randamType = ETowerType.Blue;
                else randamType = ETowerType.Green;
            }
            // 단일 부스트 미보유 시
            else if (!redBoost2 && blueBoost2 && greenBoost2) randamType = ETowerType.Red;
            else if (redBoost2 && !blueBoost2 && greenBoost2) randamType = ETowerType.Blue;
            else if (redBoost2 && blueBoost2 && !greenBoost2) randamType = ETowerType.Green;

            if (randamType == ETowerType.Red) Debug.Log("GetBoostRed");
            else if (randamType == ETowerType.Blue) Debug.Log("GetBoostBlue");
            else Debug.Log("GetBoostGreen");

            GetBoost(2, randamType);
        }
        else
        {
            //ShowGambleFailPopup();
            ShowTextLog(-1, -1);
            return;
        }

        // 강화 타입에 따라 스폰된 타워 검색 후 데미지 강화 적용
        foreach (SptTower tower in spawnTowers)
        {
            if (tower.towerType == randamType)
            {
                tower.GetAttackDamage();
            }
        }
    }
    #endregion

    #region AutoCheck
    public void StartAutoSale(int level)
    {
        spawnTowersSale.Clear();

        int lengthA = spawnTowers.Count;

        Debug.Log(lengthA);

        // 원본 리스트에서 조건에 맞는 요소들만 저장
        for (int i = 0; i < lengthA; i++)
        {
            if (spawnTowers[i].towerLevel == level)
            {
                spawnTowersSale.Add(spawnTowers[i]);
            }
        }

        int lengthB = spawnTowersSale.Count;

        Debug.Log(lengthB);

        if (spawnTowersSale.Count > 0)
        {
            // 새 리스트와 원본 리스트의 타워를 비교하고 같으면 원본 리스트에서 삭제
            for (int i = 0; i < lengthB; i++)
            {
                SptTower saleT = spawnTowersSale[i];

                for (int j = 0; j < spawnTowers.Count; j++)
                {
                    if (saleT == spawnTowers[j])
                    {
                        // 판매금 저장
                        int saleGold = spawnTowers[j].saleGold;
                        // 타워 제거
                        SaleRemoveTower(spawnTowers[j]);
                        // 판매금 정산 로직
                        GetGoldToSale(saleGold);
                        break;
                    }
                }
            }
        }
    }
    public void OnAutoSaleLv1()
    {
        if (isAutoSaleLv1 == true)
        {
            isAutoSaleLv1 = false;

            AutoSaleLv1_Btn.GetComponent<Image>().sprite = autoSaleOff_img;
        }
        else
        {
            StartAutoSale(1);
            isAutoSaleLv1 = true;

            AutoSaleLv1_Btn.GetComponent<Image>().sprite = autoSaleOn_img;
        }
    }
    public void OnAutoSaleLv2()
    {
        if (isAutoSaleLv2 == true)
        {
            isAutoSaleLv2 = false;

            AutoSaleLv2_Btn.GetComponent<Image>().sprite = autoSaleOff_img;
        }
        else
        {
            StartAutoSale(2);
            isAutoSaleLv2 = true;

            AutoSaleLv2_Btn.GetComponent<Image>().sprite = autoSaleOn_img;
        }
    }
    public void OnAutoSaleLv3()
    {
        if (isAutoSaleLv3 == true)
        {
            isAutoSaleLv3 = false;

            AutoSaleLv3_Btn.GetComponent<Image>().sprite = autoSaleOff_img;
        }
        else
        {
            StartAutoSale(3);
            isAutoSaleLv3 = true;

            AutoSaleLv3_Btn.GetComponent<Image>().sprite = autoSaleOn_img;
        }
    }
    public void OnAutoSaleLv4()
    {
        if (isAutoSaleLv4 == true)
        {
            isAutoSaleLv4 = false;

            AutoSaleLv4_Btn.GetComponent<Image>().sprite = autoSaleOff_img;
        }
        else
        {
            StartAutoSale(4);
            isAutoSaleLv4 = true;

            AutoSaleLv4_Btn.GetComponent<Image>().sprite = autoSaleOn_img;
        }
    }
    #endregion

    #region CostAct
    public void GetGoldToEnemy(int get)
    {
        curGold += get;
        SptRecordManager.instance.GetGoldToEnemyRecord(get);
        UpdateGoldUI();
        ShowCostText($"+{get}", gold_TMP.rectTransform, Color.yellow);
    }
    public void GetGoldToSale(int get)
    {
        curGold += get;
        SptRecordManager.instance.GetGoldToSaleRecord(get);
        UpdateGoldUI();
        ShowCostText($"+{get}", gold_TMP.rectTransform, Color.yellow);
    }
    public void UseGold(int use)
    {
        curGold -= use;
        SptRecordManager.instance.UseGoldRecord(use);
        UpdateGoldUI();
        ShowCostText($"-{use}", gold_TMP.rectTransform, Color.darkRed);
    }
    public void GetCore(int get)
    {
        curCore += get;
        SptRecordManager.instance.GetCoreRecord(get);
        UpdateCoreUI();
        //StartCoroutine(ShowGambleCorePopupRoutine(get));
        ShowGambleCoreNotice(get);
        ShowTextLog(1, get);
        ShowCostText($"+{get}", core_TMP.rectTransform, Color.yellow);
    }
    public void UseCore(int use)
    {
        curCore -= use;
        SptRecordManager.instance.UseCoreRecord(use);
        UpdateCoreUI();
        ShowCostText($"-{use}", core_TMP.rectTransform, Color.darkRed);
    }
    public void GetBoost(int num, ETowerType type)
    {
        switch (type)
        {
            case ETowerType.Red:
                if (num == 1) redBoost1 = true;
                else redBoost2 = true;
                redEnhanceLevel += 10;
                ShowCostText($"Get", redBoostCheck1.GetComponentInParent<RectTransform>(), Color.red);

                ShowGambleBoostNotice(0);
                ShowTextLog(2, 0);
                break;

            case ETowerType.Blue:
                if (num == 1) blueBoost1 = true;
                else blueBoost2 = true;
                blueEnhanceLevel += 10;
                ShowCostText($"Get", blueBoostCheck1.GetComponentInParent<RectTransform>(), Color.blue);

                ShowGambleBoostNotice(1);
                ShowTextLog(2, 1);
                break;

            case ETowerType.Green:
                if (num == 1) greenBoost1 = true;
                else greenBoost2 = true;
                greenEnhanceLevel += 10;
                ShowCostText($"Get", greenBoostCheck1.GetComponentInParent<RectTransform>(), Color.green);

                ShowGambleBoostNotice(2);
                ShowTextLog(2, 2);
                break;
        }
        SptRecordManager.instance.GetBoostTypeRecord(type);
        UpdateBoostUI();
        SetEnhancePopup();
    }
    #endregion

    #region SettingUI
    public void OnSettingPopup()
    {
        Time.timeScale = 0f;
        settingPopup.OnDefenceSettingPopup();
    }
    public void OffSettingPopup()
    {
        Time.timeScale = 1f;
        settingPopup.OnClosePopup();
    }
    public void OnFinishGame()
    {
        OffSettingPopup();
        // spawner 내부 로직을 통해서 게임오버 처리
        SptGameManager.instance.spawner.aliveEnemyCount = 100;
    }
    #endregion

    #region FinishPopup
    public void OpenFinishPopup(bool isClear)
    {
        Time.timeScale = 0;
        finishPopup.SetActive(true);

        // 클리어 여부에 따라 이름 변경 및 도달 라운드 On/Off
        if (isClear) finishTitle_TMP.text = "+ Clear +";
        else finishTitle_TMP.text = "= Clear Fail =";

        // 도달 스테이지
        int curStage = SptGameManager.instance.spawner.stageID;
        goalWave_TMP.text = $"{curStage:00}";

        // 게임 클리어시 획득 재화
        int get = 0;
        if (isClear) get = 200;
        else
        {
            if (curStage > 60) get = 130;           // 60보스 클리어 획득재화
            else if (curStage > 50) get = 75;       // 50보스 클리어 획득재화
            else if (curStage > 40) get = 40;       // 40보스 클리어 획득재화
            else if (curStage > 30) get = 20;       // 30보스 클리어 획득재화
            else if (curStage > 20) get = 10;       // 20보스 클리어 획득재화
            else if (curStage > 10) get = 5;        // 10보스 클리어 획득재화
            else get = 0;                           // 10이하 스테이지 클리어 획득재화
        }

        getDia_TMP.text = $"{get}";
    }
    public void OnShowADs()
    {
        Debug.Log("OnShowADs");
        if (!SptDataManager.instance.isJoin) viewAds_Btn.GetComponent<LevelPlayRewardedOnly>().Show(() => GiveReward());
        else GiveReward();
    }

    public void GiveReward()
    {
        SptGameManager.instance.defenceUI.loadingPopup.SetActive(true);

        // 디버그 여부에 따라 리워드 분기
        Debug.Log("DefenceUIPlayReward");
        if (SptGameManager.instance.isRecord)
        {
            SptGooglePlayGameServices.instance.LoadADMutiRewardData();
        }
        else
        {
            SptGameManager.instance.LoadADMutiRewardDataNext();
        }
    }
    public void FinishPopupDiaMutiple()
    {
        adsGetDia_TMP.text = $"{int.Parse(getDia_TMP.text) * 2}";
        adsDiaPanel.SetActive(true);
    }

    public void CloseFinishPopup()
    {
        finishPopup.SetActive(false);
    }
    public void OnRestartGame()
    {
        // 씬을 재시작 == 게임 재시작
        SceneManager.LoadScene("ScnDefense", LoadSceneMode.Single);
    }
    public void OnExitGame()
    {
        // 메인메뉴로 나가기
        SceneManager.LoadScene("ScnMainMenu", LoadSceneMode.Single);
    }
    #endregion

    #region RecordUI
    public void OnRecordUI()
    {
        Time.timeScale = 0f;

        totalRecord.TotalRecordSet();
        DefenceRecordSet();

        recordPopup.SetActive(true);

        OnDefenceRecord();

        /*
        // 현재 타워 갯수
        int redLv1 = 0;
        int redLv2 = 0;
        int redLv3 = 0;
        int redLv4 = 0;
        int redLv5 = 0;
        int redLv6 = 0;
        int redLv7 = 0;
        int redLv8 = 0;
        int redLv9 = 0;

        int blueLv1 = 0;
        int blueLv2 = 0;
        int blueLv3 = 0;
        int blueLv4 = 0;
        int blueLv5 = 0;
        int blueLv6 = 0;
        int blueLv7 = 0;
        int blueLv8 = 0;
        int blueLv9 = 0;

        int greenLv1 = 0;
        int greenLv2 = 0;
        int greenLv3 = 0;
        int greenLv4 = 0;
        int greenLv5 = 0;
        int greenLv6 = 0;
        int greenLv7 = 0;
        int greenLv8 = 0;
        int greenLv9 = 0;

        // 스폰 타워 유무 확인
        if (spawnTowers.Count <= 0)
        {
            // 빈 창 띄우기
            nothingSpawnPanel.SetActive(true);
            // 없으면 레코드 화면 숨기기
            curTowerRecord.SetActive(false);
        }
        else
        {
            // 빈 창 숨기기
            nothingSpawnPanel.SetActive(false);
            // 있으면 화면 보여주기
            curTowerRecord.SetActive(true);

            // 타워 카운팅
            foreach (SptTower tower in spawnTowers)
            {
                if(tower.towerType == TowerType.Red)
                {
                    switch (tower.towerLevel)
                    {
                        case 1: redLv1++; break;
                        case 2: redLv2++; break;
                        case 3: redLv3++; break;
                        case 4: redLv4++; break;
                        case 5: redLv5++; break;
                        case 6: redLv6++; break;
                        case 7: redLv7++; break;
                        case 8: redLv8++; break;
                        case 9: redLv9++; break;
                    }
                }
                else if (tower.towerType == TowerType.Blue)
                {
                    switch (tower.towerLevel)
                    {
                        case 1: blueLv1++; break;
                        case 2: blueLv2++; break;
                        case 3: blueLv3++; break;
                        case 4: blueLv4++; break;
                        case 5: blueLv5++; break;
                        case 6: blueLv6++; break;
                        case 7: blueLv7++; break;
                        case 8: blueLv8++; break;
                        case 9: blueLv9++; break;
                    }
                }
                else if (tower.towerType == TowerType.Green)
                {
                    switch (tower.towerLevel)
                    {
                        case 1: greenLv1++; break;
                        case 2: greenLv2++; break;
                        case 3: greenLv3++; break;
                        case 4: greenLv4++; break;
                        case 5: greenLv5++; break;
                        case 6: greenLv6++; break;
                        case 7: greenLv7++; break;
                        case 8: greenLv8++; break;
                        case 9: greenLv9++; break;
                    }
                }
            }

            // 레드 타워 카운팅 적용 및 카운트 == 0이면 비활성화
            if (redLv1 > 0) redLv1_TMP.text = $"x{redLv1}"; else redLv1_TMP.transform.parent.gameObject.SetActive(false);
            if (redLv2 > 0) redLv2_TMP.text = $"x{redLv2}"; else redLv2_TMP.transform.parent.gameObject.SetActive(false);
            if (redLv3 > 0) redLv3_TMP.text = $"x{redLv3}"; else redLv3_TMP.transform.parent.gameObject.SetActive(false);
            if (redLv4 > 0) redLv4_TMP.text = $"x{redLv4}"; else redLv4_TMP.transform.parent.gameObject.SetActive(false);
            if (redLv5 > 0) redLv5_TMP.text = $"x{redLv5}"; else redLv5_TMP.transform.parent.gameObject.SetActive(false);
            if (redLv6 > 0) redLv6_TMP.text = $"x{redLv6}"; else redLv6_TMP.transform.parent.gameObject.SetActive(false);
            if (redLv7 > 0) redLv7_TMP.text = $"x{redLv7}"; else redLv7_TMP.transform.parent.gameObject.SetActive(false);
            if (redLv8 > 0) redLv8_TMP.text = $"x{redLv8}"; else redLv8_TMP.transform.parent.gameObject.SetActive(false);
            if (redLv9 > 0) redLv9_TMP.text = $"x{redLv9}"; else redLv9_TMP.transform.parent.gameObject.SetActive(false);

            // 블루 타워 카운팅 적용 및 카운트 == 0이면 비활성화
            if (blueLv1 > 0) blueLv1_TMP.text = $"x{blueLv1}"; else blueLv1_TMP.transform.parent.gameObject.SetActive(false);
            if (blueLv2 > 0) blueLv2_TMP.text = $"x{blueLv2}"; else blueLv2_TMP.transform.parent.gameObject.SetActive(false);
            if (blueLv3 > 0) blueLv3_TMP.text = $"x{blueLv3}"; else blueLv3_TMP.transform.parent.gameObject.SetActive(false);
            if (blueLv4 > 0) blueLv4_TMP.text = $"x{blueLv4}"; else blueLv4_TMP.transform.parent.gameObject.SetActive(false);
            if (blueLv5 > 0) blueLv5_TMP.text = $"x{blueLv5}"; else blueLv5_TMP.transform.parent.gameObject.SetActive(false);
            if (blueLv6 > 0) blueLv6_TMP.text = $"x{blueLv6}"; else blueLv6_TMP.transform.parent.gameObject.SetActive(false);
            if (blueLv7 > 0) blueLv7_TMP.text = $"x{blueLv7}"; else blueLv7_TMP.transform.parent.gameObject.SetActive(false);
            if (blueLv8 > 0) blueLv8_TMP.text = $"x{blueLv8}"; else blueLv8_TMP.transform.parent.gameObject.SetActive(false);
            if (blueLv9 > 0) blueLv9_TMP.text = $"x{blueLv9}"; else blueLv9_TMP.transform.parent.gameObject.SetActive(false);

            // 그린 타워 카운팅 적용 및 카운트 == 0이면 비활성화
            if (greenLv1 > 0) greenLv1_TMP.text = $"x{greenLv1}"; else greenLv1_TMP.transform.parent.gameObject.SetActive(false);
            if (greenLv2 > 0) greenLv2_TMP.text = $"x{greenLv2}"; else greenLv2_TMP.transform.parent.gameObject.SetActive(false);
            if (greenLv3 > 0) greenLv3_TMP.text = $"x{greenLv3}"; else greenLv3_TMP.transform.parent.gameObject.SetActive(false);
            if (greenLv4 > 0) greenLv4_TMP.text = $"x{greenLv4}"; else greenLv4_TMP.transform.parent.gameObject.SetActive(false);
            if (greenLv5 > 0) greenLv5_TMP.text = $"x{greenLv5}"; else greenLv5_TMP.transform.parent.gameObject.SetActive(false);
            if (greenLv6 > 0) greenLv6_TMP.text = $"x{greenLv6}"; else greenLv6_TMP.transform.parent.gameObject.SetActive(false);
            if (greenLv7 > 0) greenLv7_TMP.text = $"x{greenLv7}"; else greenLv7_TMP.transform.parent.gameObject.SetActive(false);
            if (greenLv8 > 0) greenLv8_TMP.text = $"x{greenLv8}"; else greenLv8_TMP.transform.parent.gameObject.SetActive(false);
            if (greenLv9 > 0) greenLv9_TMP.text = $"x{greenLv9}"; else greenLv9_TMP.transform.parent.gameObject.SetActive(false);

        }
        */
    }
    public void OnTotalRecord()
    {
        totalRecord.gameObject.SetActive(true);
        defenceRecord.gameObject.SetActive(false);
    }
    public void OnDefenceRecord()
    {
        totalRecord.gameObject.SetActive(false);
        defenceRecord.gameObject.SetActive(true);
    }
    public void OnCloseRecordUI()
    {
        Time.timeScale = 1f;
        recordPopup.SetActive(false);
    }

    public void DefenceRecordSet()
    {
        SptRecordData data = SptRecordManager.instance.ReadCurrentRecordData();

        // 타워 뽑기 횟수
        gambleTowerCount_TMP.text = $"{data.totalGambleTowerCount}";

        // 레드타워 스폰 횟수
        if (data.totalRedLv1SpawnCount > 0) redLv1SpawnCount_TMP.text = $"x{data.totalRedLv1SpawnCount}"; else redLv1SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalRedLv2SpawnCount > 0) redLv2SpawnCount_TMP.text = $"x{data.totalRedLv2SpawnCount}"; else redLv2SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalRedLv3SpawnCount > 0) redLv3SpawnCount_TMP.text = $"x{data.totalRedLv3SpawnCount}"; else redLv3SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalRedLv4SpawnCount > 0) redLv4SpawnCount_TMP.text = $"x{data.totalRedLv4SpawnCount}"; else redLv4SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalRedLv5SpawnCount > 0) redLv5SpawnCount_TMP.text = $"x{data.totalRedLv5SpawnCount}"; else redLv5SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalRedLv6SpawnCount > 0) redLv6SpawnCount_TMP.text = $"x{data.totalRedLv6SpawnCount}"; else redLv6SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalRedLv7SpawnCount > 0) redLv7SpawnCount_TMP.text = $"x{data.totalRedLv7SpawnCount}"; else redLv7SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalRedLv8SpawnCount > 0) redLv8SpawnCount_TMP.text = $"x{data.totalRedLv8SpawnCount}"; else redLv8SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalRedLv9SpawnCount > 0) redLv9SpawnCount_TMP.text = $"x{data.totalRedLv9SpawnCount}"; else redLv9SpawnCount_TMP.transform.parent.gameObject.SetActive(false);

        // 블루타워 스폰 횟수
        if (data.totalBlueLv1SpawnCount > 0) blueLv1SpawnCount_TMP.text = $"x{data.totalBlueLv1SpawnCount}"; else blueLv1SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalBlueLv2SpawnCount > 0) blueLv2SpawnCount_TMP.text = $"x{data.totalBlueLv2SpawnCount}"; else blueLv2SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalBlueLv3SpawnCount > 0) blueLv3SpawnCount_TMP.text = $"x{data.totalBlueLv3SpawnCount}"; else blueLv3SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalBlueLv4SpawnCount > 0) blueLv4SpawnCount_TMP.text = $"x{data.totalBlueLv4SpawnCount}"; else blueLv4SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalBlueLv5SpawnCount > 0) blueLv5SpawnCount_TMP.text = $"x{data.totalBlueLv5SpawnCount}"; else blueLv5SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalBlueLv6SpawnCount > 0) blueLv6SpawnCount_TMP.text = $"x{data.totalBlueLv6SpawnCount}"; else blueLv6SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalBlueLv7SpawnCount > 0) blueLv7SpawnCount_TMP.text = $"x{data.totalBlueLv7SpawnCount}"; else blueLv7SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalBlueLv8SpawnCount > 0) blueLv8SpawnCount_TMP.text = $"x{data.totalBlueLv8SpawnCount}"; else blueLv8SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalBlueLv9SpawnCount > 0) blueLv9SpawnCount_TMP.text = $"x{data.totalBlueLv9SpawnCount}"; else blueLv9SpawnCount_TMP.transform.parent.gameObject.SetActive(false);

        // 그린타워 스폰 횟수
        if (data.totalGreenLv1SpawnCount > 0) greenLv1SpawnCount_TMP.text = $"x{data.totalGreenLv1SpawnCount}"; else greenLv1SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalGreenLv2SpawnCount > 0) greenLv2SpawnCount_TMP.text = $"x{data.totalGreenLv2SpawnCount}"; else greenLv2SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalGreenLv3SpawnCount > 0) greenLv3SpawnCount_TMP.text = $"x{data.totalGreenLv3SpawnCount}"; else greenLv3SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalGreenLv4SpawnCount > 0) greenLv4SpawnCount_TMP.text = $"x{data.totalGreenLv4SpawnCount}"; else greenLv4SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalGreenLv5SpawnCount > 0) greenLv5SpawnCount_TMP.text = $"x{data.totalGreenLv5SpawnCount}"; else greenLv5SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalGreenLv6SpawnCount > 0) greenLv6SpawnCount_TMP.text = $"x{data.totalGreenLv6SpawnCount}"; else greenLv6SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalGreenLv7SpawnCount > 0) greenLv7SpawnCount_TMP.text = $"x{data.totalGreenLv7SpawnCount}"; else greenLv7SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalGreenLv8SpawnCount > 0) greenLv8SpawnCount_TMP.text = $"x{data.totalGreenLv8SpawnCount}"; else greenLv8SpawnCount_TMP.transform.parent.gameObject.SetActive(false);
        if (data.totalGreenLv9SpawnCount > 0) greenLv9SpawnCount_TMP.text = $"x{data.totalGreenLv9SpawnCount}"; else greenLv9SpawnCount_TMP.transform.parent.gameObject.SetActive(false);

        // 타입별 강화 레벨
        redEnhanceLv_TMP.text = $"Lv{redEnhanceLevel}";
        blueEnhanceLv_TMP.text = $"Lv{blueEnhanceLevel}";
        greenEnhanceLv_TMP.text = $"Lv{greenEnhanceLevel}";

        // 골드 관련 누적 횟수
        getGold_TMP.text = $"{data.totalGetGoldEnemy + data.totalGetGoldSaleTower}G";
        useGold_TMP.text = $"{data.totalUseGold}G";

        // 부스트 관련 누적 횟수
        tryBoostCount_TMP.text = $"{data.totalTryBoostCount}";
        getRedBoostCount_TMP.text = $"{data.totalGetRedBoostCount}";
        getBlueBoostCount_TMP.text = $"{data.totalGetBlueBoostCount}";
        getGreenBoostCount_TMP.text = $"{data.totalGetGreenBoostCount}";

        // 코어 관련 누적 횟수
        tryCoreCount_TMP.text = $"{data.totalTryCoreCount}";
        getCoreCount_TMP.text = $"{data.totalGetCoreCount}";
        useCoreCount_TMP.text = $"{data.totalUseCoreCount}";
    }
    #endregion

    #region DefenceUI
    public void PlaySpeedUp()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 3f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void SetStartUI()
    {
        UpdateRoundUI(0);
        UpdateGoldUI();
        UpdateCoreUI();
        UpdateBoostUI();
        UpdateSaleButtonUI();
        UpdateEnemyUI(0);

        gambleTowerCost_TMP.text = $"Gamble\n{gambleTowerCost}";
        gambleCoreCost_TMP.text = $"Core\nGamble\n{gambleCoreCost}";
        gambleBoost1Cost_TMP.text = $"Boost1\nGamble\n{gambleBoostCost1}";
        gambleBoost2Cost_TMP.text = $"Boost2\nGamble\n{gambleBoostCost2}";
    }

    public void UpdateClock(float time)
    {
        int minute = (int)(time / 60);
        int second = (int)(time % 60);
        timer_TMP.text = $"{minute:00}:{second:00}";
    }
    public void UpdateEnemyUI(int enemies)
    {
        enemies_TMP.text = $"{enemies:00}";
    }
    public void UpdateRoundUI(int round)
    {
        round_TMP.text = $"Round {round:00}";
    }
    public void UpdateGoldUI()
    {
        gold_TMP.text = $"{curGold}";
    }
    public void UpdateCoreUI()
    {
        core_TMP.text = $"{curCore}";
    }
    public void UpdateBoostUI()
    {
        redBoostCheck1.SetActive(redBoost1);
        blueBoostCheck1.SetActive(blueBoost1);
        greenBoostCheck1.SetActive(greenBoost1);
        redBoostCheck2.SetActive(redBoost2);
        blueBoostCheck2.SetActive(blueBoost2);
        greenBoostCheck2.SetActive(greenBoost2);
    }

    public void GameStart()
    {
        itemCountData = SptSaveManager.instance.itemDatas;
        ItemApplication();

        SpawnPositionsSet();
    }
    public void ItemApplication()
    {
        gambleTowerCost = (int)(gambleTowerBaseCost / (1 + (itemCountData.item_000 * gambleCostDiscount / 100f)));
        gambleCoreCost = (int)(gambleCoreBaseCost / (1 + (itemCountData.item_001 * gambleCostDiscount / 100f)));
        gambleBoostCost1 = (int)(gambleBoostBaseCost1 / (1 + (itemCountData.item_002 * gambleCostDiscount / 100f)));
        gambleBoostCost2 = (int)(gambleBoostBaseCost2 / (1 + (itemCountData.item_002 * gambleCostDiscount / 100f)));

        curGold = 150 + (itemCountData.item_200 * boostGold);
        curCore = itemCountData.item_100;
    }
    public void SpawnPositionsSet()
    {
        if (slotList.Count > 0 && spawnPositions.Count <= 0)
        {
            foreach (var slot in slotList)
            {
                spawnPositions.Add(slot.slotID, slot.transform.position);
            }
            Debug.Log($"spawnPositions Count : {spawnPositions.Count}");
        }
    }
    public void GameFinish(bool isClear)
    {
        AllPopupClose();
        OpenFinishPopup(isClear);
    }
    public void DataRecording()
    {
        SptRecordManager.instance.EnhanceTypeRecord(ETowerType.Red, redEnhanceLevel);
        SptRecordManager.instance.EnhanceTypeRecord(ETowerType.Blue, blueEnhanceLevel);
        SptRecordManager.instance.EnhanceTypeRecord(ETowerType.Green, greenEnhanceLevel);
    }
    public void AllPopupClose()
    {
        // 선택창이 켜져있으면 끄기
        if (towerSelecPanel.activeSelf) TowerUnselec();
        // 이동창이 켜져있으면 끄기
        if (towerMovePopup.activeSelf) OnCloseMove();
        // 정보창이 켜져있으면 끄기
        if (towerInfoPopup.activeSelf) OnExitTowerInfo();
        // 강화창이 켜져있으면 끄기
        if (enhancePopup.activeSelf) OffEnhancePopup();
        // 판매창이 켜져있으면 끄기
        if (salePopup.activeSelf) OffSalePopup();
    }

    public void ShowCostText(string text, RectTransform targetPos, Color color)
    {
        // 팝업 생성
        GameObject popup = costPopupPrf;
        popup.GetComponent<SptCostPopup>().Initialize
            (text, color, targetPos.position, popupText.transform);
    }
    public void ShowDamageText(string text, RectTransform targetUI, Color color)
    {
        // 팝업 생성
        GameObject popup = damagePopupPrf;
        popup.GetComponent<SptDamagePopup>().Initialize
            (text, color, targetUI.position, popupText.transform);
    }

    public void AddNoticePopup(int popupType, GameObject popupPrf, int num) // popupType : 0=round/1=tower/2=core/3=boost/4=Game
    {
        // noticePopups에 추가
        switch (popupType)
        {
            case 0:
                GameObject roundPopup = popupPrf.GetComponent<SptRoundNotice>().Initialize(num, noticePopups.transform);
                roundNoticeList.Add(roundPopup);
                break;
            case 1:
                GameObject towerPopup = popupPrf.GetComponent<SptTowerNotice>().Initialize(num, noticePopups.transform);
                noticeList.Add(towerPopup);
                break;
            case 2:
                GameObject corePopup = popupPrf.GetComponent<SptCoreNotice>().Initialize(num, noticePopups.transform);
                noticeList.Add(corePopup);
                break;
            case 3:
                GameObject boostPopup = popupPrf.GetComponent<SptBoostNotice>().Initialize(num, noticePopups.transform);
                noticeList.Add(boostPopup);
                break;
            case 4:
                GameObject gamePopup = popupPrf.GetComponent<SptGameNotice>().Initialize(num, noticePopups.transform);
                //roundNoticeList.Add(gamePopup);
                break;
        }

        if (!isPopup)
        {
            NextNoticeCheck();
        }
    }
    public void ShowNoticePopup()
    {
        isPopup = true;
        noticeList[0].SetActive(true);
    }
    public void ShowRoundNoticePopup()
    {
        isPopup = true;
        roundNoticeList[0].SetActive(true);
    }
    public void NextNoticeCheck()
    {
        if (noticeList.Count > 0) ShowNoticePopup();
        if (roundNoticeList.Count > 0) ShowRoundNoticePopup();
    }
    public void ShowRoundNotice(int stageID)
    {
        AddNoticePopup(0, roundPopupPrf, stageID);
    }
    public void ShowGambleTowerNotice(int towerLevel)
    {
        AddNoticePopup(1, gambleTowerPopupPrf, towerLevel);
    }
    public void ShowGambleCoreNotice(int get)
    {
        AddNoticePopup(2, gambleCorePopupPrf, get);
    }
    public void ShowGambleBoostNotice(int boostType)
    {
        // 0=Red,1=Blue,2=Green
        AddNoticePopup(3, gambleBoostPopupPrf, boostType);
    }
    public void ShowGameNotice(int gameState)
    {
        // -1:fail,0=start,1=clear
        AddNoticePopup(4, gameNoticePopupPrf, gameState);
    }

    public void ShowTextLog(int popupType, int num)
    {
        // log 생성
        // Type -> -1=GambleFail/0=tower/1=core/2=boost/3=NoCost/4=dontLvUp/5=boostCollectFin
        // Num -> -1=None/TowerLv/CoreAmount/BoostType
        logManager.AddLogPopup(popupType, num);
    }
    #endregion
}