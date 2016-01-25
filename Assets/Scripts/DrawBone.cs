using UnityEngine;
using System.Collections;

public class DrawBone : MonoBehaviour
{
	public float widthStart = 1.5F;
	public float widthEnd = 1.5F;

	void drawbone(Transform t)
	{
		LineRenderer lineRend = this.GetComponent<LineRenderer>();
		lineRend.SetWidth(widthStart, widthEnd);

		Vector3 pos1 = new Vector3(t.position.x, t.position.y, t.position.z);
		Vector3 pos2 = new Vector3(t.GetChild(0).position.x, t.GetChild(0).position.y,t.GetChild(0).position.z);

		lineRend.SetPosition(0, pos1);
		lineRend.SetPosition(1, pos2);
	
	}
	void Update ()
	{
		drawbone (transform);

	}

}