using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerLoad : MonoBehaviour
{
    float cntdnw = 5;
    public Text disvar;
    void Start()
    {
        StartCoroutine(BacktoHub());
    }
    void Update()
    {
        if (cntdnw > 0)
        {
            cntdnw -= Time.deltaTime;
        }
        double b = System.Math.Round(cntdnw, 2);
        disvar.text = b.ToString();
        if (cntdnw < 0)
        {
            Debug.Log("Completed");
        }
    }

    IEnumerator BacktoHub()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        SceneManager.LoadScene("HubScene");


    }

}
