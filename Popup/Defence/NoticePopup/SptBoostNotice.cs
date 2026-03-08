using UnityEngine;

public class SptBoostNotice : SptNoticePopup
{
    public override void SetNotice(int num)
    {
        base.SetNotice(num);

        popupText.text = $"GET {(ETowerType)num} Boost!!";
    }
}
