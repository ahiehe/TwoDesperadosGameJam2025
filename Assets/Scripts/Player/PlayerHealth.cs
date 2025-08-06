using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject playerSprite;
    public static event Action OnDeath;

    public void Die()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        playerSprite.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        OnDeath?.Invoke();
        yield return new WaitForSeconds(0.1f);

    }
}
