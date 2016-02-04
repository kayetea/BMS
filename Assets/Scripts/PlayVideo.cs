using UnityEngine;
using System.Collections;

public class PlayVideo : MonoBehaviour {

	public string movieName;
	public MovieTexture movie;
	public GameObject movieTexture;

	public bool autoPlay = false;

	void Start (){
		if(autoPlay == true)
		{
			PlayM();
		}
	}

	public void PlayM (){
		Debug.Log ("play movie");
/*#IF UNITY_IOS 
		Handheld.PlayFullScreenMovie(movieName, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFit);
		Debug.Log ("play movie");
#endif*/
#if (UNITY_STANDALONE || UNITY_EDITOR)
		movieTexture.SetActive(true);
		movie.Play();
#endif
    }

	public void StopMovie(){
		movie.Stop();
		movieTexture.SetActive(false);
	}
}
