using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private string gameplay = "KaiTechDemo";
    
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(gameplay);
    }

    public void QuitGame()
    {
        Debug.Log("Exited game");
        Application.Quit();
    }
}
