using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventsTrigger : MonoBehaviour {
	public event EventHandler MouseDown = delegate {};

	void OnMouseDown() {
		MouseDown(this, EventArgs.Empty);
	}
}
