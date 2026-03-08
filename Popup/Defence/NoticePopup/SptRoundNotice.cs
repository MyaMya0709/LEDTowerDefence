using UnityEngine;

public class SptRoundNotice : SptNoticePopup
{
    public override void SetNotice(int num)
    {
        base.SetNotice(num);

        popupText.text = $"Round {num:00}";
    }
}
