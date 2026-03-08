using UnityEngine;

public class SptCoreNotice : SptNoticePopup
{
    public override void SetNotice(int num)
    {
        base.SetNotice(num);

        popupText.text = $"GET Core x{num}!!";
    }
}
