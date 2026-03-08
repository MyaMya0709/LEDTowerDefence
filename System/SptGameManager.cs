using UnityEngine;
using UnityEngine.SceneManagement;

public class SptGameManager : SptSingleton<SptGameManager>
{
    public SptDefenceUI defenceUI;
    public SptMainUI mainUI;
    public SptSpawner spawner;

    public SptDebug debug;
    public bool isRecord = true;

    public bool isFinish = false;

    protected override void EventListen()
    {
        Debug.Log("event listen");
        // 씬 로드 이벤트 구독
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected override void EventCencle()
    {
        Debug.Log("event destroy");
        // 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬 로드 시 호출되는 콜백
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("로드된 씬: " + scene.name);

        switch (scene.name)
        {
            case "ScnMainMenu":
                StartGameRoutine();
                break;

            case "ScnDefense":
                Time.timeScale = 1f;
                StartDefenceRoutine();
                break;
        }
    }

    public void StartGameRoutine()
    {
        mainUI = FindFirstObjectByType<SptMainUI>();

        // json 데이터 로드
        SptSaveManager.instance.LoadLocalData();
        // 다이아, 아이템, 페키지 데이터 로드
        SptDataManager.instance.DataLoad();
        // 기록 데이터 로드 및 기록 시작
        SptRecordManager.instance.RecordStart();
        // 음향 데이터 세팅
        SptSoundManager.instance.SettingDatasLoad();

        // 개발 테스트용 코드
        if (debug != null && debug.gameObject.activeSelf)
        {
            debug.MainSet();
            isRecord = false;
        }
        else isRecord = true;

        // UISet
        mainUI.OnBattleUI();

        // bgm 재생
        SptSoundManager.instance.StopSFX();
        SptSoundManager.instance.PlayBGM(EBgm.Main);
    }

    public void StartDefenceRoutine()
    {
        defenceUI = FindFirstObjectByType<SptDefenceUI>();
        spawner = FindFirstObjectByType<SptSpawner>();

        // bgm 재생
        SptSoundManager.instance.StopSFX();
        SptSoundManager.instance.PlayBGM(EBgm.Defence);

        if(isRecord) SptGooglePlayGameServices.instance.LoadDefenceStartData();
        else LoadDefenceStartNext();
    }

    #region 서버 데이터 송수신 이후 로직
    public void LoadDefenceStartNext()
    {
        Debug.Log("LoadDefenceStartNext");

        //if (SptRecordManager.instance.data == null)
        //{
        //    // json 데이터 로드
        //    SptSaveManager.instance.LoadLocalData();
        //    // 다이아, 아이템 데이터 로드
        //    SptDataManager.instance.DataLoad();
        //    // 기록 데이터 로드 및 기록 시작
        //    SptRecordManager.instance.RecordStart();
        //}

        // json 데이터 로드
        SptSaveManager.instance.LoadLocalData();
        // 다이아, 아이템 데이터 로드
        SptDataManager.instance.DataLoad();
        // 기록 데이터 로드 및 기록 시작
        SptRecordManager.instance.RecordStart();

        // 게임 시작 알림 및 UI 세팅
        defenceUI.ShowGameNotice(0);
        defenceUI.SetStartUI();

        // 타워 스폰 위치 세팅
        defenceUI.GameStart();

        // 스포너 게임 시작
        spawner.StartSetting();


        // 개발 테스트용 코드
        if (debug != null && debug.gameObject.activeSelf)
        {
            debug.DefenceSet();
            isRecord = false;
        }
        else isRecord = true;


        if (isRecord) SptGooglePlayGameServices.instance.SaveDefenceStartData();
        else SaveDefenceStartNext();
    }
    public void SaveDefenceStartNext()
    {

    }

    public void LoadDefenceFinNext()
    {
        Debug.Log("LoadDefenceFinNext");
        if (!isFinish) return;
        Debug.Log("isGameNoticeFin");

        // 유료재화 획득
        SptDataManager.instance.GetDiaToDefence(spawner.stageID, spawner.isGameClear);
        // 강화 레벨 저장
        defenceUI.DataRecording();
        // 기록 중지 및 저장
        SptRecordManager.instance.RecordFinish();

        // BGM 중지, SFX재생
        SptSoundManager.instance.StopBGM();

        SptSoundManager.instance.PlaySFX(ESfx.Result);

        // 게임 종료 팝업 열기
        defenceUI.GameFinish(spawner.isGameClear);

        // 데이터 서버 업로드
        if (isRecord) SptGooglePlayGameServices.instance.SaveDefenceFinData();
        else SaveDefenceFinNext();
    }
    public void SaveDefenceFinNext()
    {
        Debug.Log("SaveDefenceFinNext");
        defenceUI.exitGame_Btn.interactable = true;
        defenceUI.viewAds_Btn.interactable = true;
    }

    public void LoadItemGambleNext()
    {
        // 데이터 세팅
        SptDataManager.instance.DataLoad();
        mainUI.gambleUI.loadingPopup.OnClose();

        // 이이템 뽑기 시작
        mainUI.gambleUI.GambleStart();
    }
    public void SaveItemGambleNext()
    {
        // TODO: 서버 업로드 후에 실행할 로직 있으면 작성 
        mainUI.SetBasicUI();

        mainUI.gambleUI.ResultPopupOpen();
    }

    public void LoadPackageDataNext()
    {
        // UI 열기
        mainUI.OpenStoreUI();
    }
    public void SavePackageDataNext()
    {

    }

    public void LoadPurchaseDiaNext()
    {
        // 아이템,다이아,패키지 업데이트
        mainUI.storeUI.payPopup.packageIAP.PurchasePackageSetting();

        // UI 업데이트
        mainUI.storeUI.OnDiaStore();
        mainUI.SetBasicUI();
        mainUI.storeUI.payPopup.packageIAP.successPopup.OnClose();

        // 데이터 서버 업로드
        if (isRecord) SptGooglePlayGameServices.instance.SavePurchaseDiaData();
        //else SavePurchaseDiaNext();
    }
    public void SavePurchaseDiaNext()
    {
        // 데이터 서버 저장 후 실행할 로직
        mainUI.storeUI.payPopup.packageIAP.OpenDiaResultPopup();

        // 데이터 값 초기화
        mainUI.storeUI.payPopup.packageIAP.purchasePackageData = null;
    }

    public void LoadPurchasePackageNext()
    {
        // 아이템,다이아,패키지 업데이트
        mainUI.storeUI.payPopup.packageIAP.PurchasePackageSetting();

        // UI 업데이트
        mainUI.storeUI.OnItemStore();
        mainUI.SetBasicUI();
        mainUI.storeUI.payPopup.packageIAP.successPopup.OnClose();

        // 데이터 서버 업로드
        if (isRecord) SptGooglePlayGameServices.instance.SavePurchasePackageData();
        //else SavePurchasePackageNext();
    }
    public void SavePurchasePackageNext()
    {
        // 데이터 서버 저장 후 실행할 로직
        mainUI.storeUI.payPopup.packageIAP.OpenPackageResultPopup();

        // 데이터 값 초기화
        mainUI.storeUI.payPopup.packageIAP.purchasePackageData = null;
    }

    public void LoadADsDataNext()
    {
        SptDataManager.instance.JoinADsContract();

        if (isRecord) SptGooglePlayGameServices.instance.SaveADsDataToCloud();
        else SaveADsDataNext();
    }
    public void SaveADsDataNext()
    {
        mainUI.loadingUI.SetActive(false);
        mainUI.battleUI.ADFreeBtnOnOff();
    }

    public void LoadADDiaRewardDataNext()
    {
        // ads data
        SptDataManager.instance.ADsDataLoad();
        SptDataManager.instance.CountingADsView(true);

        // dia data
        int rewardDia = 20;
        SptDataManager.instance.DiaDataLoad();
        SptDataManager.instance.GetDiaToPurchase(rewardDia);

        // 데이터 서버 업로드
        if (isRecord) SptGooglePlayGameServices.instance.SaveAdDiaRewardData();
        else SaveADDiaRewardDataNext();
    }
    public void SaveADDiaRewardDataNext()
    {
        mainUI.loadingUI.SetActive(false);
        mainUI.gambleUI.resultPopup.SetGetDia(20);
        mainUI.SetBasicUI();
    }

    public void LoadADMutiRewardDataNext()
    {
        // ads data
        SptDataManager.instance.ADsDataLoad();
        SptDataManager.instance.CountingADsView(false);

        // dia data
        SptDataManager.instance.DiaDataLoad();
        SptDataManager.instance.GetDiaToDefence(spawner.stageID, spawner.isGameClear);

        // 데이터 서버 업로드
        if (isRecord) SptGooglePlayGameServices.instance.SaveAdMutiRewardData();
        else SaveADMutiRewardDataNext();
    }
    public void SaveADMutiRewardDataNext()
    {
        Debug.Log($"defenceUI == null? {defenceUI == null}");
        Debug.Log($"loadingPopup == null? {defenceUI.loadingPopup == null}");
        Debug.Log($"viewAds_Btn == null? {defenceUI.viewAds_Btn == null}");
        // UI Set
        defenceUI.loadingPopup.SetActive(false);
        defenceUI.viewAds_Btn.gameObject.SetActive(false);
        defenceUI.FinishPopupDiaMutiple();
    }
    #endregion
}