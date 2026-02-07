using UnityEngine;

public class TotalItems : MonoBehaviour
{
    public static TotalItems Instance;
    public int totalItems = 0;


    private void Start()
    {
        UpdateItemText();
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(int amount)
    {
        totalItems += amount;
         UpdateItemText();
    }

    void UpdateItemText()
    {
        Debug.Log("Items: " + totalItems);
    }
}
