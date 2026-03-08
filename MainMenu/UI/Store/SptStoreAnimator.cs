using NUnit.Framework;
using UnityEngine;

public class SptStoreAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private bool isLoadEnd = false;

    // 아이템 ID, 등급
    [SerializeField] private SptPackageData packageData;
    [SerializeField]private int tier; // 0 B, 1 A, 2 S, 3 SS

    public void AnimationStart(SptPackageData data)
    {
        Debug.Log("Anim Start!");

        // 애니메이션 시작
        gameObject.SetActive(true);

        // 세팅 초기화
        SetBoolClear();

        // 아이템 아이디 저장
        packageData = data;
    }

    public void AnimationProcess()
    {
        // 루프 진입, 서버 완료되면 통과
        ServerWorkCheck();
        animator.SetBool("IsLoadEnd", isLoadEnd);

        // 서버 완료시 등급에 따라 재생
        if (isLoadEnd) ItemLevelCheck();
    }

    private void ServerWorkCheck()
    {
        if (!SptGooglePlayGameServices.instance.isWork) isLoadEnd = true;
        else isLoadEnd = false;
    }

    public void ItemLevelCheck()
    {
        // 아이템 등급 체크
        int itemLevel = 4;
        if(packageData.itemDatas.Count != 0)
        {
            foreach (SptItemData item in packageData.itemDatas)
            {
                if (itemLevel == 0) break;
                if(item.itemID/100 < itemLevel) itemLevel = item.itemID/100;
            }
        }
        else itemLevel = 3;

        tier = itemLevel;

        // 애니메이션 재생 세팅
        switch (tier)
        {
            case 0: animator.SetBool("SS", true); break;
            case 1: animator.SetBool("S", true); break;
            case 2: animator.SetBool("A", true); break;
            case 3: animator.SetBool("B", true); break;
            default: Debug.LogError("Not Found Tier"); break;
        }
    }

    public void SetBoolClear()
    {
        animator.SetBool("IsLoadEnd", false);
        animator.SetBool("SS", false);
        animator.SetBool("S", false);
        animator.SetBool("A", false);
        animator.SetBool("B", false);
    }

    public void AnimationEnd()
    {
        if (packageData.itemDatas.Count != 0)
        {
            SptGameManager.instance.SavePurchasePackageNext();
        }
        else
        {
            SptGameManager.instance.SavePurchaseDiaNext();
        }
    }

    public void CloseAnimation()
    {
        // 애니메이션 끝
        gameObject.SetActive(false);
    }
}
