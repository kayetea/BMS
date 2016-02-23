using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ModelRotationSlider : MonoBehaviour {

	public Slider mainSlider;
	public GameObject animatedModel;
	private Animator anim;
	public bool mouseDown = false;
	public GameObject handle;
	private float sliderValue;

	// Use this for initialization
	void Start () {
		anim = animatedModel.GetComponent<Animator>();
	}

	//Invoked when the value of the slider changes
	public void ValueChangeCheck()
	{
		if(mouseDown){
			anim.Play("CylinderRotation", 0, mainSlider.value);
		}
	}

	void LateUpdate() {
		if(!mouseDown){
			mainSlider.normalizedValue = anim.GetCurrentAnimatorStateInfo(0).normalizedTime%1;
		}
	}

	public void MouseDown(){
		mouseDown = true;
		sliderValue = mainSlider.value;
	}

	public void MouseUp(){
		mouseDown = false;

		//check if slider has been dragged
		if (Mathf.Abs(sliderValue - mainSlider.value) > .005f)
		{
			//freeze anim
			anim.speed = 0;
			
			//change button & activate button
			handle.GetComponent<MediaButtonToggles>().ToggleAnimBtnImg(anim);
		}
		else
		{
			//freeze anim
			anim.speed = 1;
			
			//change button & activate button
			handle.GetComponent<MediaButtonToggles>().ToggleAnimBtnImg(anim);
		}

	}
}
