using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject aboutScreen;
    public GameObject creditScreen;
    public GameObject controlScreen;
    public GameObject genScreen;

    void Start()
    {
        aboutScreen.SetActive(false);
        creditScreen.SetActive(false);
        controlScreen.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("HubScene");
    }


    public void LoadAboutScreen()
    {
        genScreen.SetActive(false);
        aboutScreen.SetActive(true);
    }

    public void LoadCreditScreen()
    {
        aboutScreen.SetActive(false);
        creditScreen.SetActive(true);
    }

    public void LoadControlsScreen()
    {
        aboutScreen.SetActive(false);
        controlScreen.SetActive(true);
    }

    public void ReturntoAbout()
    {
        controlScreen.SetActive(false);
        creditScreen.SetActive(false);
        aboutScreen.SetActive(true);
    }

    public void ReturntoGenScreen()
    {
        aboutScreen.SetActive(false);
        genScreen.SetActive(true);
    }

        public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
