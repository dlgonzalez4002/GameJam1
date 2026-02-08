using UnityEngine;
using TMPro;

public class TotalItems : MonoBehaviour
{
    public static TotalItems Instance;
    public int totalItems = 0;

    public TextMeshProUGUI ItemText;
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
        ItemText.text = "Items: " + totalItems;
    }
}
