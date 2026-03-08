using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SptLogPopup : MonoBehaviour
{
    public SptLogManager manager;

    public Image bg;
    public TMP_Text popupText;

    public Color startTextColor;
    public Color startBgColor;

    public int logNum;
    [SerializeField] private float[] stepTable = { 1f, 0.8f, 0.6f, 0.4f, 0.2f, 0};

    public float elapsed ;
    public float duration = 5;

    float stepAlpha;
    float timeAlpha;
    float totalAlpha;

    private void Update()
    {
        if (elapsed <= duration)
        {
            elapsed += Time.deltaTime;
            timeAlpha = Mathf.Clamp01(1f - elapsed / duration);

            GetTotalAlpah();

            // 서서히 사라지게
            bg.color = new Color(startBgColor.r, startBgColor.g, startBgColor.b, totalAlpha);
            popupText.color = new Color(startTextColor.r, startTextColor.g, startTextColor.b, totalAlpha);

            if (elapsed > duration)
            {
                manager.logList.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }

    public GameObject Initialize(string text, Color color, Transform parent)
    {
        logNum = -1;

        popupText.text = text;
        popupText.color = color;

        startTextColor = popupText.color;
        startBgColor = bg.color;

        return Instantiate(gameObject, parent);
    }

    public void UpdateLogNumber()
    {
        logNum++;
        UpdateAlpha();
    }

    public void UpdateAlpha()
    {
        int idx = Mathf.Clamp(logNum, 0, stepTable.Length - 1);

        //if(idx >= 5)
        //{
        //    manager.logList.Remove(gameObject);
        //    Destroy(gameObject);
        //}
        //else stepAlpha = stepTable[idx];

        stepAlpha = stepTable[idx];

        GetTotalAlpah();
    }

    public void GetTotalAlpah()
    {

        /*// timeAlpha: 0~1, 시간에 따라 단조감소, 종료 시 0
        // stepAlpha: 0~1, 단계가 높을수록 더 작아짐(더 투명)

        float a_mix = Mathf.Lerp(timeAlpha, stepAlpha, 0.5f); // 비율 섞기
        totalAlpha = Mathf.Min(timeAlpha, a_mix);     // 시간 상한을 걸어 "무조건 0" 보장*/

        float t = Mathf.Clamp01(timeAlpha);
        float s = Mathf.Clamp01(stepAlpha);

        float w = 1f; // 필요하면 멤버/인스펙터로 빼기
        float a_mix = Mathf.Lerp(t, s, w);

        totalAlpha = Mathf.Min(t, a_mix); // 종료 시 0 보장 + 시간 상한
    }
}
