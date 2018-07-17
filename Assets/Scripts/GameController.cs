using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public List<PlayerController> Players;
	public PlayerController CurrentPlayer;
	public List<UnitController> SelectedUnits;
	public StatsPanelUi UnitPanel;
	public UIResourcesPanel ResourcesPanel;

	private bool isSelecting = false;
  private Vector3 selectStartMousePosition;

	void Start() {
		renderCurrentPlayerResources();
		CurrentPlayer.ResourcesAmountChanged += (o, e) => renderCurrentPlayerResources();
	}

  private void renderCurrentPlayerResources()
  {
    ResourcesPanel.DilithiumAmountText.text = "Dilithium: " + CurrentPlayer.Dilithium;
    ResourcesPanel.MetalAmountText.text = "Metal: " + CurrentPlayer.Metal;
  }

  void Update() {
		// If we let go of the left mouse button, end selection
		if(Input.GetMouseButtonDown(0)) {
			isSelecting = true;
      selectStartMousePosition = Input.mousePosition;
		} else if (Input.GetMouseButtonUp(0))
    {
      isSelecting = false;
			if(SelectedUnits.Count == 0) UnitPanel.gameObject.SetActive(false);
    }
	}

	void OnGUI()
  {
    if (isSelecting)
    {
      // Create a rect from both mouse positions
      var rect = Utils.GetScreenRect(selectStartMousePosition, Input.mousePosition);
			// Ignoring small selections
			if(rect.width * rect.height < 0.5 * 0.5) return;

      Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
      Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));

			SelectUnitsInSelectionBox();
    }
  }

	void SelectUnitsInSelectionBox() {
		// box selection works only for user units
		foreach(var unit in CurrentPlayer.Units) {
			var selected = SelectedUnits.Contains(unit);
			if(Utils.IsWithinSelectionBounds(unit.gameObject, selectStartMousePosition) != selected) {
				if(selected) {
					SelectedUnits.Remove(unit);
					unit.Deselect();
				} else {
					SelectedUnits.Add(unit);
					unit.Select();
				}
			}
		}
	}

	public void SetSelected(UnitController unit)
  {
    SelectedUnits.ForEach(u => u.Deselect());
		SelectedUnits.Clear();
		SelectedUnits.Add(unit);
		unit.Select();
		ShowUnitPanel(unit);
  }

	public void DeselectAll()
  {
    foreach (var oldSelectedUnit in SelectedUnits)
    {
      oldSelectedUnit.GetComponent<UnitController>().Deselect();
    }
    SelectedUnits.Clear();
    UnitPanel.gameObject.SetActive(false);
  }

	private void ShowUnitPanel(UnitController unit) {
		if(UnitPanel == null) {
			Debug.LogError("No unit panel");
			return;
		}
		
		UnitPanel.TypeText.text = unit.Type;
		UnitPanel.NameText.text = unit.Name;
		UnitPanel.OwnerText.text = unit.Owner.name;
		UnitPanel.HealthText.text = "Health: " + unit.Health + "/" + unit.MaxHealth;

		UnitPanel.gameObject.SetActive(true);
	}

  
}
