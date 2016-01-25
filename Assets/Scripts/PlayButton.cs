/*------------------------------------------------------
Project: AR_test
Program: PlayButton.cs
Author: Katelyn Godfrey

Description: When play button is clicked, trigger animation,
change the UI icon, and trigger souund. When clicked again,
stop these functions.

Updated: 05/28/2015 
------------------------------------------------------

ADD TO EACH ANIMATED MODEL THAT IS PARENTED UNDER IMAGE TARGET

ON THE UI PLAY BUTTON ASSOCIATED WITH THE ANIMATED MODEL, 
TELL ON CLICK TO TRIGGER PLAYBUTTON.CLICKED 

ADD THE PLAY BUTTON ASSOCIATED WITH THE MODEL TO THE VARIABLES
AND THE UI SPRITES PLAY_IDLE AND PAUSE_IDLE 

------------------------------------------------------*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour{

	public GameObject playBtn;
	private Image playButtonImg;
	public Sprite playImg;
	public Sprite pauseImg;

	private Animation anim;
	private AudioSource soundFX;

	public bool firstPress = false;

	void Start(){
		anim = GetComponent<Animation>();
		soundFX = GetComponent<AudioSource>();
		playButtonImg = playBtn.GetComponent<Image>();
	}

	public void Clicked(){
		Debug.Log("clicked");

		if (!firstPress){
			//plays animation for first time
			anim.Play ();
			firstPress = true;

			//changes play button to pause button
			playButtonImg.overrideSprite = pauseImg;

			//PLAY SFX, need to have Audio Source on object
			if(soundFX != null)
				soundFX.Play();
		}
		else{
			foreach (AnimationState state in anim){
				//pauses animation at speed 0 and plays it again at speed 1
				if(state.speed > 0){
					//pause anim
					state.speed = 0;

					//change ui to play icon
					playButtonImg.overrideSprite = playImg;
				}
				else{
					//play anim
					state.speed = 1;

					//change ui to pause icon
					playButtonImg.overrideSprite = pauseImg;
				}
			}
		}

	}

}
