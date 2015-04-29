using UnityEngine;
using System.Collections;
using Pose = Thalmic.Myo.Pose;

public class Comment : MonoBehaviour
{
	public GameObject myo = null;

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
	}

	void Update ()
	{
		ThalmicHub hub = ThalmicHub.instance;

		if (Input.GetKeyDown ("q")) {
			hub.ResetHub();
		}
	}
}
