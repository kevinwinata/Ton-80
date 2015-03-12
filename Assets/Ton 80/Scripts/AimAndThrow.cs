using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class AimAndThrow : MonoBehaviour
{
	public GameObject myo = null;

	private Pose _lastPose = Pose.Unknown;
	private Rigidbody rb;
	private Vector3 velocity;
	private Vector3 oneFrameAgo;

	void Start() 
	{
        rb = GetComponent<Rigidbody>();
    }

	void Update ()
	{
		// Access the ThalmicMyo component attached to the Myo object.
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
		if (thalmicMyo.pose != _lastPose && 
			(_lastPose == Pose.Fist || _lastPose == Pose.WaveIn) &&
			velocity.z > 0) {
			//rb.velocity = new Vector3(velocity.x*10,velocity.y*10,velocity.z*10);
			rb.AddForce(velocity.x*300,velocity.y*300,velocity.z*800);
			rb.useGravity = true;
		}
		if (thalmicMyo.pose != Pose.Rest && thalmicMyo.pose != Pose.Unknown) {
			_lastPose = thalmicMyo.pose;
		}
	}

	 void FixedUpdate () 
	 {
	     velocity = transform.position - oneFrameAgo;
	     oneFrameAgo = transform.position;
	     if (velocity.magnitude > 0.75)
	     {
	     	rb.AddForce(velocity.x*(-200),velocity.y*(-200),velocity.z*(-600));
	     }
	     if (transform.position.z > 5.23)
	     {
	     	rb.isKinematic = true;
	     }
	 }
}