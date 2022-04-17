using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryController : MonoBehaviour
{
    static List<string> items = new List<string>();
    //public GameObject collectibles;
    public GameObject itemList;
    public GameObject itemTemplate;
    public float listMargin;
    public float itemOffset;

    // Start is called before the first frame update
    void Start()
    {   
        /*RectTransform self = GetComponent<RectTransform>();
        Debug.Log($"Size: {self.rect.height}");*/
        ScanItems();
        DisplayItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ScanItems () {
        //collect the names of each gameobject in collectibles
        //List<string> items = new List<string>();
        /*foreach (Transform item in collectibles.transform) {
            items.Add(item.GetComponent<CollectibleController>().itemName);
        }*/

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
        Debug.Log(string.Join(", ", items) + $" {items.Count}");
    }

    void CreateListItem(string name, int count) {
        //Debug.Log($"Constructing list item {name}");
        GameObject listItem = Instantiate(itemTemplate);
        //listItem.transform.parent = itemList.transform;
        listItem.transform.SetParent(itemList.transform);
        listItem.GetComponent<ListItemController>().SetText(name, count);
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

    void EmptyListItems() {
        if (itemList.transform.childCount > 0) {
            foreach (Transform item in itemList.transform) {
                Destroy(item.gameObject);
            }
        }
        Debug.Log($"There are currently {itemList.transform.childCount} in the list");
    }

    void RefreshItemList() {
        EmptyListItems();
        StartCoroutine(displayAfterEmpty());
    }

    IEnumerator displayAfterEmpty() {
        yield return 0;
        ScanItems();
        DisplayItems();
    }
    //called from a gameobject after it is deactivated from the Collectibles empty
    public void CollectedItem(string item) {
        if (itemList.transform.childCount > 0) {
            foreach (Transform listItem in itemList.transform) {
                ListItemController listItemScript = listItem.GetComponent<ListItemController>();
                if (item == listItemScript.title.text) {
                    listItemScript.AddToCount();
                    items.Add(item);
                    return;
                }
            }
        }
        items.Add(item);
        RefreshItemList();
    }

}
