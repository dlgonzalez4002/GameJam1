using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public GameObject textObject;
    void Start()
    {
        textObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Complete"))
        {
            textObject.SetActive(true);
        }
    }
}
