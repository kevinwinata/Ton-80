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

	public int state;
	// 0 : unpicked
	// 1 : picked, aiming
	// 2 : thrown
	// 3 : stick

	void Start() 
	{
		rb = GetComponent<Rigidbody>();
		state = 0;
	}

	void Update()
	{
		// Access the ThalmicMyo component attached to the Myo object.
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

		switch(state) {
			case 0 :
				if (thalmicMyo.pose != _lastPose && 
					(thalmicMyo.pose == Pose.Fist || thalmicMyo.pose == Pose.WaveOut)) 
				{
					transform.DOMoveY(0,1).OnComplete(delegate()
						{
							joint = gameObject.AddComponent<FixedJoint>();
							Rigidbody stickrb = (Rigidbody)GameObject.Find("Stick").GetComponent("Rigidbody");
							joint.connectedBody = stickrb;
							joint.breakForce = 1;
							
							transform.position = new Vector3(transform.position.x, 0, transform.position.z);
							state = 1;
						});
				}
				break;
			case 1 : 
				if (thalmicMyo.pose != _lastPose && 
					(_lastPose == Pose.Fist || _lastPose == Pose.WaveOut) &&
					velocity.z > 0) 
				{
					rb.AddForce(velocity.x*300,velocity.y*300,velocity.z*800);
					rb.useGravity = true;
					state = 2;
				}
				break;
			case 2 : 
				if (velocity.magnitude > 0.75)
				{
					rb.AddForce(velocity.x*(-200),velocity.y*(-200),velocity.z*(-600));
				}
				if (transform.position.z > 5.23)
				{
					rb.isKinematic = true;
					state = 3;
				}
				break;
			case 3 : 
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