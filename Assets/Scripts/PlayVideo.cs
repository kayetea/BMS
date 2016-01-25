using UnityEngine;
using System.Collections;

public class PlayVideo : MonoBehaviour {

	public string movieName;
	public MovieTexture movie;
	private GameObject movieTexture;

	void Start (){
	
	}

	public void PlayM (){
		Debug.Log ("play movie");
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Handheld.PlayFullScreenMovie(movieName, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFit);
			Debug.Log ("play movie");
        }
		else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer && !movie.isPlaying)
		{
			movieTexture = this.transform.GetChild(0).gameObject;
			movieTexture.SetActive(true);
			movie.Play();
		}
    }

	public void StopMovie(){
		movie.Stop();
		movieTexture.SetActive(false);
	}
}
