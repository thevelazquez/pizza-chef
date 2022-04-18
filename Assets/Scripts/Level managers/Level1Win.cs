using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Win : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BackToHub());
    }

    // Update is called once per frame
    IEnumerator BackToHub() {
        yield return 2.0;
        SceneManager.LoadScene("HubScene");
    }
    void Update()
    {
        
    }
}
