using UnityEngine;
using UnityEngine.SceneManagement;

public class TPScript : MonoBehaviour
{
    public string sceneName;
    
    public void changeScene() {
        SceneManager.LoadScene(sceneName);
    }
}
