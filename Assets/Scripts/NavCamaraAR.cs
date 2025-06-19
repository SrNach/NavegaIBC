using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavCamaraAR : MonoBehaviour
{
    [SerializeField] private GameObject navTargetObj;
    [SerializeField] private Camera ARCamera;
    [SerializeField] private GameObject Flecha;

    Espacio seleccionado;

    private NavMeshPath path;

    private void Start()
    {
        seleccionado = GestionEspacio.Instancia.espacioSeleccionado;
        //Debug.Log("Seleccion: "+ seleccionado.nombre +" "+ seleccionado.coord);
        if (seleccionado != null && navTargetObj != null)
        {
            navTargetObj.transform.position = seleccionado.coord;
        }
        path = new NavMeshPath();
    }

    void Update()
    {
        NavMesh.CalculatePath(ARCamera.transform.position, navTargetObj.transform.position, NavMesh.AllAreas, path);

        if (path.corners.Length < 2)
            return;

        Vector3 siguientePunto = path.corners[1];
        for (int i = 1; i < path.corners.Length; i++)
        {
            float distancia = Vector3.Distance(ARCamera.transform.position, path.corners[i]);
            if (distancia > 0.5f)
            {
                siguientePunto = path.corners[i];
                break;
            }
        }
        Flecha.transform.position = ARCamera.transform.position + ARCamera.transform.forward * 1.0f + ARCamera.transform.up * -0.3f;
        Vector3 direccion = (siguientePunto - Flecha.transform.position).normalized;
        if (direccion != Vector3.zero)
        {
            Quaternion rotacion = Quaternion.LookRotation(direccion);
            Flecha.transform.rotation = Quaternion.Slerp(Flecha.transform.rotation, rotacion, Time.deltaTime * 5f);
        }
    }

    // Funcion boton
    public void yaLlegue()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}