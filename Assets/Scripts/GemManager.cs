using UnityEngine;
using UnityEngine.UI;
public class GemManager : MonoBehaviour
{
    
    public int gemCount;
    public Text gemText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gemText.text = "Gems: " + gemCount.ToString();
    }
}
