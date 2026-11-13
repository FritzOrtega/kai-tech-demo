using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Collector : MonoBehaviour
{
    
    public GemManager gm;
    
    [System.NonSerialized]
    public int requiredGems = 40;

    public UIDocument uiDoc;
    
    private Button restartButton;
    private Label endText;
    private Label requirement;

    void Start()
    {
        if (uiDoc != null)
        {
            var root = uiDoc.rootVisualElement;
            requirement = root.Q<Label>("Requirement");

            if (requirement != null)
            {
                requirement.text = $"Collect {requiredGems} gems!";
            }

            restartButton = root.Q<Button>("RestartButton");
            if (restartButton != null)
            {
                restartButton.style.display = DisplayStyle.None;
                restartButton.clicked += Restart;
            }

            endText = root.Q<Label>("EndText");
            if (endText != null)
            {
                endText.style.display = DisplayStyle.None;
            }

        }

        else
        {
            Debug.LogError("Collector error: UI Document not found");
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IItem item = collision.GetComponent<IItem>();

          if (item != null)
            {
                gm.AddGems(1);
                CheckWinCondition();
                item.Collect();
            }   
    }

    void CheckWinCondition()
    {
        if (gm != null && gm.gemCount >= requiredGems)
        {
            if (requirement != null) requirement.style.display = DisplayStyle.None;
            if (restartButton != null) restartButton.style.display = DisplayStyle.Flex;
            
            if (endText != null)
            {
                endText.style.display = DisplayStyle.Flex;
                endText.text = "Great job!";
            }

            Time.timeScale = 0f;
            SFXManager.Play("Win");
            
        }
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        
        if (restartButton != null)
        {
            restartButton.clicked -= Restart;
        }
        
        Scene gameplay = SceneManager.GetActiveScene();
        if (gameplay.buildIndex >= 0)
        {
            SceneManager.LoadSceneAsync(gameplay.buildIndex);
        }
    }

    private void OnDestroy()
    {
        if (restartButton != null)
        {
            restartButton.clicked -= Restart;
        }
    }
}
