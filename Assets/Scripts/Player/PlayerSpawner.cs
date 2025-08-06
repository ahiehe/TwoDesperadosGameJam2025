using UnityEngine;
using Cinemachine;
using System;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    public static GameObject PlayerObject { get; private set; }

    public static event Action OnPlayerCreation;
    private void Awake()
    {  
        PlayerHealth.OnDeath += Respawn;
        SpawnPlayer();
    }

    private void OnDestroy()
    {
        PlayerHealth.OnDeath -= Respawn;
    }

    public void Respawn()
    {
        Destroy(PlayerObject);
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        PlayerObject = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        OnPlayerCreation?.Invoke();
    }
}
