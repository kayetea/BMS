using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EditorGridRows: MonoBehaviour {

	public Text[] textColArray = new Text[7];
	

	// Use this for initialization
	void Start () {
		int i = 0;
		foreach (Transform child in this.transform) {
			//reset each text element
			child.GetComponentInChildren<Text>().text = null;

			textColArray[i] = child.GetComponentInChildren<Text>();
			i++;
		}
	}
}
