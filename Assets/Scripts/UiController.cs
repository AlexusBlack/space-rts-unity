using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiController : MonoBehaviour {
	public GameController GameController;
	public StatsPanelUi UnitPanel;
  public UIResourcesPanel ResourcesPanel;
  public List<GameObject> ButtonPanels;

	private PlayerController CurrentPlayer;
	private bool PlayerSelectingPosition = false;
	private bool LeftMouseDown = false;
	private Vector3 LeftMouseDownPosition;
	private bool boxSelectionActive = false;
	private Rect boxSelectionRect;
  private event EventHandler PlayerPositionSelected = delegate { };

	// Use this for initialization
	void Start () {
		CurrentPlayer = GameController.CurrentPlayer;
		renderCurrentPlayerResources();
    GameController.CurrentPlayer.ResourcesAmountChanged += (o, e) => renderCurrentPlayerResources();
	}
	
	private void renderCurrentPlayerResources()
  {
    ResourcesPanel.DilithiumAmountText.text = "Dilithium: " + CurrentPlayer.Dilithium;
    ResourcesPanel.MetalAmountText.text = "Metal: " + CurrentPlayer.Metal;
  }

  void Update()
  {
		HandleLeftMouseButton();
		HandleRightMouseButton();
  }

	void HandleLeftMouseButton() {
		// If UI element click, doing nothing
		if(EventSystem.current.IsPointerOverGameObject()) return;

		if (Input.GetMouseButtonDown(0))
		{
			LeftMouseDown = true;
			LeftMouseDownPosition = Input.mousePosition;

			// Handling player position selection mode
			if (PlayerSelectingPosition)
			{
				PlayerPositionSelected(this, EventArgs.Empty);
			} 
			else // no special selection mode
			{
				// check if hit unit, check if hit UI, if not, deselect all
				var clickedUnit = ClickHitUnit();
				if(clickedUnit == null) {
					GameController.DeselectAll();
				} else {
					GameController.SetSelected(clickedUnit);
				}
			}
		}

		if(LeftMouseDown) {
			boxSelectionRect = Utils.GetScreenRect(LeftMouseDownPosition, Input.mousePosition);
			// if selection area is big enough, starting box selection mode
			if (boxSelectionRect.width * boxSelectionRect.height > 0.5 * 0.5) {
				boxSelectionActive = true;
			} else {
				boxSelectionActive = false;
			}
		} 
		
		if(Input.GetMouseButtonUp(0)) 
		{
			LeftMouseDown = false;
			boxSelectionActive = false;
		}
	}

	void HandleRightMouseButton() {
		if (Input.GetMouseButtonDown(1))
		{
			// If there are selected units, sending them to the click location
			if(GameController.SelectedUnits.Count > 0) {
				var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit)) {
					GameController.SelectedUnits.ForEach(u => u.GoTo(hit.point));
				}
			}
		}
	}

  void OnGUI()
  {
    // Drawing box in box selection mode
		if (boxSelectionActive)
    {
      Utils.DrawScreenRect(boxSelectionRect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
      Utils.DrawScreenRectBorder(boxSelectionRect, 2, new Color(0.8f, 0.8f, 0.95f));

			// And selecting units that are in boundaries of the box
      SelectPlayerUnitsInSelectionBox();
    }
  }

	void SelectPlayerUnitsInSelectionBox()
  {
    // box selection works only for user units
    foreach (var unit in CurrentPlayer.Units)
    {
      var selected = GameController.SelectedUnits.Contains(unit);
      // if state of unit doesn't match current state
      if (Utils.IsWithinSelectionBounds(unit.gameObject, LeftMouseDownPosition) != selected)
      {
        // matching it
        if (selected)
        {
          GameController.SelectedUnits.Remove(unit);
          unit.Deselect();
        }
        else
        {
          GameController.SelectedUnits.Add(unit);
          unit.Select();
        }

        // when only one unit selected showing panel for it
        if (GameController.SelectedUnits.Count == 1)
        {
          ShowUnitPanel(GameController.SelectedUnits[0]);
        }
        else
        {
          HideUnitPanel();
        }
      }
    }
  }

	public void GetPositionFromUser(Action<Vector3> callback)
  {
    PlayerSelectingPosition = true;
    EventHandler handler = null;
    handler = (o, e) =>
    {
      var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit))
      {
        PlayerPositionSelected -= handler;
        PlayerSelectingPosition = false;
        callback(hit.point);
      }
    };
    PlayerPositionSelected += handler;
  }

	private UnitController ClickHitUnit() {
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			if (hit.collider != null) {
				if(hit.collider.gameObject.tag == "Unit") return hit.collider.gameObject.GetComponent<UnitController>();
			}
		}
		return null;
	}

  public void ShowUnitPanel(UnitController unit)
  {
    if (UnitPanel == null)
    {
      Debug.LogError("No unit panel");
      return;
    }

    UnitPanel.TypeText.text = unit.Type;
    UnitPanel.NameText.text = unit.Name;
    UnitPanel.OwnerText.text = unit.Owner.name;
    UnitPanel.HealthText.text = "Health: " + unit.Health + "/" + unit.MaxHealth;

    UnitPanel.gameObject.SetActive(true);

    if (unit.Owner == CurrentPlayer)
    {
      var panel = unit.ButtonsPanel;
      SetActiveButtonsPanel(panel);
    }
    else
    {
      HideButtonPanels();
    }
  }

  public void HideUnitPanel()
  {
    UnitPanel.gameObject.SetActive(false);
    HideButtonPanels();
  }

  private void SetActiveButtonsPanel(GameObject panel)
  {
    ButtonPanels.ForEach(p => p.SetActive(p == panel));
  }

  private void HideButtonPanels()
  {
    ButtonPanels.ForEach(p => p.SetActive(false));
  }
}
