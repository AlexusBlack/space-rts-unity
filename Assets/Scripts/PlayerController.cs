using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public GameController GameController;
  public List<UnitController> Units;

  public void Start() {
    // reporting to game controller
    GameController.Players.Add(this);
  }
}
