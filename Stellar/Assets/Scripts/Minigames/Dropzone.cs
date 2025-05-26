using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public GameManager gameManager;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        Drag dragScript = dropped.GetComponent<Drag>();
        if (dragScript == null || dragScript.locked) return;

        Transform currentParent = dragScript.transform.parent;
        Transform newSlot = transform;

        if (newSlot.childCount > 0)
        {
            Transform existingItem = newSlot.GetChild(0);
            Drag existingDrag = existingItem.GetComponent<Drag>();

            if (existingDrag != null)
            {
                if (existingDrag.locked)
                {
                    dropped.transform.SetParent(currentParent);
                    dropped.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    return;
                }
                existingItem.SetParent(currentParent);
                existingItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                if (existingDrag.correctSlot == currentParent)
                {
                    existingDrag.locked = true;
                }
                else
                {
                    existingDrag.locked = false;
                }

                existingDrag.UpdateVisualState();
            }
        }

        dropped.transform.SetParent(newSlot);
        dropped.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        if (dragScript.correctSlot == newSlot)
        {
            dragScript.locked = true;
        }
        else
        {
            dragScript.locked = false;
        }

        dragScript.UpdateVisualState();

        if (gameManager != null)
        {
            gameManager.CheckForWin();
        }
    }
}
