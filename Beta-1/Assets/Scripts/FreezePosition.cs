using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePosition : MonoBehaviour {
	private Vector3 StartPosition;

	// Use this for initialization
	void Start () {
		StartPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = StartPosition;
	}
}
