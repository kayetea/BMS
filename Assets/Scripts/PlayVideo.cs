using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayVideo : MonoBehaviour {

	public string movieName;
	public GameObject movieObj;
	public MovieTexture movie;
	public GameObject movieContainer;
	private GameObject canvas;

	public bool autoPlay = false;

	void Start (){
		canvas = GameObject.Find ("Canvas");

		if(autoPlay == true)
		{
			PlayMSimple();
		}
	}

	public void PlayM(){
		movieContainer.SetActive(true);

		GameObject movieObjI = Instantiate(movieObj);
		movieObjI.transform.SetParent(movieContainer.transform, false);
		//movieObj = movieObjI;
		movieObjI.GetComponent<RawImage>().texture = movie;

		movie.Play();

		//make it so you can click on the video and exit it
		EventTrigger trigger = movieObjI.GetComponent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerClick;
		entry.callback.AddListener( (eventData) => { StopMovie(movieObjI); } );
		trigger.delegates.Add(entry);

		canvas.GetComponent<ScreenFadeOut>().moviePlaying = true;
	}

	private void PlayMSimple(){
		movie.Play ();
	}

	public void StopMovie(GameObject obj){
		movie.Stop ();
		DestroyObject(obj);
		movieContainer.SetActive(false);

		canvas.GetComponent<ScreenFadeOut>().moviePlaying = false;
	}
}
