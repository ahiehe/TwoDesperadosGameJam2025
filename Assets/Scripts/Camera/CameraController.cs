using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance {  get; private set; }
    private CinemachineVirtualCamera virtualCam;

    private float defaultDistance;

    private void Awake()
    {
        instance = this;

        virtualCam = GetComponent<CinemachineVirtualCamera>();
        defaultDistance = virtualCam.m_Lens.OrthographicSize;
        PlayerSpawner.OnPlayerCreation += SetCameraToDefault;
    }

    private void Start()
    {
        SetCameraToDefault();
    }

    private void OnDestroy()
    {
        PlayerSpawner.OnPlayerCreation -= SetCameraToDefault;
    }

    public void SetCameraToDefault()
    {
        virtualCam.m_Lens.OrthographicSize = defaultDistance;
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        SetFollow(PlayerSpawner.PlayerObject.transform);
    }

    public void SetFollow(Transform followPos)
    {
        virtualCam.LookAt = followPos;
        virtualCam.Follow = followPos;
    }

    public void SetDistance(float distance)
    {
        virtualCam.m_Lens.OrthographicSize = distance;
    }
}
