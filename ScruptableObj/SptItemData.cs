using UnityEngine;

[CreateAssetMenu(fileName = "StoItemData", menuName = "Data/StoItemData")]
public class SptItemData : ScriptableObject
{
    public int itemID;

    public Sprite itemIcon;
    public string itemName;
    public float itemValue;
    public string itemDescription;
}
