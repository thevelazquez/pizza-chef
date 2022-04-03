using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject collectibles;
    public GameObject itemList;
    public float listMargin;
    public float itemOffset;

    // Start is called before the first frame update
    void Start()
    {   
        /*RectTransform self = GetComponent<RectTransform>();
        Debug.Log($"Size: {self.rect.height}");*/
        DisplayItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayItems() {
        int index = 0;
        foreach (Transform item in itemList.transform) {
            RectTransform itemRect = item.GetComponent<RectTransform>();
            itemRect.anchoredPosition = new Vector2(0, ((itemRect.rect.height + itemOffset) * -index - listMargin));

            //Debug.Log($"localPos: {item.localPosition} Rect: {itemRect.rect}");
            index++;
        }
    }
}
