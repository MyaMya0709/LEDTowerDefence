using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SptPackageData : MonoBehaviour
{
    public string productID;

    public int packageID;
    public Image bg;
    public TMP_Text packageDescription;

    public List<SptItemData> itemDatas;     // 판매 아이템 정보
    public int saleDia;                     // 판매 다이아

    public bool isSoldOut = false;

    // 이미 구입한 페키지인지 확인
    public void SoldOutCheck()
    {
        Debug.Log($"{packageID}");
        foreach (var data in SptDataManager.instance.purchaseCountList)
        {
            //Debug.Log($"{data.packageID}");
            if (data.packageID == packageID && packageID >= 100)
            {
                isSoldOut = true;
                break;
            }
            else isSoldOut = false;
        }
    }

    // PackageUI세팅
    public void PackageSetting()
    {
        SoldOutCheck();

        if (isSoldOut)
        {
            bg.color = Color.gray;
            GetComponent<Button>().interactable = false;
        }
        else
        {
            bg.color = Color.white;
            GetComponent<Button>().interactable = true;
        }
    }

    // 구입창 오픈
    public void OnPaymentPopup()
    {
        SptGameManager.instance.mainUI.storeUI.OnPaymentPopup(this);
    }
}
