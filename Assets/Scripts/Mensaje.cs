using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.InputSystem;

public class Mensaje : MonoBehaviour
{
    public GameObject MensajeLento;
    public float tiempoEnPantalla = 6f;
    public float duracionDesvanecer = 0.4f;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = MensajeLento.GetComponent<CanvasGroup>();
        MensajeLento.SetActive(false);
    }

    public void MostrarMensaje(UnityAction alFinalizar)
    {
        StartCoroutine(MostrarYDesvanecer(alFinalizar));
    }

    IEnumerator MostrarYDesvanecer(UnityAction alFinalizar)
    {
        MensajeLento.SetActive(true);
        canvasGroup.alpha = 1;

        Time.timeScale = 0;

        float tiempo = 0;
        bool fueTocado = false;

        // Espera hasta que pasen 4 segundos o se toque la pantalla
        while (tiempo < tiempoEnPantalla && !fueTocado)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame || Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                fueTocado = true;
            }

            tiempo += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1;

        // Fade out
        float fade = 0;
        while (fade < duracionDesvanecer)
        {
            fade += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, fade / duracionDesvanecer);
            yield return null;
        }

        MensajeLento.SetActive(false);
        alFinalizar?.Invoke();
    }
}