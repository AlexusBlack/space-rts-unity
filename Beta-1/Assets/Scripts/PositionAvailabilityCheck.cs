using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAvailabilityCheck : MonoBehaviour {
	public Material PositionAvailableMaterial;
  public Material PositionUnAvailableMaterial;
	public MeshRenderer Renderer;
	
	private int triggered = 0; 
	
	void Start() {
		if(Renderer == null) Debug.LogError("Body object reference required");
		if(PositionAvailableMaterial == null) Debug.LogError("Position available material required");
		if(PositionUnAvailableMaterial == null) Debug.LogError("Position unavailable material required");
	}

	void OnTriggerEnter(Collider collision) {
		if(collision.gameObject.tag == "MapGrid") return;
		triggered++;
		Renderer.material = PositionUnAvailableMaterial;
	}

	void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == "MapGrid") return;
		triggered--;
		if(triggered == 0) Renderer.material = PositionAvailableMaterial;
	}

	public bool IsPositionAvailable() {
		return triggered == 0;
	}
}
