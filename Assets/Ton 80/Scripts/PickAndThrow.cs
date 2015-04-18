using UnityEngine;
using System.Collections;
using DG.Tweening;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class PickAndThrow : MonoBehaviour
{
	public GameObject myo = null;

	private Pose _lastPose = Pose.Unknown;
	private Rigidbody rb;
	private Vector3 velocity;
	private Vector3 oneFrameAgo;

	private FixedJoint joint;

	public ScoreBoard scoreBoard;

	public int state;
	// 0 : unpicked
	// 1 : original dart waiting for clone to be thrown
	// 2 : picked, aiming
	// 3 : thrown
	// 4 : stuck

	void Start() 
	{
		rb = GetComponent<Rigidbody>();
		GameObject.Find("dart").GetComponent<PickAndThrow>().state = 0;
	}

	void Update()
	{
		// Access the ThalmicMyo component attached to the Myo object.
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

		switch(state) {

			case 0 :
				if (thalmicMyo.pose != _lastPose && 
					(thalmicMyo.pose == Pose.Fist || thalmicMyo.pose == Pose.WaveOut) && 
					!GameObject.Find("dart(Clone)")) 
				{
					state = 1;
					GameObject clone = Instantiate(gameObject, transform.position, transform.rotation) as GameObject;
					clone.transform.DOMoveY(0,0.5f).OnComplete(delegate()
						{
							joint = clone.AddComponent<FixedJoint>();
							Rigidbody stickrb = (Rigidbody)GameObject.Find("Stick").GetComponent("Rigidbody");
							joint.connectedBody = stickrb;
							joint.breakForce = 1;
							
							clone.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
							clone.GetComponent<PickAndThrow>().state = 2;
						}
					);
					if (scoreBoard.isBoardFull())
					{
						scoreBoard.emptyBoard();
					}
				}
				break;

			case 1 : 
				break;

			case 2 : 
				if (thalmicMyo.pose != _lastPose && 
					(_lastPose == Pose.Fist || _lastPose == Pose.WaveOut) &&
					velocity.z > 0) 
				{
					GameObject.Destroy(gameObject.GetComponent<FixedJoint>());
					Vector3 v = new Vector3(velocity.x*150,velocity.y*150,velocity.z*400);
					rb.AddForce(v,ForceMode.Impulse);
					rb.useGravity = true;
					state = 3;
				}
				break;

			case 3 : 
				if (velocity.magnitude > 0.75)
				{
					rb.AddForce(velocity.x*(-200),velocity.y*(-200),velocity.z*(-600));
				}
				if (transform.position.z > 3.95 || transform.position.y < -2)
				{
					rb.isKinematic = true;
					name = "dart(Stuck)";
					tag = "Stuck";
					scoreBoard.addDart(transform.position);
					state = 4;
				}
				GameObject.Find("dart").GetComponent<PickAndThrow>().state = 0;
				break;
		}
		
		if (thalmicMyo.pose != Pose.Unknown) {
			_lastPose = thalmicMyo.pose;
		}
	}

	 void FixedUpdate() 
	 {
		velocity = transform.position - oneFrameAgo;
		oneFrameAgo = transform.position;
	 }
}