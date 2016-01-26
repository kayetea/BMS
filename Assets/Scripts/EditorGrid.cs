using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EditorGrid : MonoBehaviour {

	private Text[] textRowArray;
	

	// Use this for initialization
	void Start () {
		foreach (Transform child in this.transform) {
			//reset each text element
			Debug.Log (child.GetComponentInChildren<Text>().text);
			child.GetComponentInChildren<Text>().text = null;

			//textRowArray[].add
			//child.GetComponentInChildren<Text>().text;

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
