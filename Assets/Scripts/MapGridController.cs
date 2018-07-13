using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGridController : MonoBehaviour {
	public PlayerController Owner;

	private bool isSelecting = false;
  private Vector3 selectStartMousePosition;

	void OnMouseDown() {
		if(Input.GetMouseButton(0)) {
			isSelecting = true;
      selectStartMousePosition = Input.mousePosition;
		}
	}

	void Update() {
		// If we let go of the left mouse button, end selection
    if (Input.GetMouseButtonUp(0))
    {
      isSelecting = false;
    }
	}

	void OnGUI()
  {
    if (isSelecting)
    {
      // Create a rect from both mouse positions
      var rect = Utils.GetScreenRect(selectStartMousePosition, Input.mousePosition);
      Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
      Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));

			SelectUnitsInSelectionBox();
    }
  }

	void SelectUnitsInSelectionBox() {
		foreach(var unit in Owner.units) {
			var selected = Owner.selectedUnits.Contains(unit);
			if(Utils.IsWithinSelectionBounds(unit, selectStartMousePosition) != selected) {
				if(selected) {
					Owner.selectedUnits.Remove(unit);
					unit.GetComponent<UnitController>().Deselect();
				} else {
					Owner.selectedUnits.Add(unit);
					unit.GetComponent<UnitController>().Select();
				}
			}
		}
	}
}
