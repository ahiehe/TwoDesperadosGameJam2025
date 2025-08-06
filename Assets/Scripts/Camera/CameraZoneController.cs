using UnityEngine;

public class CameraZoneController : MonoBehaviour
{
    [SerializeField] private Transform cameraPos;

    [Header("Cam settings")]
    [SerializeField] private float camDistance;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CameraController.instance.SetFollow(cameraPos);
            if (camDistance != 0) CameraController.instance.SetDistance(camDistance);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CameraController.instance.SetCameraToDefault();
        }
    }
}
