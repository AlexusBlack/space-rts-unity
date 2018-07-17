using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectAllUnitsOnClick : MonoBehaviour {
	public GameController GameController;

	public void OnMouseDown() {
		if(Input.GetMouseButton(0)) {
			GameController.DeselectAll();
		}
	}
}
