using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SptBeginLaunch : MonoBehaviour
{
    public SptRetryLoginPopup retryLoginPopup_Obj;

    public void LoadDatas()
    {
        // json 파일 체크 및 데이터 초기화
        SptSaveManager.instance.LoadLocalData();

        // 서버데이터 로드
        SptGooglePlayGameServices.instance.LoadAllData();
    }

    public void InGameSetting()
    {
        // 인게임 다이아, 아이템 데이터 로드
        SptDataManager.instance.DataLoad();
        // 기록 데이터 로드
        SptRecordManager.instance.RecordDataLoad();
        // 메인메뉴 호출
        StartCoroutine(LoadMainMenu("ScnMainMenu"));
    }


    IEnumerator LoadMainMenu(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100f) + "%");
            yield return null;
        }
    }

    public void OnLogin()
    {
        Debug.Log("OnLogin");
        retryLoginPopup_Obj.ClosePopup();
        SptGooglePlayGameServices.instance.OnTryLogin();
    }
    public void OnExitGame()
    {
        Debug.Log("OnExitGame");
        retryLoginPopup_Obj.ClosePopup();
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
}
