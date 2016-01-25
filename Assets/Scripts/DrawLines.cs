using UnityEngine;
using System.Collections;

// Put this script on a Camera
public class DrawLines : MonoBehaviour
{
    public GameObject mainPoint;
    public Transform secondPoint;
	public LineRenderer lineRenderer;
	public bool canvasElement;

	public Material[] glowMaterials; 

	void Start(){
	}

    // Connect all of the `points` to the `mainPoint`
    void DrawConnectingLines()
    {
		if (mainPoint && secondPoint)
        {

			Vector3 mainPointPos = mainPoint.transform.position;
			if(canvasElement)
			{
				//mainPointPos = Camera.main.WorldToViewportPoint(mainPoint.transform.position);
				//mainPointPos = (secondPoint.transform.position);
			}

			Vector3 pointPos = secondPoint.transform.position;
			//Debug.Log (secondPoint.transform.TransformPoint(GetComponent<MeshFilter>().mesh.bounds.center));
			//testCube.transform.position = secondPoint.TransformPointLocalToWorld(GetComponent<MeshFilter>().mesh.bounds.center);

			lineRenderer.SetPosition(0, mainPointPos);
			lineRenderer.SetPosition(1, pointPos);
        }
    }

    // To show the lines in the game window when it is running. 
    void OnPostRender()
    {
       DrawConnectingLines();
    }

    // To show the lines in the editor
    void OnDrawGizmos()
    {
        DrawConnectingLines();
    }
}