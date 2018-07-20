using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour {
	public GameController GameController;
	public PlayerController Owner;
	public GameObject ButtonsPanel;
	public GameObject Selection;

	public int Health = 100;
	public int MaxHealth = 100;
	public string Name = "No name";
	public string Type = "None";
	public bool CanMove = true;
	// public ? Icon = "default icon";
	public bool Selected { get { return selected; } }

	private NavMeshAgent agent;
	private bool selected = false;

	void Start() {
		// Reporting to Owner
		Owner.Units.Add(this);

		agent = GetComponent<NavMeshAgent>();
	}

	public void Select() {
		selected = true;
		
		// Showing selection indicator
		if(Selection != null) {
			Selection.SetActive(true);
		}
	}

	public void Deselect() {
		selected = false;
		if(Selection != null) {
			Selection.SetActive(false);
		}
	}

	public void GoTo(Vector3 destination) {
		if(CanMove) {
			agent.SetDestination(destination);
		}
	}
}
