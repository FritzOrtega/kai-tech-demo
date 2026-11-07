using UnityEngine;

public class Gem : MonoBehaviour, IItem
{
    public void Collect()
    {
        SFXManager.Play("Collect");
        Destroy(gameObject);
    }

}
