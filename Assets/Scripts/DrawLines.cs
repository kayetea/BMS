using UnityEngine;
using System.Collections;

// Put this script on a Camera
public class DrawLines : MonoBehaviour
{
    public GameObject mainPoint;
    public Transform secondPoint;
	public LineRenderer lineRenderer;
	public bool canvasElement;

	public Material[] glowMaterialsGreen;
	public Material[] glowMaterialsBlue;

	void Start(){
	}

    // Connect all of the `points` to the `mainPoint`
    void DrawConnectingLines()
    {
		if (mainPoint && secondPoint)
        {
			Vector3 mainPointPos = mainPoint.transform.position;
			Vector3 pointPos = secondPoint.transform.position;

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