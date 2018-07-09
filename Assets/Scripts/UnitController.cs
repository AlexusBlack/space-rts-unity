using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour {
	public Camera camera;
	public GameObject selection;
	public bool canMove = true;

	private NavMeshAgent agent;
	private bool selected;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(selected && canMove && Input.GetMouseButton(0)) {
			var ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				agent.SetDestination(hit.point);
			}
		}
	}


	void OnMouseDown() {
		if(selection != null) {
			selection.SetActive(true);
			selected = true;
		}
	}
}
