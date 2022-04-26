using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HPscript : MonoBehaviour
{
    public int HealthPoints;
    int maxHealth;
    bool showGUI = false;
    public GameObject Self;
    public GameObject Ingredient;
    Coroutine damaged;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = HealthPoints;
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

    void OnGUI()
    {
        if(Self.GetComponentInChildren<Renderer>().isVisible && showGUI)
        {
            Vector3 target = Camera.main.WorldToScreenPoint(Self.transform.position);
            Rect hpGUI = new Rect(target.x, Screen.height - target.y, 60, 20);
            hpGUI.center = new Vector3(target.x,Screen.height - target.y,target.z);
            GUI.Box(hpGUI, HealthPoints + "/" + maxHealth);
        }
    }

    public void changeHP(int amount)
    {
        if(damaged != null)
            StopCoroutine(damaged);
        HealthPoints+=amount;
        Debug.Log(Self.name);
        damaged = StartCoroutine(Damaged());
    }

    IEnumerator Damaged()
    {
        showGUI = true;
        yield return new WaitForSeconds(4);
        showGUI = false;
    }
}
