using UnityEngine;
using UnityEngine.UIElements;
public class GemManager : MonoBehaviour
{
    
    public int gemCount;
    private UIDocument uiDocument;
    private Label gemText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();

        if (uiDocument != null)
        {
            var root = uiDocument.rootVisualElement;
            gemText = root.Q<Label>("Requirement");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gemText != null)
        {
            gemText.text = "Gems: " + gemCount.ToString();
        }
            
    }
}
