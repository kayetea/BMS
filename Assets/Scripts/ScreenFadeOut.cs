using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenFadeOut : MonoBehaviour {

	public GameObject screenGO;
	private Image screenImg;
	public bool moviePlaying = true;

	// Use this for initialization
	void Start () {
		screenGO.SetActive(true);

		screenImg = screenGO.GetComponent<Image>();
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeOut(){
		float t = 1f;

		screenImg.CrossFadeAlpha(0, t, false);
		yield return new WaitForSeconds(t);
		moviePlaying = false;
		screenGO.SetActive(false);
	}

}
