using UnityEngine;

public class DoorBoxDestroy : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(TotalItems.Instance.totalItems >= 5)
        {
            Destroy(gameObject);
        }
    }
}
