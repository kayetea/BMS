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
using System.Diagnostics;

public class Zoom : MonoBehaviour {
	
	public Material objMaterial; //glow

	private GameObject zoomCamera;
	private Camera mainCamera;
	private float camMoveDuration;

	private Vector3 camPos; 
	public Transform camTrans; //assign for each
	public string uiName; //assign in editor

	private GameObject animControlsUI; //anim UI
	private GameObject canvas;
	private GameObject canvasTemp;
	private Animator gurneyAnim;

	// Use this for initialization
	void Start () {
		objMaterial.SetColor("_OutlineColor", Color.green);

		//establish variables
		canvas = GameObject.Find ("Canvas");
		mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera>();
		animControlsUI = GameObject.Find("AnimationPanel");
		canvasTemp = GameObject.Find ("Canvas/Temp");
		gurneyAnim = GameObject.Find("Cylinder/baramed_select").GetComponent<Animator>();
		camPos = camTrans.position;
	}
	
	//~~~~ ACTIVATES WHEN BOX COLLIDER CLICKED ~~~~
	void OnMouseDown(){

		//~~~~ CLICKED FIRST TIME ~~~~
			//make sure that you're not already zoomed in
		if(!GameObject.Find ("zoomCamera(Clone)") && !canvas.GetComponent<ScreenFadeOut>().moviePlaying){

			//neutralize glow color
			foreach (Material mat in mainCamera.GetComponent<DrawLines>().glowMaterialsGreen)
			{
				mat.SetColor("_OutlineColor", Color.white);
			}
			foreach (Material mat in mainCamera.GetComponent<DrawLines>().glowMaterialsBlue)
			{
				mat.SetColor("_OutlineColor", Color.white);
			}

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
			GameObject.Find ("zoomCamera(Clone)").GetComponent<LerpCamera>().StartLerp(camMoveDuration, camPos, mainCamera.transform.position, camTrans.rotation, mainCamera.transform.rotation);

		}
		//~~~~ CLICKED SECOND TIME (AFTER ZOOMED) ON SCREEN* ~~~~
		else if (uiName == "ComputerUI")// * only for computer screen	
		{
			//launch demo exe
			System.Diagnostics.Process.Start (Application.dataPath + "/Demo/baramed.exe");

			//STILL NEED TO MAKE IT SO THAT THERE IS A NEUTRAL BACKGROUND TO CLICK BACK TO
			GameObject ui = Instantiate(Resources.Load ("DemoUI") as GameObject);
			ui.transform.SetParent(canvasTemp.transform, false);
			ui.GetComponentInChildren<Button>().onClick.AddListener(this.DestroyTemp);

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

		//fade in graphics
		Graphic[] graphics = ui.GetComponentsInChildren<Graphic>();
		
		for (int i = 0; i < graphics.Length; i++)
		{
			graphics[i].CrossFadeAlpha(0, 0, false); 
			graphics[i].CrossFadeAlpha(1, .5f, false);
		}

		//if entrance, pause gurney anim
		if(uiName == "EntranceUI")
		{
			gurneyAnim.Play("gurneyAnim", -1, .4f);
			gurneyAnim.speed = 0;
		}
		else if (uiName == "ComputerUI")
		{
			gurneyAnim.Play("gurneyAnim", -1, .1f);
			gurneyAnim.speed = 0;
		}
	}
	
	//~~~~ ZOOM OUT TO MAIN SCREEN ~~~~
	public void ZoomOut()
	{	  
		//move camera
		GameObject zoomCamClone = GameObject.Find ("zoomCamera(Clone)");
		zoomCamClone.GetComponent<LerpCamera>().StartLerp(2, mainCamera.transform.position, camPos, mainCamera.transform.rotation, camTrans.rotation);
		StartCoroutine(ResetMainScreen(zoomCamClone, 2));
	}

	//~~~~ HIDE SPECIAL UI AND RESET MAIN SCREEN  ~~~~
	IEnumerator ResetMainScreen(GameObject camera, float timeToWait){
		DestroyTemp();

		Animator anim = GameObject.Find ("Cylinder").GetComponent<Animator>();
		//wait for rotation anim to finish, then change to idle
		yield return new WaitForSeconds(timeToWait);

		if (gurneyAnim.speed == 0)
		{
			gurneyAnim.speed = 1;
		}

		//show computer controls
		animControlsUI.SetActive(true);

		//fade in
		Graphic[] graphics2 = animControlsUI.GetComponentsInChildren<Graphic>();
		
		for (int i = 0; i < graphics2.Length; i++) 
		{
			graphics2[i].CrossFadeAlpha(0, 0, false); 
			graphics2[i].CrossFadeAlpha(1, .5f, false);
		}
	
		anim.speed = 1;
		animControlsUI.GetComponentInChildren<MediaButtonToggles>().ToggleAnimBtnImg(anim);

		mainCamera.enabled = true;
	
		DestroyObject(camera);

		//CHANGE ALL GLOW GREEN/Blue
		foreach (Material mat in mainCamera.GetComponent<DrawLines>().glowMaterialsGreen)
		{
			mat.SetColor("_OutlineColor", Color.green);
		}
		foreach (Material mat in mainCamera.GetComponent<DrawLines>().glowMaterialsBlue)
		{
			mat.SetColor("_OutlineColor", Color.cyan);
		}
	}

	//~~~~ REMOVE TEMP UI COMPONENTS ~~~~
	public void DestroyTemp(){
		int temp = canvasTemp.transform.childCount;

		//remove any temp UI on the screen
		if(temp > 0 )
		{
			DestroyObject(canvasTemp.transform.GetChild(temp-1).gameObject);
		}

		//remove draw lines from screen if they're there
		mainCamera.GetComponent<LineRenderer>().enabled = false;
		mainCamera.GetComponent<DrawLines>().enabled = false;
	}

}
