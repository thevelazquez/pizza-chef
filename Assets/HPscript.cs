using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HPscript : MonoBehaviour
{
    public int HealthPoints;
    public GameObject Self;
    public GameObject Ingredient;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
        if(HealthPoints < 1 && Self.tag == "Player" || HealthPoints < 1 && Self.tag == "Blocking")
        {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        else if(HealthPoints < 1 && Self.tag == "Enemy")
        {
        Destroy(gameObject);
        DropIngredient();
        }
    }
    
    void DropIngredient()
    {
        Vector3 position = transform.position;
        GameObject ingredient = Instantiate(Ingredient, position + new Vector3 (0.0f,1.0f,0.0f), Quaternion.identity);
        ingredient.SetActive(true);
    }
}
