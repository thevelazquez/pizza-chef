using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HPscript : MonoBehaviour
{
    public int HealthPoints;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
        if(HealthPoints == 0)
        {
            SceneManager.LoadScene("MainMenu");

        }
    }
}
