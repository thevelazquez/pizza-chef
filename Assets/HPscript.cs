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
    GameObject loseMessage;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = HealthPoints;
        if(Self.tag == "Player")
        {
            loseMessage =  GameObject.FindWithTag("Lose Text");
            if(loseMessage != null)
                loseMessage.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(HealthPoints < 1 && Self.tag == "Enemy")
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
        if((Self.tag != "Player" && Self.tag != "Blocking") && Self.GetComponentInChildren<Renderer>().isVisible && showGUI)
        {
            Vector3 target = Camera.main.WorldToScreenPoint(Self.transform.position);
            Rect hpGUI = new Rect(target.x, Screen.height - target.y, 60, 20);
            hpGUI.center = new Vector3(target.x,Screen.height - target.y,target.z);
            GUI.Box(hpGUI, HealthPoints + "/" + maxHealth);
        }
    }

    public void changeHP(int amount)
    {
        HealthPoints+=amount;
        if(loseMessage != null && (HealthPoints < 1 && Self.tag == "Player" || HealthPoints < 1 && Self.tag == "Blocking"))
        {
            StartCoroutine(Lose());
        }
        else
        {
            damaged = StartCoroutine(Damaged());
        }
    }

    public IEnumerator Lose()
    {
        if(loseMessage != null)
        {
            Time.timeScale = 0f;
            loseMessage.SetActive(true);
            yield return new WaitForSecondsRealtime(3);
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            loseMessage.SetActive(false);
        }
    }

    IEnumerator Damaged()
    {
        if(damaged != null)
            StopCoroutine(damaged);
        showGUI = true;
        yield return new WaitForSeconds(4);
        showGUI = false;
    }
}
