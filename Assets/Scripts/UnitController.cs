using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitController : MonoBehaviour {
	public PlayerController Owner;
	public GameObject Selection;
	public bool Selected { get { return selected; } }

	private bool selected;

	public void Select() {
		selected = true;
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
		if(Input.GetMouseButton(0)) {
			Select();
		}
		Owner.SetSelected(gameObject);
	}	

}
