using UnityEngine;

public class PlanetGentleFloat : MonoBehaviour
{
    public float radius = 5f;           
    public float minDuration = 2.5f;
    public float maxDuration = 4.0f;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(FloatingLoop());
    }

    System.Collections.IEnumerator FloatingLoop()
    {
        while (true)
        {
            Vector2 currentPos = rectTransform.anchoredPosition;

            Vector2 offset = Random.insideUnitCircle.normalized * Random.Range(1f, radius);

            Vector3 scale = rectTransform.lossyScale;
            offset = new Vector2(offset.x / scale.x, offset.y / scale.y);

            Vector2 targetPos = currentPos + offset;
            float duration = Random.Range(minDuration, maxDuration);

            LeanTween.moveLocal(gameObject, targetPos, duration)
                .setEase(LeanTweenType.easeInOutSine);

            yield return new WaitForSeconds(duration * 0.9f);
        }
    }
}
