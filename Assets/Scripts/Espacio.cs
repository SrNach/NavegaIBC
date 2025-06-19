using UnityEngine;

[System.Serializable]
public class Espacio
{
    public int id;
    public string nombre;
    public int piso;
    public Vector3 coord;

    public Espacio(int id, string nombre, int piso, Vector3 coord)
    {
        this.id = id;
        this.nombre = nombre;
        this.piso = piso;
        this.coord = coord;
    }
}

public class Sala : Espacio
{
    public int capacidad;
    public bool tienePC;

    public Sala(int id, string nombre, int piso, int capacidad, bool tienePC, Vector3 coord)
        : base(id, nombre, piso, coord)
    {
        this.capacidad = capacidad;
        this.tienePC = tienePC;
    }
}

public class Bano : Espacio
{
    public int sexo;

    public Bano(int id, string nombre, int piso, int sexo, Vector3 coord)
        : base(id, nombre, piso, coord)
    {
        this.sexo = sexo;
    }
}

public class Otros : Espacio
{
    public Otros(int id, string nombre, int piso, Vector3 coord)
        : base(id, nombre, piso, coord) { }
}