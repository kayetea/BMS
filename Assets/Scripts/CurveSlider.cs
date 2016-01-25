using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CurveSlider : MonoBehaviour {

	public Slider animSlider;

	public float amplitude = 50;
	public float offset = 0;
	private float phaseShift;

	// Use this for initialization
	void Start () {
		phaseShift = Mathf.PI;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void YPosition(){
		Vector2 temp = animSlider.handleRect.anchoredPosition;
		temp.y = (amplitude * Mathf.Cos((Mathf.PI *(animSlider.value - 0.5f))+ phaseShift)) + offset;
		animSlider.handleRect.anchoredPosition = temp;
	}
}
