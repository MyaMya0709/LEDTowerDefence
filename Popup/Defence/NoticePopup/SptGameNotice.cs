using UnityEngine;

public class SptGameNotice : SptNoticePopup
{
    public Animator animator;
    [SerializeField] private int gameState;

    private void SetAinmator(int num)
    {
        switch (num)
        {
            case -1:
                Debug.Log("Fail");
                animator.SetTrigger("OnFail");
                //animator.SetFloat("StateNum", -1);
                break;
            case 0:
                Debug.Log("Start");
                animator.SetTrigger("OnStart");
                //animator.SetFloat("StateNum", 0);
                break;
            case 1:
                Debug.Log("Clear");
                animator.SetTrigger("OnClear");
                //animator.SetFloat("StateNum", 1);
                break;
        }
    }
    private void SetColor(int num)
    {
        Color textColor = Color.white;
        Color bgColor = Color.white;

        switch (num)
        {
            case -1:
                textColor = Color.red;
                bgColor = Color.black;
                break;
            case 0:
                textColor = Color.white;
                bgColor = Color.black;
                break;
            case 1:
                textColor = Color.blue;
                bgColor = Color.white;
                break;
        }

        startTextColor = popupText.color = textColor;
        startBgColor = bg.color = bgColor;
    }
    private void SetText(int num)
    {
        string text = string.Empty;

        switch (num)
        {
            case -1:
                Debug.Log("Fail");
                text = "Defence Failed!!";
                break;
            case 0:
                Debug.Log("Start");
                text = "Defence Start!!";
                break;
            case 1:
                Debug.Log("Clear");
                text = "Defence Clear!!";
                break;
        }

        popupText.text = text;
    }

    public override void SetNotice(int num)
    {
        gameState = num;

        if (num == 0) duration = 1;
        else duration = 4;

        SetAinmator(num);
        SetColor(num);
        SetText(num);
    }

    public override GameObject Initialize(int num, Transform parent)
    {
        GameObject obj = Instantiate(gameObject, parent);
        SptGameNotice objSpt = obj.GetComponent<SptGameNotice>();

        objSpt.ui = FindFirstObjectByType<SptDefenceUI>();

        objSpt.SetNotice(num);

        return obj;
    }

    public override void PopupDestroy()
    {
        if (gameState == 0)
        {
            Debug.Log("GameStartDestroy");
            SptGameManager.instance.isFinish = false;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("GameFinDestroy");
            SptGameManager.instance.isFinish = true;
            if (!SptGooglePlayGameServices.instance.isWork) SptGameManager.instance.LoadDefenceFinNext();
            Destroy(gameObject);
        }
    }
}
