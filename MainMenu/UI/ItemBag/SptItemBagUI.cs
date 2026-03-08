using System.Collections.Generic;
using UnityEngine;

public class SptItemBagUI : MonoBehaviour
{
    public List<SptItemBagSlot> itemSlotList;
    public SptItemInfoPopup itemInfoPopup;

    public void OpenUI()
    {
        SetItemBagUI();
        gameObject.SetActive(true);
    }
    public void CloseUI()
    {
        itemInfoPopup.OnClose();
        gameObject.SetActive(false);
    }

    public void SetItemBagUI()
    {
        foreach (SptItemBagSlot item in itemSlotList)
        {
            item.SetSlot();
        }
    }
}
