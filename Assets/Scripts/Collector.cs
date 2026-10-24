using UnityEngine;
using UnityEngine.UIElements;

public class Collector : MonoBehaviour
{
    
    public GemManager gm;
    public int requiredGems = 30;

    public UIDocument uiDoc;
    
    private Button restartButton;
    private Label endText;
    private Label requirement;

    void Start()
    {
        requirement = uiDoc.rootVisualElement.Q<Label>("Requirement");
        requirement.text = "Collect " + requiredGems.ToString() + " gems!";

        restartButton = uiDoc.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += Restart;

        endText = uiDoc.rootVisualElement.Q<Label>("EndText");
        endText.style.display = DisplayStyle.None;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IItem item = collision.GetComponent<IItem>();

        if(item != null)
        {
            item.Collect();
            gm.gemCount++;
            CheckWinCondition();

        }
    }

    void CheckWinCondition()
    {
        if (gm.gemCount >= requiredGems)
        {
            requirement.style.display = DisplayStyle.None;
            
            restartButton.style.display = DisplayStyle.Flex;
            endText.style.display = DisplayStyle.Flex;
            endText.text = "Great job!";
        }
    }

    private void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("kai tech demo");
    }
}
