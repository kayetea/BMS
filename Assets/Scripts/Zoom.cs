/* Add to each clickable 3d object -- add a box collider and the script
 * 
 * Assign the: 
 * -glow material to the clickable object
 * 
 * 
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Zoom : MonoBehaviour {
	
	public Material objMaterial; //glow

	private GameObject zoomCamera;
	private Camera mainCamera;
	private float camMoveDuration;

	public Vector3 camPos; //assign each time script is applied
	public string uiName; //assign in editor

	private GameObject animControlsUI; //anim UI
	private GameObject canvasTemp;

	// Use this for initialization
	void Start () {
		objMaterial.SetColor("_OutlineColor", Color.green);

		//establish variables
		mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera>();
		animControlsUI = GameObject.Find("AnimationPanel");
		canvasTemp = GameObject.Find ("Canvas/Temp");
	}
	
	//~~~~ ACTIVATES WHEN BOX COLLIDER CLICKED ~~~~
	void OnMouseDown(){

		//~~~~ CLICKED FIRST TIME ~~~~
			//make sure that you're not already zoomed in
		if(!GameObject.Find ("zoomCamera(Clone)")){
			//neutralize glow color
			objMaterial.SetColor("_OutlineColor", Color.white);

			//load new camera
			Instantiate(Resources.Load ("zoomCamera") as GameObject);

			//hide main camera & any UI up
			mainCamera.enabled = false; 
			DestroyTemp();

			//~~~~ MOVE CAMERA TO TARGET ~~~~
			//calculate time to lerp
			Animator anim = GameObject.Find ("Cylinder").GetComponent<Animator>();
			float fullClipLength = anim.GetCurrentAnimatorStateInfo(0).length;
			float currentClipTime = (anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) * fullClipLength;

			camMoveDuration = fullClipLength - currentClipTime;

			//rotate in reverse if that's closer
			if(currentClipTime/fullClipLength < .5)
			{
				anim.speed = -1;
				camMoveDuration = currentClipTime;
			}

			//spin anim to start point if needed (if it's been paused)
			if(anim.speed == 0 && camMoveDuration > 0)
			{
				anim.speed = 1;
			}
			StartCoroutine(StopAnimAfterLoop(anim, camMoveDuration));

			//hide anim controls
			animControlsUI.SetActive(false);

			//move camera
			GameObject.Find ("zoomCamera(Clone)").GetComponent<LerpCamera>().StartLerp(camMoveDuration, camPos, mainCamera.transform.position);
		}
		//~~~~ CLICKED SECOND TIME (AFTER ZOOMED) ON SCREEN* ~~~~
		else if (uiName == "ComputerUI")// * only for computer screen	
		{
			//load new screen
			mainCamera.GetComponent<SceneLoad>().ChangeScene();

			/*//clicking on the screen while zoomed in, show computer screen
			GameObject ui = Instantiate(Resources.Load("BaraPressScreen") as GameObject);
			ui.transform.SetParent (canvasTemp.transform, false);
			ui.GetComponentInChildren<Button>().onClick.AddListener(this.DestroyTemp);*/
		}
	}
		 
	//~~~~ COROUTINE TO WAIT AFTER ANIMATON HAS FINISHED TO LOAD UI ~~~~
	IEnumerator StopAnimAfterLoop(Animator anim, float timeToWait){

		//wait for rotation anim to finish, then change to idle
		yield return new WaitForSeconds(timeToWait);
		anim.speed = 0;

		//show specific UI
		GameObject ui = Instantiate(Resources.Load (uiName) as GameObject);
		ui.transform.SetParent(canvasTemp.transform, false);
		ui.GetComponentInChildren<Button>().onClick.AddListener(this.ZoomOut);
	}

	//~~~~ ZOOM OUT TO MAIN SCREEN ~~~~
	public void ZoomOut()
	{	
		//move camera
		GameObject zoomCamClone = GameObject.Find ("zoomCamera(Clone)");
		zoomCamClone.GetComponent<LerpCamera>().StartLerp(2, mainCamera.transform.position, camPos);
		StartCoroutine(ResetMainScreen(zoomCamClone, 2));

	}

	//~~~~ HIDE SPECIAL UI AND RESET MAIN SCREEN  ~~~~
	IEnumerator ResetMainScreen(GameObject camera, float timeToWait){
		DestroyTemp();

		Animator anim = GameObject.Find ("Cylinder").GetComponent<Animator>();
		//wait for rotation anim to finish, then change to idle
		yield return new WaitForSeconds(timeToWait);

		//show computer controls
		animControlsUI.SetActive(true);
		anim.speed = 1;
		animControlsUI.GetComponentInChildren<MediaButtonToggles>().ToggleAnimBtnImg(anim);

		mainCamera.enabled = true;
	
		DestroyObject(camera);

		//CHANGE ALL GLOW GREEN
		//objMaterial.SetColor("_OutlineColor", Color.green);
		foreach (Material mat in mainCamera.GetComponent<DrawLines>().glowMaterials)
		{
			mat.SetColor("_OutlineColor", Color.green);
		}

	}

	//~~~~ REMOVE TEMP UI COMPONENTS ~~~~
	public void DestroyTemp(){

		//remove any temp UI on the screen
		int temp = canvasTemp.transform.childCount;
		if(temp > 0 )
		{
			DestroyObject(canvasTemp.transform.GetChild(temp-1).gameObject);
		}

		//remove draw lines
		mainCamera.GetComponent<LineRenderer>().enabled = false;
		mainCamera.GetComponent<DrawLines>().enabled = false;

	}

}
