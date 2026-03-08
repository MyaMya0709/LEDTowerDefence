using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SptMainUI : MonoBehaviour
{
    public SptItemBagUI itemBagUI;
    public SptTotalRecordSet recordUI;
    public SptBattleUI battleUI;
    public SptGambleUI gambleUI;

    public SptStoreUI storeUI;
    public int checkStoreNum;

    public GameObject loadingUI;

    [Header("BasicUI")]
    public TMP_Text curDia_TMP;

    public InputActionReference cancelAction;

    private void OnEnable()
    {
        cancelAction.action.performed += OnGameExit;
        cancelAction.action.Enable();
    }
    private void OnDisable()
    {
        cancelAction.action.performed -= OnGameExit;
        cancelAction.action.Disable();
    }

    #region BasicUI
    public void AllUIClose()
    {
        itemBagUI.CloseUI();
        recordUI.gameObject.SetActive(false);
        battleUI.CloseUI();
        gambleUI.CloseUI();
        storeUI.CloseUI();
    }
    public void OnItemBagUI()
    {
        AllUIClose();
        itemBagUI.OpenUI();
        SetBasicUI();
    }
    public void OnRecordUI()
    {
        AllUIClose();
        recordUI.TotalRecordSet();
        recordUI.gameObject.SetActive(true);
        SetBasicUI();
    }
    public void OnBattleUI()
    {
        AllUIClose();
        battleUI.OpenUI();
        SetBasicUI();
    }
    public void OnGambleUI()
    {
        AllUIClose();
        gambleUI.OpenUI();
        SetBasicUI();
    }
    public void OnItemStoreUI()
    {
        OpenLoadingUI();

        checkStoreNum = 0;
        //서버 데이터 로드
        if (SptGameManager.instance.isRecord) SptGooglePlayGameServices.instance.LoadPackageDataFromCloud();
        else SptGameManager.instance.LoadPackageDataNext();
    }
    public void OnDiaStoreUI()
    {
        OpenLoadingUI();

        checkStoreNum = 1;
        //서버 데이터 로드
        if (SptGameManager.instance.isRecord) SptGooglePlayGameServices.instance.LoadPackageDataFromCloud();
        else SptGameManager.instance.LoadPackageDataNext();
    }
    public void OpenStoreUI() // 0: Item, 1: Dia
    {
        AllUIClose();

        switch (checkStoreNum)
        {
            case 0:
                storeUI.OpenItemUI();
                break;
            case 1:
                storeUI.OpenDiaUI();
                break;
            default:
                Debug.Log("Error");
                break;
        }

        SetBasicUI();

        CloseLoadingUI();
    }
    public void OpenLoadingUI()
    {
        loadingUI.SetActive(true);
    }
    public void CloseLoadingUI()
    {
        loadingUI.SetActive(false);
    }

    public void SetBasicUI()
    {
        curDia_TMP.text = $"{SptDataManager.instance.curDia}";
    }
    #endregion

    public void OnGameExit(InputAction.CallbackContext ctx)
    {
        ExitGame();
    }
    public void ExitGame()
    {
        Debug.Log("aaa");
#if UNITY_ANDROID
        Application.Quit();
#elif UNITY_EDITOR
        // 에디터에서 테스트 시 플레이 모드 종료
        EditorApplication.isPlaying = false;
#else
        // iOS 등에서는 Application.Quit()가 동작하지 않을 수 있음.
        Debug.Log("Quit requested (will work on Android builds).");
#endif
    }

    public void OnGameStart()
    {
        SceneManager.LoadScene("ScnDefense");
    }
    public void OnGameReset()
    {
        // ToDo : 데이터 리셋 로직 필요
    }
}
