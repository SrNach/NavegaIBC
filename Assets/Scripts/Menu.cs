using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject PantallaPrincipal;
    public GameObject PantallaSeleccion;
    public GameObject PantallaConfirmacion;

    [Header("Dropdowns")]
    public TMP_Dropdown ddEspacios;
    public TMP_Dropdown ddOpciones;

    [Header("Textos")]
    public TMP_Text textito;
    public TMP_Text textoConfirmacion;

    public GestionEspacio gEsp;
    private string tipoSeleccionado; // Sala, Baño, Otros
    private string opcionSeleccionada;
    public Mensaje mensajeController;


    void Start()
    {
        gEsp = GestionEspacio.Instancia;
        MostrarPantallaPrincipal();
    }

    // PANTALLA PRINCIPAL
    void MostrarPantallaPrincipal()
    {
        PantallaPrincipal.SetActive(true);
        PantallaSeleccion.SetActive(false);
        PantallaConfirmacion.SetActive(false);
    }

    // PANTALLA SELECCIÓN
    void MostrarPantallaSeleccion()
    {
        PantallaPrincipal.SetActive(false);
        PantallaSeleccion.SetActive(true);
        PantallaConfirmacion.SetActive(false);

        if (tipoSeleccionado == "Otros")
        {
            textito.text = "Selecciona destino";
        }
        else
            textito.text = "Selecciona " + tipoSeleccionado.ToLower();

        List<Espacio> opciones = ObtenerOpciones(tipoSeleccionado);
        ddOpciones.ClearOptions();

        List<string> nombres = new List<string>();
        foreach (var espacio in opciones)
        {
            nombres.Add(espacio.nombre);
        }

        ddOpciones.AddOptions(nombres);
    }

    // PANTALLA CONFIRMACIÓN
    void MostrarPantallaConfirmacion()
    {
        PantallaPrincipal.SetActive(false);
        PantallaSeleccion.SetActive(false);
        PantallaConfirmacion.SetActive(true);

        textoConfirmacion.text = "¿Quieres ir a " +opcionSeleccionada + "?";
    }

    List<Espacio> ObtenerOpciones(string tipo)
    {
        switch (tipoSeleccionado)
        {
            case "Sala":
                return GestionEspacio.Instancia.GetSalas().ConvertAll(s => (Espacio)s);
                //break;
            case "Baño":
                return GestionEspacio.Instancia.GetBanos().ConvertAll(b => (Espacio)b);
                //break;
            case "Otros":
                return GestionEspacio.Instancia.GetOtros().ConvertAll(c => (Espacio)c);
                //break;
        }
        return new List<Espacio>();
    }

    public void OnSeleccionEspacio()
    {
        tipoSeleccionado = ddEspacios.options[ddEspacios.value].text;
        MostrarPantallaSeleccion();
    }

    public void OnSeleccionOpcion()
    {
        opcionSeleccionada = ddOpciones.options[ddOpciones.value].text;
        MostrarPantallaConfirmacion();
    }

    public void OnConfirmarSi()
    {
        Espacio esp = gEsp.getEspacio(opcionSeleccionada);
        if (esp == null)
        {
            MostrarPantallaPrincipal();
            return;
        }

        gEsp.espacioSeleccionado = esp;
        mensajeController.MostrarMensaje(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        });
    }

    public void OnConfirmarNo()
    {
        MostrarPantallaPrincipal();
    }

    public void OnVolver()
    {
        MostrarPantallaPrincipal();
    }
    public void OnVolverConf()
    {
        MostrarPantallaSeleccion();
    }
}