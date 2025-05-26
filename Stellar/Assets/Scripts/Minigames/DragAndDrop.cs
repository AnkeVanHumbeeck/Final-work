using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Transform originalParent;
    private Vector2 originalPosition;
    private Image image;
    private Color originalColor;

    [HideInInspector] public bool locked = false;
    public Transform correctSlot;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>() ?? FindObjectOfType<Canvas>();

        image = GetComponent<Image>();
        if (image != null)
        {
            originalColor = image.color;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (locked) return;

        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (locked) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (locked) return;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if (transform.parent == originalParent || transform.parent == transform.root)
        {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    public void UpdateVisualState()
    {
        if (image == null) return;

        if (locked)
        {
            image.color = Color.Lerp(originalColor, Color.gray, 0.5f);
        }
        else
        {
            image.color = originalColor;
        }
    }
}
