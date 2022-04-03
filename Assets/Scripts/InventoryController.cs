using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryController : MonoBehaviour
{
    public GameObject collectibles;
    public GameObject itemList;
    public GameObject itemTemplate;
    public float listMargin;
    public float itemOffset;

    // Start is called before the first frame update
    void Start()
    {   
        /*RectTransform self = GetComponent<RectTransform>();
        Debug.Log($"Size: {self.rect.height}");*/
        ScanCollectibles();
        DisplayItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ScanCollectibles () {
        //collect the names of each gameobject in collectibles
        List<string> items = new List<string>();
        foreach (Transform item in collectibles.transform) {
            items.Add(item.GetComponent<CollectibleController>().itemName);
        }

        //make a new list without duplicate items
        List<string> itemTypes = items.Distinct().ToList(); 
        //Count each itesm within the original list then create an inventory list item
        foreach (string itemToCount in itemTypes) {
            int x = 0;
            foreach (string item in items) {
                if (item == itemToCount) {
                    x++;
                }
            }
            CreateListItem(itemToCount, x);
        }

    }

    public void CreateListItem(string name, int count) {
        GameObject listItem = Instantiate(itemTemplate);
        listItem.transform.parent = itemList.transform;
        listItem.GetComponent<ListItemController>().SetText(name, $"0/{count}");
        listItem.transform.localScale = new Vector3(1,1,1);
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

    public void CollectedItem(string item) {
        Debug.Log($"Collected: {item}");
    }
}
