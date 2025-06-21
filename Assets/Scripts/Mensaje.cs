using System.Collections;
using UnityEngine;
using UnityEngine.Events;
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


        while (tiempo < tiempoEnPantalla && !fueTocado)
        {
            if (Touchscreen.current != null)
            {
                foreach (var touch in Touchscreen.current.touches)
                {
                    if (touch.press.wasPressedThisFrame)
                    {
                        fueTocado = true;
                        break;
                    }
                }
            }

            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                fueTocado = true;
            }

            tiempo += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1;

        float fade = 0f;
        while (fade < duracionDesvanecer)
        {
            fade += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(fade / duracionDesvanecer);
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }

        MensajeLento.SetActive(false);
        alFinalizar?.Invoke();
    }
}
