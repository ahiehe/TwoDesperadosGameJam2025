using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private GameObject playerObject;
    private void Start()
    {
        playerObject = Instantiate(playerPrefab,transform.position, Quaternion.identity);
        PlayerHealth.OnRespawn += Respawn;
    }

    private void OnDestroy()
    {
        PlayerHealth.OnRespawn -= Respawn;
    }

    public void Respawn()
    {
        Destroy(playerObject);
        playerObject = Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }
}
