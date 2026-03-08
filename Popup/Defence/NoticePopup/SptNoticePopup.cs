using System.Collections;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class SptNoticePopup : MonoBehaviour
{
    public SptDefenceUI ui;

    public Image bg;
    public TMP_Text popupText;
    public float duration = 1f;          // 표시 시간

    public float timer;
    public Color startTextColor;
    public Color startBgColor;

    public virtual void Update()
    {
        if (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            // 서서히 사라지게
            bg.color = new Color(startBgColor.r, startBgColor.g, startBgColor.b, 1 - t);
            popupText.color = new Color(startTextColor.r, startTextColor.g, startTextColor.b, 1 - t);

            if (timer >= duration) PopupDestroy();
        }
    }

    public virtual void SetNotice(int num)
    {
        ui = FindFirstObjectByType<SptDefenceUI>();

        startTextColor = popupText.color;
        startBgColor = bg.color;
    }

    public virtual GameObject Initialize(int num, Transform parent)
    {
        GameObject obj = Instantiate(gameObject, parent);

        obj.GetComponent<SptNoticePopup>().SetNotice(num);

        obj.SetActive(false);

        return obj;
    }

    public virtual void PopupDestroy()
    {
        ui.noticeList.Remove(gameObject);
        ui.roundNoticeList.Remove(gameObject);
        ui.isPopup = false;

        ui.NextNoticeCheck();

        Destroy(gameObject);
    }
}