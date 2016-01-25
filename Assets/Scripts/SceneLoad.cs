/*------------------------------------------------------

------------------------------------------------------*/

using UnityEngine;
using System.Collections;

public class SceneLoad : MonoBehaviour {

	public string sceneName;

	// Use this for initialization
	void Start () {
	
	}
	
	public void Exit() {
		Application.Quit(); 
	}

	public void ChangeScene(){
		Application.LoadLevel(sceneName);
		Resources.UnloadUnusedAssets ();
	}

	public void OnMouseDown(){
		Application.LoadLevel(sceneName);
		Resources.UnloadUnusedAssets ();
	}
}
