using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderMover : MonoBehaviour {

	public Slider slider;
	public float moveRate = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	public void TranslateImage ()
	{
		float value = slider.value;
		Vector2 moveX = new Vector2 (this.transform.localPosition.x, this.transform.localPosition.y);
		moveX.x = value * moveRate;
		this.transform.localPosition = moveX;
	}
}
