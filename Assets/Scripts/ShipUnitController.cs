using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipUnitController : MonoBehaviour {
	public Camera Camera;
	
	public bool canMove = true;

	private NavMeshAgent agent;
	private UnitController unit;
	

	// Use this for initialization
	void Start () {
		unit = GetComponent<UnitController>();
		if(unit == null) {
			Debug.LogError("Ship Unit Controller requires Unit Controller");
		}
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(unit.Selected && canMove && Input.GetMouseButton(1)) {
			var ray = Camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				agent.SetDestination(hit.point);
			}
		}
	}
}
