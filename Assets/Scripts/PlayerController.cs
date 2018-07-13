using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public GameObject UnitPanel;
  public List<GameObject> units;
  public List<GameObject> selectedUnits;

  public void SetSelected(GameObject unit)
  {
    foreach (var oldSelectedUnit in selectedUnits)
    {
      oldSelectedUnit.GetComponent<UnitController>().Deselect();
    }
		selectedUnits.Clear();
    selectedUnits.Add(unit);
    unit.GetComponent<UnitController>().Select();
    UnitPanel.SetActive(true);
  }

  public void DeselectAll()
  {
    foreach (var oldSelectedUnit in selectedUnits)
    {
      oldSelectedUnit.GetComponent<UnitController>().Deselect();
    }
    selectedUnits.Clear();
    UnitPanel.SetActive(false);
  }
}
