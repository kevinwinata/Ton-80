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

		if (!hub.hubInitialized) {
			GUI.Label(new Rect (12, 8, Screen.width, Screen.height),
				"Cannot contact Myo Connect. Is Myo Connect running?\n" +
				"Press Q to try again."
			);
		} else if (!thalmicMyo.isPaired) {
			GUI.Label(new Rect (12, 8, Screen.width, Screen.height),
				"No Myo currently paired."
			);
		} else if (!thalmicMyo.armSynced) {
			GUI.Label(new Rect (12, 8, Screen.width, Screen.height),
				"Perform the Sync Gesture."
			);
		} else {
			string str;
			switch(thalmicMyo.pose) {
				case Pose.Fist:
					str = "Fist";
					break;
				case Pose.WaveIn:
					str = "WaveIn";
					break;
				case Pose.WaveOut:
					str = "WaveOut";
					break;
				case Pose.DoubleTap:
					str = "DoubleTap";
					break;
				case Pose.FingersSpread:
					str = "FingersSpread";
					break;
				case Pose.Rest:
					str = "Rest";
					break;
				default:
					str = "Unknown";
					break;
			}
			GUI.Label (new Rect (12, 8, Screen.width, Screen.height),
				// "Fist: Vibrate Myo armband\n" +
				// "Wave in: Set box material to blue\n" +
				// "Wave out: Set box material to green\n" +
				// "Double tap: Reset box material\n" +
				// "Fingers spread: Set forward direction"
				str
			);
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
