using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElegirDestino : MonoBehaviour
{
    [SerializeField] private GameObject navTargetObj;
    [SerializeField] private Camera ARCamera;
    [SerializeField] private GameObject Flecha;
    [SerializeField] private GameObject mundoAR;

    private NavMeshPath path;
    private Quaternion rotacionActual = Quaternion.identity;

    private void Start()
    {
        path = new NavMeshPath();
        Input.compass.enabled = true;
        Input.location.Start();
    }

    private void Update()
    {
        // Alinear el mundo virtual con el norte real continuamente
        if (Input.compass.enabled && Input.compass.timestamp > 0)
        {
            float heading = Input.compass.trueHeading;
            Quaternion rotacionObjetivo = Quaternion.Euler(0, -heading, 0);
            rotacionActual = Quaternion.Slerp(rotacionActual, rotacionObjetivo, Time.deltaTime * 2f); // Suavizado
            mundoAR.transform.rotation = rotacionActual;
        }

        // Calcular ruta hacia el destino
        NavMesh.CalculatePath(ARCamera.transform.position, navTargetObj.transform.position, NavMesh.AllAreas, path);

        if (path.corners.Length < 2)
            return;

        Vector3 siguientePunto = path.corners[path.corners.Length - 1];
        for (int i = 1; i < path.corners.Length; i++)
        {
            if (Vector3.Distance(Flecha.transform.position, path.corners[i]) > 0.5f)
            {
                siguientePunto = path.corners[i];
                break;
            }
        }

        // Posicionar la flecha frente a la cámara
        Flecha.transform.position = ARCamera.transform.position + ARCamera.transform.forward * 1.0f + Vector3.up * -0.3f;

        // Rotar la flecha hacia el siguiente punto de la ruta
        Vector3 direccion = (siguientePunto - Flecha.transform.position).normalized;
        if (direccion != Vector3.zero)
        {
            Quaternion rotacion = Quaternion.LookRotation(direccion);
            Flecha.transform.rotation = Quaternion.Slerp(Flecha.transform.rotation, rotacion, Time.deltaTime * 5f);
        }
    }
}