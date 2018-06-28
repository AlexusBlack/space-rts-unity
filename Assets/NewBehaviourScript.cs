using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	public uint MaxVelocity = 50;
	public float MaxForce = 1;
	public uint MaxSpeed = 50;
	public float Mass = 1;
	//public Vector3 target = new Vector3(0,0,0);
	public GameObject target;

	private uint? lastTimestamp = null;
	private Vector3 startPosition = new Vector3(0,0,0);
	private Vector3 desiredVelocity = new Vector3(0,0,0);
	private Vector3 velocity = new Vector3(100, 0, 0);
	private Vector3 steering = new Vector3(0,0,0);

	// Use this for initialization
	private void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	private void Update () {
		var fraction = Time.deltaTime;

		var seekSteering = seek(target.transform.position);
		steering = seekSteering;
		velocity = velocity + steering;
		velocity = Vector3.ClampMagnitude(velocity, MaxSpeed);

		//transform.SetPositionAndRotation(transform.position + velocity * fraction, new Quaternion());
		transform.Translate(velocity * fraction, Space.World);
		transform.rotation = Quaternion.LookRotation(velocity);
		//transform.Rotate(5 * fraction, 0, 0);
		//transform.LookAt(transform.position + velocity);

		if(Vector3.Distance(transform.position, target.transform.position) < 2*2) Reset();

		visualize(transform.position, velocity, desiredVelocity, steering);
	}

	public void Reset() {
		this.transform.SetPositionAndRotation(startPosition, new Quaternion());
		this.velocity = new Vector3(100, 0, 0);
	}

	private Vector3 seek(Vector3 target) {
		var _desiredVelocity = Vector3.Normalize(target - transform.position) * MaxVelocity;
		var _steering = _desiredVelocity - velocity;
		_steering = Vector3.ClampMagnitude(_steering, MaxForce);
		_steering = _steering / Mass;

		desiredVelocity = _desiredVelocity;
		
		return _steering;
	}

	private void visualize(Vector3 position, Vector3 velocity, Vector3 desiredVelocity, Vector3 steering) {
		Debug.DrawLine(position, position + velocity, Color.green);
		Debug.DrawLine(position, position + desiredVelocity, Color.gray);
		Debug.DrawLine(position + velocity, position + steering, Color.blue);
	}
}
