using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitController : MonoBehaviour {
	public GameController GameController;
	public PlayerController Owner;

	public GameObject Selection;
	public int Health = 100;
	public int MaxHealth = 100;
	public string Name = "No name";
	public string Type = "None";
	public bool Selectable = true;
	// public ? Icon = "default icon";

	public bool Selected { get { return selected; } }

	private bool selected = false;

	void Start() {
		// Reporting to Owner
		Owner.Units.Add(this);
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

	void OnMouseDown() {
		if(Selectable && Input.GetMouseButton(0)) {
			GameController.SetSelected(this);
		}
	}	

}
