using UnityEngine;

public class AudioScript : MonoBehaviour
{

    AudioSource audio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PickUp")) {
            audio.Play();
            
        }
    }
}
