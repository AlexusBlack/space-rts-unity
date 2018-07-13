using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public GameObject UnitPanel;
  public GameObject[] units;
  public GameObject[] selectedUnits;

  private bool isSelecting = false;
  private Vector3 selectStartMousePosition;

  public void SetSelected(GameObject unit)
  {
    foreach (var oldSelectedUnit in selectedUnits)
    {
      oldSelectedUnit.GetComponent<UnitController>().Deselect();
    }
    selectedUnits = new[] { unit };
    unit.GetComponent<UnitController>().Select();
    UnitPanel.SetActive(true);
  }

  public void DeselectAll()
  {
    foreach (var oldSelectedUnit in selectedUnits)
    {
      oldSelectedUnit.GetComponent<UnitController>().Deselect();
    }
    selectedUnits = new GameObject[] { };
    UnitPanel.SetActive(false);
  }

  void Update()
  {
    // If we press the left mouse button, save mouse location and begin selection
    if (Input.GetMouseButtonDown(0))
    {
      isSelecting = true;
      selectStartMousePosition = Input.mousePosition;
    }
    // If we let go of the left mouse button, end selection
    if (Input.GetMouseButtonUp(0)) {
      isSelecting = false;
		}
  }

  void OnGUI()
  {
    if( isSelecting )
		{
			// Create a rect from both mouse positions
			var rect = Utils.GetScreenRect(selectStartMousePosition, Input.mousePosition );
			Utils.DrawScreenRect( rect, new Color( 0.8f, 0.8f, 0.95f, 0.25f ) );
			Utils.DrawScreenRectBorder( rect, 2, new Color( 0.8f, 0.8f, 0.95f ) );
		}
  }

	
}
