using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayVideo : MonoBehaviour {

	public string movieName;
	public GameObject movieObj;
	private MovieTexture movie;
	public GameObject movieContainer;

	public bool autoPlay = false;

	void Start (){
		if(autoPlay == true)
		{
			PlayM();
		}

		movie = (MovieTexture)movieObj.GetComponent<RawImage>().texture;
	}

	public void PlayM (){
		Debug.Log ("play movie");
/*for IOS/ mobile
 #IF UNITY_IOS 
		Handheld.PlayFullScreenMovie(movieName, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFit);
		Debug.Log ("play movie");
#endif*/
#if (UNITY_STANDALONE || UNITY_EDITOR)
		movieContainer.SetActive(true);
		movieObj.SetActive (true);
		movie.Play();
#endif
    }

	public void StopMovie(){
		movie.Stop();
		movieObj.SetActive (false);
		movieContainer.SetActive(false);
	}
}
