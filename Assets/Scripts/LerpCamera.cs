using UnityEngine;
using System.Collections;

public class LerpCamera : MonoBehaviour {

	public void StartLerp(float lerpSpeed, Vector3 newPosition, Vector3 startingPos, Quaternion newRotation, Quaternion startingRotation){
		StartCoroutine(LerpToPosition(lerpSpeed, newPosition, startingPos, newRotation, startingRotation));
	}
	
	IEnumerator LerpToPosition(float lerpSpeed, Vector3 newPosition, Vector3 startingPos, Quaternion newRotation, Quaternion startingRotation)
	{
		float t = 0.0f;
		
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / lerpSpeed);
			transform.position = Vector3.Lerp(startingPos, newPosition, t);
			transform.rotation = Quaternion.Lerp (startingRotation, newRotation, t);
			yield return 0;
		}
	}
}