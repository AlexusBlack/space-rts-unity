using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour {
	public PlayerController owner;
	public Camera cam;
	public GameObject selection;
	public bool canMove = true;

	private NavMeshAgent agent;
	private bool selected;

	public void Select() {
		selected = true;
		if(selection != null) {
			selection.SetActive(true);
		}
	}

	public void Deselect() {
		selected = false;
		if(selection != null) {
			selection.SetActive(false);
		}
	}

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(selected && canMove && Input.GetMouseButton(1)) {
			var ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				agent.SetDestination(hit.point);
			}
		}
	}

	void OnMouseDown() {
		if(Input.GetMouseButton(0)) {
			Select();
		}
		owner.SetSelected(gameObject);
	}

}
