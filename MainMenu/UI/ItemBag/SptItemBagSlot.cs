using UnityEngine;
using UnityEngine.UI;

public class SptItemBagSlot : MonoBehaviour
{
    public SptItemData itemData;
    public string itemName;
    public Image itemIcon;
    public float baseValue;

    public int itemLevel;
    public float resultValue;
    public float resultNextValue;
    public string itemDescription;
    public string itemNextDescription;

    public SptItemInfoPopup infoPopup;

    public void SetSlot()
    {
        itemName = itemData.itemName;
        itemIcon.sprite = itemData.itemIcon;
        baseValue = itemData.itemValue;

        // 저장된 레벨 불러오기
        itemLevel = SptDataManager.instance.GetItemLevel(itemData.itemID);

        // 레벨에 따라 아이콘 활성화
        if (itemLevel > 0) itemIcon.color = Color.white;
        else itemIcon.color = Color.black;

        // 레벨에 따라 변경된 값 저장
        resultValue = baseValue + baseValue * itemLevel;
        resultNextValue = baseValue + baseValue * (itemLevel + 1);

        // 아이템 설명에 변경된 값 적용
        itemDescription = itemData.itemDescription.Replace("{value}",resultValue.ToString());
        itemNextDescription = itemData.itemDescription.Replace("{value}", resultNextValue.ToString());
    }

    public void OnInfoPopup()
    {
        infoPopup.SetPopup(this);
    }
}
