using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public GameObject UnitPanel;
	public GameObject[] units;
	public GameObject[] selectedUnits;

	public void SetSelected(GameObject unit) {
		foreach(var oldSelectedUnit in selectedUnits) {
			oldSelectedUnit.GetComponent<UnitController>().Deselect();
		}
		selectedUnits = new [] { unit };
		unit.GetComponent<UnitController>().Select();
		UnitPanel.SetActive(true);
	}

	public void DeselectAll() {
		foreach(var oldSelectedUnit in selectedUnits) {
			oldSelectedUnit.GetComponent<UnitController>().Deselect();
		}
		selectedUnits = new GameObject[] { };
		UnitPanel.SetActive(true);
	}
}
