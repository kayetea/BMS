﻿using UnityEngine;
using System.Collections;

public class InfoBubble : MonoBehaviour {

	public string infoUI;
	public Material objMaterial; //glow
	public GameObject sliderHandle;

	private GameObject canvasTemp;
	private GameObject mainCamera;

	void Start(){
		canvasTemp = GameObject.Find("Canvas/Temp");
		mainCamera = GameObject.Find ("Main Camera");

		objMaterial.SetColor("_OutlineColor", Color.cyan);

	}

	void OnMouseDown(){
		bool currentUI = false;

		if(!GameObject.Find("zoomCamera(Clone)")){ //makes sure that you're not already zoomed in on a feature

			//Delete a prev button
			if(canvasTemp.transform.childCount > 0)
			{
				int temp = canvasTemp.transform.childCount;
				if(canvasTemp.transform.GetChild(temp-1).gameObject.name == infoUI+"(Clone)")
				{
					currentUI = true;
				}

				DestroyObject(canvasTemp.transform.GetChild(temp-1).gameObject);

				//Reset all glow materials to green/blue
				foreach (Material mat in mainCamera.GetComponent<DrawLines>().glowMaterialsGreen)
				{
					mat.SetColor("_OutlineColor", Color.green);
				}
				foreach (Material mat in mainCamera.GetComponent<DrawLines>().glowMaterialsBlue)
				{
					mat.SetColor("_OutlineColor", Color.cyan);
				} 
			}

			if (!currentUI){
				//load new info bubble
				GameObject ui = Instantiate(Resources.Load (infoUI) as GameObject);
				ui.transform.SetParent(canvasTemp.transform, false);

				objMaterial.SetColor("_OutlineColor", Color.gray);

				//enable line renderer and drawlines and set
				mainCamera.GetComponent<LineRenderer>().enabled = true;
				mainCamera.GetComponent<DrawLines>().enabled = true;

				//set drawlines
				mainCamera.GetComponent<DrawLines>().secondPoint = this.transform;

				//pause anim
				sliderHandle.GetComponent<ModelRotationSlider>().MouseUp();

				// if tv, change the screen image

			}
			else{
				//remove linerenderer and draw components
				mainCamera.GetComponent<LineRenderer>().enabled = false;
				mainCamera.GetComponent<DrawLines>().enabled = false;

				//glow green
				objMaterial.SetColor("_OutlineColor", Color.cyan);

			}
		}
	}

}
