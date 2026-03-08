using System.Collections;
using UnityEngine;

public class SptMissileHitEffect : MonoBehaviour
{
    public Sprite hitEffect;

    private void Start()
    {
        StartCoroutine(AppleEffect());
    }

    public IEnumerator AppleEffect()
    {
        // 폭발 이펙트 호출
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.sprite = hitEffect; // 이미지 교체

        float duration = 1f;             // 애니메이션 지속 시간
        Vector3 startScale = Vector3.one * 0.5f;    // 시작 스케일
        Vector3 endScale = startScale * 2f; // 끝 스케일 (2배)

        float elapsed = 0f;
        Color originalColor = sprite.color = Color.white;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // 스케일 보간 (선형)
            transform.localScale = Vector3.Lerp(startScale, endScale, t);

            // 알파 보간: 원래 컬러에서 알파를 0까지
            Color c = originalColor;
            c.a = Mathf.Lerp(originalColor.a, 0f, t);
            sprite.color = c;

            yield return null;
        }

        // 끝에 확실히 투명하게 & 스케일 맞추기
        transform.localScale = endScale;
        Color endColor = originalColor;
        endColor.a = 0f;
        sprite.color = endColor;

        Destroy(gameObject);
    }
}
