using UnityEngine;
using System.Collections.Generic;

public class Shuffler : MonoBehaviour
{
    public List<GameObject> items;
    public List<Transform> dropSlots;

    void Start()
    {
        ShuffleItems();
    }

    void ShuffleItems()
    {
        List<GameObject> shuffled = new List<GameObject>(items);

        bool validShuffle = false;
        int maxAttempts = 100;
        int attempt = 0;

        while (!validShuffle && attempt < maxAttempts)
        {
            attempt++;
            for (int i = 0; i < shuffled.Count; i++)
            {
                int randomIndex = Random.Range(i, shuffled.Count);
                GameObject temp = shuffled[i];
                shuffled[i] = shuffled[randomIndex];
                shuffled[randomIndex] = temp;
            }

            validShuffle = true;

            for (int i = 0; i < shuffled.Count; i++)
            {
                Drag drag = shuffled[i].GetComponent<Drag>();
                if (drag != null && drag.correctSlot == dropSlots[i])
                {
                    validShuffle = false;
                    break;
                }
            }
        }

        for (int i = 0; i < dropSlots.Count; i++)
        {
            GameObject item = shuffled[i];
            item.transform.SetParent(dropSlots[i]);
            RectTransform rt = item.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector2.zero;
        }

        if (!validShuffle)
        {
            Debug.LogWarning("Could not find a valid shuffle after 100 attempts.");
        }
    }
}
