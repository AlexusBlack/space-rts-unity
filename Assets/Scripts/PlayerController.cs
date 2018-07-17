using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public GameController GameController;
  public List<UnitController> Units;

  public int Dilithium = 0;
  public int Metal = 0;

  private int dilithium;
  private int metal;

  public void Start() {
    // reporting to game controller
    GameController.Players.Add(this);
  }

  public void AddDilithium(int value) {
    Dilithium += value;
    ResourcesAmountChanged(this, EventArgs.Empty);
  }

  public void AddMetal(int value) {
    Metal += value;
    ResourcesAmountChanged(this, EventArgs.Empty);
  }

  public event EventHandler ResourcesAmountChanged = delegate {};
}
