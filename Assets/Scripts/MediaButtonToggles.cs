using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MediaButtonToggles : MonoBehaviour {

	public float animSpeed = 1;
	public Sprite pauseImg;
	public Sprite playImg;

	public void ToggleVisibility(GameObject toggleObject)
	{
		Debug.Log ("enter toggle visibility");
		if (toggleObject.activeSelf)
        {
            Debug.Log("turn off");
			toggleObject.SetActive(false);
        }
		else if (!toggleObject.activeInHierarchy)
        {
            Debug.Log("turn on");
			toggleObject.SetActive(true);
        }
    }

    public void ToggleAnimation(GameObject toggleObject)
    {
		if(toggleObject.GetComponent<Animator>()){
			Animator anim = toggleObject.GetComponent<Animator>();
			if(anim.speed == 0)
			{
				anim.speed = animSpeed;
				ToggleAnimBtnImg(anim);
			}
			else{
				anim.speed = 0;
				ToggleAnimBtnImg(anim);
			}
		}
		//check of animation component
		/*else if(toggleObject.GetComponent<Animation>()){
			foreach (AnimationState state in toggleObject.GetComponent<Animation>()){
				//pauses animation at speed 0 and plays it again at speed 1
				if(state.speed > 0){
					//pause anim
					state.speed = 0;
				}
				else{
					//play anim
					state.speed = 1;
				}
			}
		}
		*/
    }

	public void ToggleAnimBtnImg(Animator anim)
	{
		if(anim.speed == 0)
		{
			//just paused the anim, show pause button and enable button
			this.GetComponent<Image>().sprite = pauseImg;
			//this.GetComponent<Button>().enabled = true;
		}
		else
		{
			//just re-start animation, show play buttona nd disable button 
			this.GetComponent<Image>().sprite = playImg;
			//this.GetComponent<Button>().enabled = false;
		}
	}
}