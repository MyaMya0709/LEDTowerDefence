using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SptBattleUI : MonoBehaviour
{
    public SptADsWaitPopup adsWaitPopup;
    public Button adsFree_Btn;
    public Button ads_Btn;

    public void OpenUI()
    {
        gameObject.SetActive(true);
        ADFreeBtnOnOff();
    }
    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void OpenADsWaitPopup()
    {
        adsWaitPopup.OpenPopup();
    }

    public void ADFreeBtnOnOff()
    {
        adsFree_Btn.gameObject.SetActive(!SptDataManager.instance.isJoin);
    }

    public void OnShowADs()
    {
        // 시간 체크 : 충족 X -> 잠시후 다시 시도 팝업 
        if (!TimeCheck()) { adsWaitPopup.OpenPopup(); return; }

        if (!SptDataManager.instance.isJoin) ads_Btn.GetComponent<LevelPlayRewardedOnly>().Show(() => GiveReward());
        else GiveReward();
    }

    public void GiveReward()
    {
        SptGameManager.instance.mainUI.loadingUI.SetActive(true);

        // 디버그 여부에 따라 리워드 분기
        Debug.Log("BattleUIPlayReward");
        if (SptGameManager.instance.isRecord)
        {
            SptGooglePlayGameServices.instance.LoadADDiaRewardData();
        }
        else if (!SptGameManager.instance.isRecord)
        {
            SptGameManager.instance.LoadADDiaRewardDataNext();
        }
    }

    public bool TimeCheck()
    {
        bool isOver = false;

        isOver = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - SptDataManager.instance.adsViewTime) > (1000 * 60 * 15);

        return isOver;
    }
}
