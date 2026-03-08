using UnityEngine;

public class SptRetryLoginPopup : MonoBehaviour
{

    public void OpenPopup()
    {
        gameObject.SetActive(true);
    }
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
