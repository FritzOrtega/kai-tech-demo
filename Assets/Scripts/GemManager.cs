using UnityEngine;
using UnityEngine.UIElements;
public class GemManager : MonoBehaviour
{
    
    public int gemCount;
    public UIDocument uiDocument;
    private Label gemText;

    
    void Start()
    {
        if (uiDocument == null)
        {
            uiDocument = GetComponent<UIDocument>();
        }

        if (uiDocument != null)
        {
            var root = uiDocument.rootVisualElement;
            gemText = root.Q<Label>("GemCount");
            UpdateGemUI();
        }

        else
        {
            Debug.LogError("GemManager error: UI Document either not found or assigned");
        }
    }

    public void AddGems(int amount)
    {
        gemCount += amount;
        UpdateGemUI();
  
    }
    
    
    private void UpdateGemUI()
    {
        if (gemText != null)
        {
            gemText.text = "Gems: " + gemCount.ToString();
        }
            
    }
}
