using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
  public UiController UiController;
	public List<PlayerController> Players;
  public PlayerController CurrentPlayer;
  public List<UnitController> SelectedUnits;

  public void SetSelected(UnitController unit)
  {
    SelectedUnits.ForEach(u => u.Deselect());
    SelectedUnits.Clear();
    SelectedUnits.Add(unit);
    unit.Select();
    UiController.ShowUnitPanel(unit);
  }

  public void DeselectAll()
  {
    foreach (var oldSelectedUnit in SelectedUnits)
    {
      oldSelectedUnit.GetComponent<UnitController>().Deselect();
    }
    SelectedUnits.Clear();
    UiController.HideUnitPanel();
  }

  public void GoToCommand()
  {
    UiController.GetPositionFromUser((destination) => SelectedUnits[0].GoTo(destination));
  }

}
