﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class States {
	private static bool _paused, _moving, _jumping, _interacting, _missionOpen;

	public static bool paused {
		get { return _paused; }
		set { _paused = value; }
	}

	public static bool moving {
		get { return !_paused && _moving; }
		set { 
			if (!_paused) {
				_moving = value;
			}
		}
	}

	public static bool jumping {
		get { return !_paused && _jumping; }
		set { 
			if (!_paused) {
				_jumping = value;
			}
		}
	}

	public static bool interacting {
		get { return !_paused && _interacting; }
		set { 
			if (!_paused) {
				_interacting = value;
			}
		}
	}

	public static bool missionOpen {
		get { return _missionOpen; }
		set { 
			if (!_paused) {
				_missionOpen = value;
			}
		}
	}

	public static void TogglePause() {
		_paused = !_paused;
	}

	public static void ToggleMissions() {
		_missionOpen = !_missionOpen;
	}

	public static void ResetAll() {
		_paused=_moving=_jumping=_interacting=false;
	}
}
