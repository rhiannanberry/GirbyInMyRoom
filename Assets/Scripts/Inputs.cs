using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inputs {
	private static bool _interact, _togglePause, _leftClick, _fwd, _bwd, _left, _right, _jump, _toggleMission;
	private static Vector2 _moveDirection, _mouseVelocity;

	public static bool interact {
		get { return (!States.paused && _interact); }
		set { 
			_interact = !States.paused && value;
		}
	}

	public static bool togglePause {
		get { return _togglePause; }
		set {//TODO
			_togglePause = value;
			if (_togglePause) States.TogglePause();
		}
	}

	public static bool toggleMission {
		get { return _toggleMission; }
		set {//TODO
			_toggleMission = value;
			if (_toggleMission) States.ToggleMissions();
		}
	}

	public static bool leftClick {
		get { return (States.paused && _leftClick); }
		set { _leftClick = States.paused && value; }
	}

	public static bool fwd {
		get { return (!States.paused && _fwd); }
		set { _fwd = !States.paused && value; }
	}

	public static bool bwd {
		get { return (!States.paused && _bwd); }
		set { _bwd = !States.paused && value; }
	}

	public static bool left {
		get { return (!States.paused && _left); }
		set { _left = !States.paused && value; }
	}

	public static bool right {
		get { return (!States.paused && _right); }
		set { _right = !States.paused && value; }
	}

	public static bool jump {
		get { return (!States.paused && _jump); }
		set { _jump= !States.paused && value; }
	}

	public static Vector2 moveDirection {
		get {
			if (States.paused) {
				return Vector2.zero;
			}
			return _moveDirection;
		}
		set { _moveDirection = value; }
	}

	public static Vector2 mouseVelocity {
		get { return _mouseVelocity; }
		set { _mouseVelocity = value; }
	}

	public static Vector2 cameraVelocity {
		get {
			if (States.paused) {
				return Vector2.zero;
			}
			return _mouseVelocity;
		}
	}

	public static void ResetAll() {
		_interact=_togglePause=_leftClick=_fwd=_bwd=_left=_right=_jump=false;
		_moveDirection=_mouseVelocity=Vector2.zero;
		States.ResetAll();
	}
}
