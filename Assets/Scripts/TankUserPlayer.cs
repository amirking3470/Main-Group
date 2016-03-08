//* THIS IS THE CURRENT HUMAN CONTROLLED PLAYER FUNCTION, IT IS AN EXTENSION OF THE DEFAULT PLAYER FILE, OF WHICH ALL CODE IS COPIED *//
using UnityEngine;
using System.Collections;

public class TankUserPlayer : Player {

	// Use this for initialization
	void Start () {

	}

	public static TankUserPlayer instance;

	void Awake() {
		instance = this; //initilizing the gamemanager instance on line 7
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.instance.players [GameManager.instance.currentPlayerIndex] == this) {
			transform.GetComponent<Renderer> ().material.color = Color.green; //Current player's turn will highlight them green
		} else {
			transform.GetComponent<Renderer> ().material.color = Color.white;
		}

		if (HP <= 0) {
			transform.GetComponent<Renderer> ().material.color = Color.red; //When a player's hp gets to zero, the are changed to red and rotaed 90 degrees
			transform.rotation = Quaternion.Euler (new Vector3 (90,0,0));
		}
		//collisionCheck ();
	}

	public override void TurnUpdate ()
	{
		if (Vector3.Distance(moveDestination, transform.position) > 0.1f) {
			transform.position += (moveDestination - transform.position).normalized * moveSpeed * Time.deltaTime;
			//if the target destination is more than 1 tile, the player will move over time instead of warping to the point
			if (Vector3.Distance(moveDestination, transform.position) <= 0.1f) {
				transform.position = moveDestination;
				actionPoints--; //when the move is complete, the action point is removed
				movingHighlight();
			}
			if (actionPoints == 0) {
				int x = (int)GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.x;
				int y = (int)GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.y;
				ClearMoveHighlight (x, y);
			}
		}

		base.TurnUpdate ();
	}


	public override void TurnOnGUI () {
		//* Adding GUI elements, I completly followed the tutorial here, so I dont know much about it *//
		//* AMIR AND JEFF, PLAY WITH THIS POTENTALLY? MAYBE TWEAK IT A BIT *//
		float buttonHeight = 50; 
		float buttonWidth = 150;

		Rect buttonRect = new Rect (0, Screen.height - buttonHeight * 3, buttonWidth, buttonHeight);

		//move button

		if (GUI.Button (buttonRect, "Move")) {
			if (!moving) {
				moving = true;
				attacking = false;
				movingHighlight ();
			} else {
				moving = false;
				attacking = false;
				movingHighlight ();
			}
		}

		//attack button
		buttonRect = new Rect (0, Screen.height - buttonHeight * 2, buttonWidth, buttonHeight);

		if (GUI.Button (buttonRect, "Attack")) {
			if (!attacking) {
				moving = false;
				attacking = true;
				movingHighlight ();
				MeleeHighlight ();
			} else {
				moving = false;
				attacking = false;
				movingHighlight ();
				MeleeHighlight ();
			}
		}

		// end turn button
		buttonRect = new Rect (0, Screen.height - buttonHeight * 1, buttonWidth, buttonHeight);

		if (GUI.Button (buttonRect, "End Turn")) {
			actionPoints = 3;
			moving = false;
			attacking = false;
			movingHighlight ();
			MeleeHighlight ();
			GameManager.instance.nextTurn();
		}

		base.TurnOnGUI ();
	}
}
