using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerUnitController : MonoBehaviour {
	public bool canMove = true;
	public GameObject ButtonsPanel;

	private NavMeshAgent agent;
	private UnitController unit;
	private Camera camera;

	// Use this for initialization
	void Start () {
		camera = Camera.main;
		unit = GetComponent<UnitController>();
		if(unit == null) {
			Debug.LogError("Ship Unit Controller requires Unit Controller");
		}
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(unit.Selected && canMove && Input.GetMouseButton(1)) {
			var ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				agent.SetDestination(hit.point);
			}
		}
	}
}
