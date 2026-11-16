using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    [SerializeField] private string menu = "Menu";
    [SerializeField] private string gameplay = "KaiTechDemo";
    
    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync(menu);
        MusicManager.pauseBGM();
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