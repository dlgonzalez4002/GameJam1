using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("PickUp"))
        {
            TotalItems.Instance.AddItem(1);
            Destroy(gameObject);
        }
    }
}
