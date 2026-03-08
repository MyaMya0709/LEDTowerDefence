using UnityEngine;

public class SptCostPopup : SptTextPopup
{
    public Sprite Icon;
    public override void Update()
    {
        if (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            // 점점 빠르게 이동 및 서서히 사라지게
            transform.position = Vector3.Lerp(startPos, endPos, t);
            popupText.color = new Color(startColor.r, startColor.g, startColor.b, 1 - t);

            if (timer >= duration) Destroy(gameObject);
        }
    }

    public override void Initialize(string text, Color color, Vector3 pos, Transform parent)
    {

        popupText.text = text;
        popupText.color = color;
        startPos = pos + Vector3.up * 0.2f;

        endPos = startPos + new Vector3(0, moveDistance, 0);
        startColor = popupText.color;

        Instantiate(gameObject, pos, Quaternion.identity, parent);
    }
}
