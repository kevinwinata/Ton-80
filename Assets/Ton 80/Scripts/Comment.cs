using UnityEngine;
using System.Collections;
using Pose = Thalmic.Myo.Pose;

public class Comment : MonoBehaviour
{
	public GameObject myo = null;

	void OnGUI ()
	{
		GUI.skin.label.fontSize = 20;
		Rect labelPos = new Rect(12,Screen.height-30,Screen.width,Screen.height);
		ThalmicHub hub = ThalmicHub.instance;

		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

		if (!hub.hubInitialized) 
		{
			GUI.Label(labelPos,
				"Cannot contact Myo Connect. Is Myo Connect running?\n" +
				"Press Q to try again."
			);
		} 
		else if (!thalmicMyo.isPaired) 
		{
			GUI.Label(labelPos,
				"No Myo currently paired."
			);
		} 
		else if (!thalmicMyo.armSynced) 
		{
			GUI.Label(labelPos,
				"Perform the Sync Gesture."
			);
		} 
		else 
		{
			switch(thalmicMyo.pose) 
			{
				case Pose.Fist:
					GUI.Label(labelPos,"Fist");
					break;
				case Pose.WaveIn:
					GUI.Label(labelPos,"WaveIn");
					break;
				case Pose.WaveOut:
					GUI.Label(labelPos,"WaveOut");
					break;
				case Pose.DoubleTap:
					GUI.Label(labelPos,"DoubleTap");
					break;
				case Pose.FingersSpread:
					GUI.Label(labelPos,"FingersSpread");
					break;
				case Pose.Rest:
					GUI.Label(labelPos,"Rest");
					break;
				default:
					GUI.Label(labelPos,"Unknown");
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
