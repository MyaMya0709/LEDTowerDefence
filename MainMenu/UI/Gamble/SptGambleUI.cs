using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class SptGambleUI : MonoBehaviour
{
    public SptGetResultPopup resultPopup;
    public SptNotEnoughDiaPopup notEnoughPopup;
    public SptLoadingPopup loadingPopup;

    public SptGambleSurvice gambleSurvice;
    public SptGambleAnimator gambleAnimator;

    (bool, int) gambleResult;

    public void OpenUIDataLoad()
    {
        // 서버데이터 로드
    }
    public void OpenUI()
    {
        gameObject.SetActive(true);
    }
    public void CloseUI()
    {
        resultPopup.OnClose();
        notEnoughPopup.OnClose();
        gameObject.SetActive(false);
    }

    public void OnClickGamble()
    {
        // 서버 데이터 송수신 중이면 반환
        if (SptGooglePlayGameServices.instance.isWork) return;

        loadingPopup.OpenPopup();

        // 서버 데이터 로드
        if (SptGameManager.instance.isRecord) SptGooglePlayGameServices.instance.LoadItemGambleData();
        else SptGameManager.instance.LoadItemGambleNext();
    }

    public void GambleStart()
    {
        gambleResult = gambleSurvice.ItemGamble(); // false, insufficientDia || true, itemID

        if (gambleResult.Item1)
        {
            // 뽑기 애니메이션 시작
            gambleAnimator.AnimationStart(gambleResult.Item2);
        }
        else
        {
            // 부족한 다이아의 수량 참조 후 Popup 열기
            notEnoughPopup.OnOpen(gambleResult.Item2);
        }
    }

    public void ResultPopupOpen()
    {
        // 뽑기 결과 참조 후 Popup 열기
        resultPopup.SetGamble(gambleResult.Item2);
    }
}
