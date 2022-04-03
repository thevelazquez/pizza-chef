using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListItemController : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText (string name, string x) {
        title.text = name;
        count.text = x;
    }
}
