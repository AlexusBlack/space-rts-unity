using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGridController : MonoBehaviour {
	public PlayerController Owner;

	void OnMouseDown() {
		if(Input.GetMouseButton(0)) {
			Owner.DeselectAll();
		}
	}
}
