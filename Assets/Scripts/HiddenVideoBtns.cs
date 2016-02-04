using UnityEngine;
using System.Collections;

public class HiddenVideoBtns : MonoBehaviour {

	public void MouseEnter(){
		GetComponent<Animator>().Play("hoverRollOut");
	}

	public void MouseExit(){
		GetComponent<Animator>().Play("rollExit");
	}
}
