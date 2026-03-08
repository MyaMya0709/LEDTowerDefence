using UnityEngine;

public class SptTowerNotice : SptNoticePopup
{
    public override void SetNotice(int num)
    {
        base.SetNotice(num);

        popupText.text = $"GET Lv{num} Tower!!";
    }
}
