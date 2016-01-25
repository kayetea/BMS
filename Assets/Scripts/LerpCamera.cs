using UnityEngine;
using System.Collections;

public class LerpCamera : MonoBehaviour {

	public void StartLerp(float lerpSpeed, Vector3 newPosition, Vector3 startingPos){
		StartCoroutine(LerpToPosition(lerpSpeed, newPosition, startingPos));
	}
	
	IEnumerator LerpToPosition(float lerpSpeed, Vector3 newPosition, Vector3 startingPos)
	{
		float t = 0.0f;
		
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / lerpSpeed);
			transform.position = Vector3.Lerp(startingPos, newPosition, t);
			yield return 0;
		}
	}
}