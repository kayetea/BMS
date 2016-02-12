using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayVideo : MonoBehaviour {

	public string movieName;
	public GameObject movieObj;
	public MovieTexture movie;
	public GameObject movieContainer;

	public bool autoPlay = false;

	void Start (){
		if(autoPlay == true)
		{
			PlayM();
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
	}

	public void StopMovie(GameObject obj){
		movie.Stop ();
		DestroyObject(obj);
		movieContainer.SetActive(false);
	}
}
