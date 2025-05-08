using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElegirDestino : MonoBehaviour
{
    [SerializeField]
    private GameObject navTargetObj;
    [SerializeField]
    private Camera ARCamera;

    private NavMeshPath path;
    private LineRenderer line;
    // Start is called before the first frame update
    private void Start()
    {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        NavMesh.CalculatePath(ARCamera.transform.position, navTargetObj.transform.position, NavMesh.AllAreas, path);
        line.positionCount = path.corners.Length;
        line.SetPositions(path.corners);
        line.enabled = true;     
    }
}
