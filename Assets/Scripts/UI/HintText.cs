using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HintText : MonoBehaviour
{
    [SerializeField] private Text hintText;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private string message;

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        hintText.text = message;    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeText( 0, 1));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeText( 1, 0)); 
        }
    }

    IEnumerator FadeText(float startAlpha, float endAlpha)
    {
        float elapsed = 0;
        Color c = hintText.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            hintText.color = new Color(c.r, c.g, c.b, newAlpha);
            yield return null;
        }

        hintText.color = new Color(c.r, c.g, c.b, endAlpha);
    }
}
