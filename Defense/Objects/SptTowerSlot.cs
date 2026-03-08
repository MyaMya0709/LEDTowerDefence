using UnityEngine;
using UnityEngine.EventSystems;

public class SptTowerSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public SptDefenceUI UI;
    public int slotID;

    private void Start()
    {
        //slotID = transform.GetSiblingIndex();
        //Debug.Log("내 순번은: " + slotID);

        UI = FindFirstObjectByType<SptDefenceUI>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Pointer Down");
        UI.OnClickSlot(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Pointer Up");
    }
}