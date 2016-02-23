using UnityEngine;
using System.Collections;

public class Fade3dPart : MonoBehaviour {

	//private bool isZoomed = false;
	public Material transMat;
	private Material glowMat;

	// Use this for initialization
	void Start () {
		glowMat = this.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseOver(){
		if(GameObject.Find("zoomCamera(Clone)"))
		{
			//fade 3d object
			this.GetComponent<Renderer>().material = transMat;

			//show piping below
		}
	}

	void OnMouseExit(){
		if(GameObject.Find ("zoomCamera(Clone)"))
		{
			this.GetComponent<Renderer>().material = glowMat;
		}
	}
}
