using System;
using UnityEngine;
using Unity.Services.LevelPlay;

public class LevelPlayRewardedOnly : MonoBehaviour
{
    [Header("LevelPlay")]
    [SerializeField] private string appKey = "23f394d5d";

    [Header("Rewarded Ad Unit Id (Dashboard)")]
    [SerializeField] private string rewardedAdUnitId = "ut09uum8v113hf65";

    private LevelPlayRewardedAd rewarded;
    private bool initialized;

    private Action pendingReward; // 보상 지급 콜백

    private void Start()
    {
        // Init 이벤트는 Init 전에 연결
        LevelPlay.OnInitSuccess += OnInitSuccess;
        LevelPlay.OnInitFailed += OnInitFailed;

        LevelPlay.Init(appKey);
    }

    private void OnInitSuccess(LevelPlayConfiguration config)
    {
        initialized = true;

        rewarded = new LevelPlayRewardedAd(rewardedAdUnitId);

        rewarded.OnAdLoaded += info => { /* 로드 완료 */Debug.Log("LoadComplete"); };
        rewarded.OnAdLoadFailed += err => Debug.LogWarning($"[RV] LoadFailed: {err}");
        rewarded.OnAdDisplayFailed += (info, err) => Debug.LogWarning($"[RV] DisplayFailed: {err}");

        // 보상 지급 타이밍(여기에서만 보상 지급)
        rewarded.OnAdRewarded += (reward, info) =>
        {
            Debug.Log("AddReward");
            pendingReward?.Invoke();
            pendingReward = null;
        };

        // 닫히면 다음 광고 미리 로드
        rewarded.OnAdClosed += info =>
        {
            rewarded.LoadAd();
        };

        // 첫 로드
        rewarded.LoadAd();
    }

    private void OnInitFailed(LevelPlayInitError err)
    {
        initialized = false;
        Debug.LogError($"[LevelPlay] Init Failed: {err}");
    }

    public bool IsReady()
    {
        return initialized && rewarded != null && rewarded.IsAdReady();
    }

    /// <summary>
    /// 보상형 광고 보여주기. 성공적으로 "Rewarded" 콜백이 올 때만 onReward 실행.
    /// </summary>
    public void Show(Action onReward)
    {
        Debug.Log("ADShow");
        if (!initialized || rewarded == null)
            return;


        if (rewarded.IsAdReady())
        {
            pendingReward = onReward;
            rewarded.ShowAd();
        }
        else
        {
            // 준비 안 됐으면 로드만 걸고 보상 콜백은 유지(선택)
            rewarded.LoadAd();
            // 원하면 여기서 pendingReward = null; 로 "준비 안되면 보상 예약 안함" 정책으로 바꿔도 됨.
        }
    }

    private void OnDestroy()
    {
        LevelPlay.OnInitSuccess -= OnInitSuccess;
        LevelPlay.OnInitFailed -= OnInitFailed;

        rewarded?.DestroyAd();
    }
}


