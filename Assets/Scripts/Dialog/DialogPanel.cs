using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogPanel : MonoBehaviour
{
    public static DialogPanel Instance;

    [Header("UI Components")]
    public Image backgroundImage;
    public TextMeshProUGUI dialogText;

    [Header("Fade Settings")]
    public float fadeDuration = 0.5f;

    private Coroutine currentRoutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Debug.Log("Awaken");

        HideInstant();
    }

    public void DisplayDialog(string message, float duration)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowDialogRoutine(message, duration));
    }

    private IEnumerator ShowDialogRoutine(string message, float duration)
    {
        dialogText.text = message;
        yield return StartCoroutine(Fade(0, 1, fadeDuration));

        yield return new WaitForSeconds(duration);

        yield return StartCoroutine(Fade(1, 0, fadeDuration));

        dialogText.text = "";
        currentRoutine = null;
    }

    private IEnumerator Fade(float fromAlpha, float toAlpha, float duration)
    {
        float elapsed = 0f;
        Color bgColor = backgroundImage.color;
        Color textColor = dialogText.color;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            Color newBg = bgColor;
            newBg.a = Mathf.Lerp(fromAlpha, toAlpha, t);
            backgroundImage.color = newBg;

            Color newText = textColor;
            newText.a = Mathf.Lerp(fromAlpha, toAlpha, t);
            dialogText.color = newText;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Set final alpha
        bgColor.a = toAlpha;
        backgroundImage.color = bgColor;

        textColor.a = toAlpha;
        dialogText.color = textColor;
    }

    private void HideInstant()
    {
        Color bgColor = backgroundImage.color;
        bgColor.a = 0;
        backgroundImage.color = bgColor;

        Color textColor = dialogText.color;
        textColor.a = 0;
        dialogText.color = textColor;

        dialogText.text = "";
    }
}
