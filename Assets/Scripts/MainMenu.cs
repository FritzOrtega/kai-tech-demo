using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [Header("Scenes")]
    [SerializeField] private string menu = "Menu";
    [SerializeField] private string gameplay = "KaiTechDemo";
    
    public void SelectMenu()
    {
        SceneManager.LoadSceneAsync(menu);
    }
    
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