/*

using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.Services.Core;            // Unity Services 초기화용
using Unity.Services.LevelPlay;
using UnityEngine;

public class SptRewardAds : MonoBehaviour
{
    bool isAdFree = false;
    private LevelPlayRewardedAd rewardedAd;
    private readonly string adUnitId = "ut09uum8v113hf65"; // 네 광고유닛 ID
    bool isMultiReward = false;

    public void StartAds(bool isMultiReward)
    {
        if(!isAdFree)
        // 코루틴 대신 async 실행으로 초기화 보장
        {
            this.isMultiReward = isMultiReward;
            _ = InitializeAndLoad(); // fire-and-forget (오류는 내부에서 처리)
        }
        else
        {
            Debug.Log("PlayReward");
            if (!isMultiReward && SptGameManager.instance.isRecord)
            {
                SptGameManager.instance.mainUI.loadingUI.SetActive(true);
                SptGooglePlayGameServices.instance.LoadADDiaRewardData();
            }
            else if (!isMultiReward && !SptGameManager.instance.isRecord)
            {
                SptGameManager.instance.LoadADDiaRewardDataNext();
            }
            else if(isMultiReward && SptGameManager.instance.isRecord)
            {
                SptGameManager.instance.defenceUI.loadingPopup.SetActive(true);
                SptGooglePlayGameServices.instance.LoadADMutiRewardData();
            }
            else if (isMultiReward && !SptGameManager.instance.isRecord)
            {
                SptGameManager.instance.LoadADMutiRewardDataNext();
            }
        }
    }

    // UnityServices 초기화 후 광고 생성/로드
    private async Task InitializeAndLoad()
    {
        // 1. Unity Services 초기화
        await UnityServices.InitializeAsync();
        Debug.Log("[SptRewardAds] UnityServices initialized");

        // 2. 광고 객체 생성 및 이벤트 등록
        if (rewardedAd == null)
        {
            rewardedAd = new LevelPlayRewardedAd(adUnitId);
            rewardedAd.OnAdLoaded += RewardedOnAdLoadedEvent;
            rewardedAd.OnAdLoadFailed += RewardedOnAdLoadFailedEvent;
            rewardedAd.OnAdDisplayed += RewardedOnAdDisplayedEvent;
            rewardedAd.OnAdDisplayFailed += RewardedOnAdDisplayFailedEvent;
            rewardedAd.OnAdClicked += RewardedOnAdClickedEvent;
            rewardedAd.OnAdClosed += RewardedOnAdClosedEvent;
            rewardedAd.OnAdRewarded += RewardedOnAdRewarded;
            rewardedAd.OnAdInfoChanged += RewardedOnAdInfoChangedEvent;
        }

        // 3. 광고 로드
        rewardedAd.LoadAd();
        Debug.Log("[SptRewardAds] LoadAd called after initialization");
    }

    // 이벤트 핸들러
    void RewardedOnAdLoadedEvent(LevelPlayAdInfo adInfo) { ShowRewardedAd(); }
    void RewardedOnAdLoadFailedEvent(LevelPlayAdError error) { Debug.LogError("[SptRewardAds] Load Failed: " + error); }
    void RewardedOnAdClickedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdDisplayedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdDisplayFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error) { Debug.LogError("[SptRewardAds] DisplayFailed: " + error); }
    void RewardedOnAdClosedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdRewarded(LevelPlayAdInfo adInfo, LevelPlayReward adReward)
    {
        Debug.Log("PlayReward");
        if (!isMultiReward && SptGameManager.instance.isRecord)
        {
            SptGameManager.instance.mainUI.loadingUI.SetActive(true);
            SptGooglePlayGameServices.instance.LoadADDiaRewardData();
        }
        else if (!isMultiReward && !SptGameManager.instance.isRecord)
        {
            SptGameManager.instance.LoadADDiaRewardDataNext();
        }
        else if (isMultiReward && SptGameManager.instance.isRecord)
        {
            SptGameManager.instance.defenceUI.loadingPopup.SetActive(true);
            SptGooglePlayGameServices.instance.LoadADMutiRewardData();
        }
        else if (isMultiReward && !SptGameManager.instance.isRecord)
        {
            SptGameManager.instance.LoadADMutiRewardDataNext();
        }
    }
    void RewardedOnAdInfoChangedEvent(LevelPlayAdInfo adInfo) { }
    private void OnDisable()
    {
        if (rewardedAd != null)
        {
            rewardedAd.OnAdLoaded -= RewardedOnAdLoadedEvent;
            rewardedAd.OnAdLoadFailed -= RewardedOnAdLoadFailedEvent;
            rewardedAd.OnAdDisplayed -= RewardedOnAdDisplayedEvent;
            rewardedAd.OnAdDisplayFailed -= RewardedOnAdDisplayFailedEvent;
            rewardedAd.OnAdClicked -= RewardedOnAdClickedEvent;
            rewardedAd.OnAdClosed -= RewardedOnAdClosedEvent;
            rewardedAd.OnAdRewarded -= RewardedOnAdRewarded;
            rewardedAd.OnAdInfoChanged -= RewardedOnAdInfoChangedEvent;
        }
    }

    void ShowRewardedAd()
    {
        //Show RewardedAd, check if the ad is ready before showing
        if (rewardedAd.IsAdReady())
        {
            rewardedAd.ShowAd();
        }
    }

    /*
    IEnumerator Reward()
    {
        CreateRewardedAd();
        
        yield return new WaitForSeconds(1f);

        LoadRewardedAd();        
    }


    private LevelPlayRewardedAd RewardedAd;
    void CreateRewardedAd()
    {
        //Create RewardedAd instance
        RewardedAd = new LevelPlayRewardedAd("ut09uum8v113hf65");

        //Subscribe RewardedAd events
        RewardedAd.OnAdLoaded += RewardedOnAdLoadedEvent;
        RewardedAd.OnAdLoadFailed += RewardedOnAdLoadFailedEvent;
        RewardedAd.OnAdDisplayed += RewardedOnAdDisplayedEvent;
        RewardedAd.OnAdDisplayFailed += RewardedOnAdDisplayFailedEvent;
        RewardedAd.OnAdClicked += RewardedOnAdClickedEvent;
        RewardedAd.OnAdClosed += RewardedOnAdClosedEvent;
        RewardedAd.OnAdRewarded += RewardedOnAdRewarded;
        RewardedAd.OnAdInfoChanged += RewardedOnAdInfoChangedEvent;
    }
    void LoadRewardedAd()
    {
        //Load or reload RewardedAd     
        RewardedAd.LoadAd();
    }
    void ShowRewardedAd()
    {
        //Show RewardedAd, check if the ad is ready before showing
        if (RewardedAd.IsAdReady())
        {
            RewardedAd.ShowAd();
        }
    }


    //Implement RewardedAd events
    void RewardedOnAdLoadedEvent(LevelPlayAdInfo adInfo) { ShowRewardedAd(); }
    void RewardedOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) { }
    void RewardedOnAdClickedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdDisplayedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdDisplayFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error) { }
    void RewardedOnAdClosedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdRewarded(LevelPlayAdInfo adInfo, LevelPlayReward adReward) { }
    void RewardedOnAdInfoChangedEvent(LevelPlayAdInfo adInfo) { }
    
    
}
*/