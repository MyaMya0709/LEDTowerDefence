using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SptLogManager : MonoBehaviour
{
    public GameObject logPrf;
    public CanvasGroup canvasGroup;

    public List<GameObject> logList;

    int maxLogs = 5;

    // Type -> -1=GambleFail/0=tower/1=core/2=boost/3=NoCost/4=dontLvUp/5=boostCollectFin
    // Num -> -1=None/TowerLv/CoreAmount/BoostType
    public void AddLogPopup(int popupType, int num)
    {
        string text;
        Color color;

        // text,color 설정
        switch (popupType)
        {
            case 0:
                text = $"GET Lv{num} Tower!!";
                color = Color.white;
                break;
            case 1:
                text = $"GET Core x{num}!!";
                color = Color.white;
                break;
            case 2:
                text = $"GET {(ETowerType)num} Boost!!";
                switch (num)
                {
                    case 0:
                        color = Color.red;
                        break;
                    case 1:
                        color = Color.blue;
                        break;
                    case 2:
                        color = Color.green;
                        break;
                    default:
                        color = Color.white;
                        break;
                }
                break;
            case 3:
                text = $"Not Enough Cost!!";
                color = Color.white;
                break;
            case 4:
                text = $"Max Enhance Level!!";
                color = Color.white;
                break;
            case 5:
                text = $"Boost All Collect";
                color = Color.white;
                break;
            default:
                text = "Gamble Fail!!";
                color = Color.white;
                break;
        }

        // 실체화 및 리스트에 저장
        GameObject popup = logPrf.GetComponent<SptLogPopup>().Initialize(text, color, transform);
        popup.GetComponent<SptLogPopup>().manager = this;
        logList.Add(popup);

        UpdateLog();
    }

    public void UpdateLog()
    {
        // 모든 로그의 logNum++
        foreach (GameObject logObj in logList)
        {
            if (logObj == null) continue;
            SptLogPopup log = logObj.GetComponent<SptLogPopup>();
            log.UpdateLogNumber();
        }

        // 로그가 5개 이상이면
        while (logList.Count > maxLogs)
        {
            /*// 로그 리스트 순회
            for (int i = 0; i < logList.Count; i++)
            {
                GameObject logObj = logList[i];
                SptLogPopup log = logList[i].GetComponent<SptLogPopup>();

                // 로그 넘버가 5 이상인 로그 제거
                if(log.logNum > 4)
                {
                    logList.RemoveAt(i);
                    Destroy(logObj);
                }
            }*/

            GameObject oldest = logList[0];
            logList.RemoveAt(0);                       // 먼저 리스트에서 제거
            if (oldest != null)  Destroy(oldest);      // 그 다음 파괴
        }
    }
}
