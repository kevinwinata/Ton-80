using UnityEngine;
using System.Collections;
using Pose = Thalmic.Myo.Pose;

public class Comment : MonoBehaviour
{
	public GameObject myo = null;
	public ScoreBoard scoreBoard;

	void OnGUI ()
	{
		GUI.skin.label.fontSize = 20;

		ThalmicHub hub = ThalmicHub.instance;

		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

		if (!hub.hubInitialized) 
		{
			GUI.Label(new Rect(12,8,Screen.width,Screen.height),
				"Cannot contact Myo Connect. Is Myo Connect running?\n" +
				"Press Q to try again."
			);
		} 
		else if (!thalmicMyo.isPaired) 
		{
			GUI.Label(new Rect(12,8,Screen.width,Screen.height),
				"No Myo currently paired."
			);
		} 
		else if (!thalmicMyo.armSynced) 
		{
			GUI.Label(new Rect(12,8,Screen.width,Screen.height),
				"Perform the Sync Gesture."
			);
		} 
		else 
		{
			switch(thalmicMyo.pose) 
			{
				case Pose.Fist:
					GUI.Label(new Rect(12,8,Screen.width,Screen.height),"Fist");
					break;
				case Pose.WaveIn:
					GUI.Label(new Rect(12,8,Screen.width,Screen.height),"WaveIn");
					break;
				case Pose.WaveOut:
					GUI.Label(new Rect(12,8,Screen.width,Screen.height),"WaveOut");
					break;
				case Pose.DoubleTap:
					GUI.Label(new Rect(12,8,Screen.width,Screen.height),"DoubleTap");
					break;
				case Pose.FingersSpread:
					GUI.Label(new Rect(12,8,Screen.width,Screen.height),"FingersSpread");
					break;
				case Pose.Rest:
					GUI.Label(new Rect(12,8,Screen.width,Screen.height),"Rest");
					break;
				default:
					GUI.Label(new Rect(12,8,Screen.width,Screen.height),"Unknown");
					break;
			}
		}

		string comment;
		switch(scoreBoard.commentIdx) 
		{
			case 0 : 
				switch(Random.Range(0,6)) 
				{
					case 0 : 
						comment = "What about you join the Stormtroopers?";
						break;
					case 1 : 
						comment = "wow  much accurate  such skill  many score";
						break;
					case 2 : 
						comment = "Y U No hit";
						break;
					case 3 : 
						comment = "Woah! You just killed a mosquito!";
						break;
					case 4 : 
						comment = "Should I bring that dartboard closer to your face?";
						break;
					case 5 : 
						comment = "You had one job, man. One job.";
						break;
					case 6 : 
						comment = "So close, yet so far.";
						break;
				}
				break;
			case 1 :
				switch(Random.Range(0,1)) 
				{
					case 0 : 
						comment = "It's a bird! It's a plane! No, IT'S THE DART YOU THREW";
						break;
					case 1 : 
						comment = "The force is strong in this one.";
						break;
				}
				break;
			case 2 :
				switch(Random.Range(0,3)) 
				{
					case 0 : 
						comment = "";
						break;
					case 1 : 
						comment = "";
						break;
					case 2 : 
						comment = "";
						break;
					case 3 : 
						comment = "";
						break;
				}
				break;
			case 3 :
				switch(Random.Range(0,1)) 
				{
					case 0 : 
						comment = "Rheumatics? At your age?";
						break;
					case 1 : 
						comment = "Do you even lift bro?";
						break;
				}
				break;
			case 4 :
				switch(Random.Range(0,2)) 
				{
					case 0 : 
						comment = "Keep Calm and Wipe Your Hands";
						break;
					case 1 : 
						comment = "WATCH YOUR FEET, DART INCOMING";
						break;
					case 2 : 
						comment = "Well, I guess the chance is just.. slipped out of your hands. YEEAAAAHH";
						break;
				}
				break;
		}
		GUI.Label(new Rect(12,Screen.height-20,Screen.width,Screen.height),comment);
	}

	void Update ()
	{
		ThalmicHub hub = ThalmicHub.instance;

		if (Input.GetKeyDown ("q")) {
			hub.ResetHub();
		}
	}
}
