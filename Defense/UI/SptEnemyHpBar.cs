using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SptEnemyHpBar : MonoBehaviour
{
    public Canvas canvas;
    public Image hpSlider;
    public TMP_Text hp_TMP;

    public SptEnemy enemy; // 적 스크립트 참조

    void Awake()
    {
        canvas.worldCamera = Camera.main;
        canvas.enabled = false;
    }

    void Update()
    {
        if (!canvas.enabled && enemy.curHp < enemy.maxHp)
        {
            canvas.enabled = true;
        }

        hpSlider.fillAmount = enemy.curHp / enemy.maxHp;

        //// 카메라 바라보기
        //panel.transform.forward = Camera.main.transform.forward;
    }
}
