using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float mouseSensitivity = 120f;
    public float minPitch = -36f;
    public float maxPitch = 25f;

    private float yaw;
    private float pitch;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - player.transform.position;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        
        Quaternion yawRotation = Quaternion.Euler(0f, yaw, 0f);
        Vector3 yawOffset = yawRotation * offset;

        
        Vector3 rightAxis = yawRotation * Vector3.right;
        Quaternion pitchRotation = Quaternion.AngleAxis(pitch, rightAxis);
        Vector3 finalOffset = pitchRotation * yawOffset;

     
        transform.position = player.transform.position + finalOffset;
        transform.LookAt(player.transform.position);


    }
}
