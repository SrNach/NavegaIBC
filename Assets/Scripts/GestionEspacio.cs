using System.Collections.Generic;
using UnityEngine;

public class GestionEspacio : MonoBehaviour
{
    public static GestionEspacio Instancia { get; private set; }

    private Dictionary<int, Espacio> espacios = new();
    public Espacio espacioSeleccionado;

    void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;
        DontDestroyOnLoad(gameObject);
        InicializarEspacios();
    }

    void InicializarEspacios()
    {
        espacios.Clear();
        espacios[1] = new Sala(1, "IBC 2-1", 2, 0, false, new Vector3(-4.41f, 5.32f, 6.87f));
        espacios[2] = new Sala(2, "IBC 2-4", 2, 0, false, new Vector3(-10.06f, 5.32f, 17.95f)); 
        espacios[3] = new Sala(3, "IBC 2-5", 2, 0, false, new Vector3(9.27f, 5.32f, 18.49f)); 
        espacios[4] = new Sala(4, "IBC 2-6", 2, 0, false, new Vector3(19.3f, 5.32f, 16.48f)); 
        espacios[5] = new Otros(5, "Cafeteria", 1, new Vector3(-19.85f, 1.51f, 8.41f)); 
        espacios[6] = new Otros(6, "Secretaria", 3, new Vector3(9.27f, 9.04f, 13.36f)); 
        espacios[7] = new Bano(7, "Baño Mujeres P1", 1, 0, new Vector3(-16f, -0.32f, 0.62f));
        espacios[8] = new Bano(8, "Baño Hombres P1", 1, 1, new Vector3(-16.47f, -0.32f, 1.64f));
        espacios[9] = new Bano(9, "Baño Mixto P1", 1, 2, new Vector3(-14.42f, -0.32f, 0.29f)); 
        espacios[10] = new Bano(10, "Baño Mujeres P2", 2, 0, new Vector3(12.3f, 5.32f, 5.3f));
        espacios[11] = new Bano(11, "Baño Hombres P2", 2, 1, new Vector3(-13f, 5.32f, 9.69f)); 
        espacios[12] = new Sala(12, "IBC 2-2", 2, 0, false, new Vector3(-26.57f, 3.23f, 10.62f)); 
        espacios[13] = new Sala(13, "IBC 2-3", 2, 0, false, new Vector3(-26.57f, 3.23f, 10.62f)); 
        espacios[14] = new Sala(14, "IBC S1-2", 0, 0, false, new Vector3(17.35f, -1.26f, 9.23f)); 
        espacios[15] = new Sala(15, "IBC S1-3", 0, 0, false, new Vector3(17.35f, -1.26f, 9.23f)); 
        espacios[16] = new Sala(16, "IBC S1-4", 0, 0, false, new Vector3(15.41f, -1.26f, 17.25f)); 
        espacios[17] = new Sala(17, "IBC 1-1", 1, 0, true, new Vector3(-10.37f, -1.26f, 39.32f)); 
        espacios[18] = new Sala(18, "IBC 1-2", 1, 0, true, new Vector3(-10.7f, -1.26f, 39.21f)); 
        espacios[19] = new Sala(19, "IBC 1-3", 1, 0, true, new Vector3(-18.75f, -1.26f, 39.14f)); 
        espacios[20] = new Sala(20, "IBC 2-11", 2, 0, true, new Vector3(14f, 5.32f, 5.36f)); 
        espacios[21] = new Bano(21, "Baño discapacitados", 1, 2, new Vector3(15.22f, -0.32f, 0.44f)); 
        espacios[22] = new Otros(22, "Aulario", 0, new Vector3(-2.83f, -1.26f, 9.362f));
        espacios[23] = new Sala(23, "Laboratorio", 2, 0, true, new Vector3(-13.26f, 5.32f, 13.55f));
    }

    public Espacio getEspacio(string n)
    {
        foreach (var espacio in espacios)
        {
            if (espacio.Value.nombre.Equals(n))
                return espacio.Value;
        }
        return null;
    }

    public List<Sala> GetSalas()
    {
        List<Sala> salas = new List<Sala>();
        foreach (var espacio in espacios)
        {
            if (espacio.Value is Sala sala)
                salas.Add(sala);
        }
        return salas;
    }

    public List<Bano> GetBanos()
    {
        List<Bano> banos = new List<Bano>();
        foreach (var espacio in espacios)
        {
            if (espacio.Value is Bano bano)
                banos.Add(bano);
        }
        return banos;
    }

    public List<Otros> GetOtros()
    {
        List<Otros> otros = new List<Otros>();
        foreach (var espacio in espacios)
        {
            if (espacio.Value is Otros otro)
                otros.Add(otro);
        }
        return otros;
    }
}