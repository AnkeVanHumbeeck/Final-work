using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RocketDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public List<RectTransform> pathPoints;
    public Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public float followSpeed = 200f;
    private bool isDragging = false;
    private Vector2 originalPosition;


    private int currentPointIndex = 0;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        originalPosition = rectTransform.anchoredPosition;

        canvasGroup = GetComponent<CanvasGroup>();

        if (pathPoints.Count > 0)
        {
            currentPointIndex = 0;
            rectTransform.anchoredPosition = pathPoints[currentPointIndex].anchoredPosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            eventData.position,
            canvas.worldCamera,
            out localMousePos
        );

        List<int> allowedIndices = new List<int>();
        if (currentPointIndex - 1 >= 0) allowedIndices.Add(currentPointIndex - 1);
        if (currentPointIndex + 1 < pathPoints.Count) allowedIndices.Add(currentPointIndex + 1);

        RectTransform nearestPoint = null;
        float minDist = float.MaxValue;

        foreach (int i in allowedIndices)
        {
            float dist = Vector2.Distance(localMousePos, pathPoints[i].anchoredPosition);
            if (dist < minDist)
            {
                minDist = dist;
                nearestPoint = pathPoints[i];
            }
        }

        if (nearestPoint != null)
        {
            Vector2 targetPos = nearestPoint.anchoredPosition;
            float step = followSpeed * Time.deltaTime;

            Vector2 oldPos = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = Vector2.MoveTowards(oldPos, targetPos, step);

            Vector2 movement = rectTransform.anchoredPosition - oldPos;

            Vector2 forward = new Vector2(
                Mathf.Cos(rectTransform.eulerAngles.z * Mathf.Deg2Rad),
                Mathf.Sin(rectTransform.eulerAngles.z * Mathf.Deg2Rad)
            );

            Vector2 toTarget = (targetPos - oldPos).normalized;

            float dot = Vector2.Dot(forward, toTarget);

            if (movement.sqrMagnitude > 0.01f && dot > -0.7f)
            {
                float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                float smoothAngle = Mathf.LerpAngle(
                    rectTransform.eulerAngles.z,
                    angle,
                    Time.deltaTime * 5f
                );
                rectTransform.rotation = Quaternion.Euler(0, 0, smoothAngle);
            }

            if (Vector2.Distance(rectTransform.anchoredPosition, targetPos) < 1f)
            {
                currentPointIndex = pathPoints.IndexOf(nearestPoint);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        canvasGroup.blocksRaycasts = true;
        TryDrop(eventData);
    }
    
    private bool TryDrop(PointerEventData eventData)
    {
        foreach (var result in eventData.hovered)
        {
            if (result.CompareTag("LevelPoint"))
            {
                Debug.Log("Dropped on level: " + result.name);
                SceneManager.LoadScene(result.name);
                return true;
            }
        }

        return false;
    }
}