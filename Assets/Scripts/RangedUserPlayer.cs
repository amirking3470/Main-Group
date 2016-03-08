//* THIS IS THE CURRENT HUMAN CONTROLLED PLAYER FUNCTION, IT IS AN EXTENSION OF THE DEFAULT PLAYER FILE, OF WHICH ALL CODE IS COPIED *//
using UnityEngine;
using System.Collections;

public class RangedUserPlayer : Player {

	// Use this for initialization
	void Start () {

	}

	public static RangedUserPlayer instance;

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
				colDelete ();
				collisionCheck ();
			}
			if (actionPoints == 0) {
				int x = (int)GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.x;
				int y = (int)GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.y;
				ClearMoveHighlight (x, y);
			}
		}

		base.TurnUpdate ();
	}

	public void rangedHighlight () {
		if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].ranged == true) {
			if (attacking == true) {
				int xPot = (int)GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.x;
				int yPot = (int)GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.y;
				int x;
				int y;
				for (int i = 3; i >= 0; i--) {
					for (int j = 3; j >= 0; j--) {
						bool xneg = (xPot + i) > (GameManager.instance.mapSizeX - 1);
						bool yneg = (yPot + j) > (GameManager.instance.mapSizeY - 1);
						x = xPot + i;
						y = yPot + j;

						if (xneg == true) {
							x = xPot + 0;
						}
						if (yneg == true) {
							y = yPot + 0;
						}
						GameManager.instance.map [x] [y].transform.GetComponent<Renderer> ().material.color = Color.magenta;
					}
					for (int j = 0; j <= 3; j++) {
						bool xneg = (xPot - i) < 0;
						bool yneg = (yPot - j) < 0;
						x = xPot - i;
						y = yPot - j;

						if (xneg == true) {
							x = xPot + 0;
						}
						if (yneg == true) {
							y = yPot + 0;
						}
						GameManager.instance.map [x] [y].transform.GetComponent<Renderer> ().material.color = Color.magenta;
					}
				}
				for (int i = 0; i <= 3; i++) {
					for (int j = 3; j >= 0; j--) {
						bool xneg = (xPot - i) < 0;
						bool yneg = (yPot + j) > (GameManager.instance.mapSizeY - 1);
						x = xPot - i;
						y = yPot + j;

						if (xneg == true) {
							x = xPot + 0;
						}
						if (yneg == true) {
							y = yPot + 0;
						}
						GameManager.instance.map [x] [y].transform.GetComponent<Renderer> ().material.color = Color.magenta;
					}
					for (int j = 0; j <= 3; j++) {
						bool xneg = (xPot + i) > (GameManager.instance.mapSizeX - 1);
						bool yneg = (yPot - j) < 0;
						x = xPot + i;
						y = yPot - j;

						if (xneg == true) {
							x = xPot + 0;
						}
						if (yneg == true) {
							y = yPot + 0;
						}
						GameManager.instance.map [x] [y].transform.GetComponent<Renderer> ().material.color = Color.magenta;
					}
				}
			} else if (attacking == false) {
				int xPot = (int)GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.x;
				int yPot = (int)GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.y;
				int x;
				int y;
				for (int i = 3; i >= 0; i--) {
					for (int j = 3; j >= 0; j--) {
						bool xneg = (xPot + i) > (GameManager.instance.mapSizeX - 1);
						bool yneg = (yPot + j) > (GameManager.instance.mapSizeY - 1);
						x = xPot + i;
						y = yPot + j;

						if (xneg == true) {
							x = xPot + 0;
						}
						if (yneg == true) {
							y = yPot + 0;
						}

						GameManager.instance.map[x][y].transform.GetComponent<Renderer> ().material.color = Color.white;
					}
					for (int j = 0; j <= 3; j++) {
						bool xneg = (xPot - i) < 0;
						bool yneg = (yPot - j) < 0;
						x = xPot - i;
						y = yPot - j;

						if (xneg == true) {
							x = xPot + 0;
						}
						if (yneg == true) {
							y = yPot + 0;
						}

						GameManager.instance.map[x][y].transform.GetComponent<Renderer> ().material.color = Color.white;
					}
				}
				for (int i = 0; i <= 3; i++) {
					for (int j = 3; j >= 0; j--) {
						bool xneg = (xPot - i) < 0;
						bool yneg = (yPot + j) > (GameManager.instance.mapSizeY - 1);
						x = xPot - i;
						y = yPot + j;

						if (xneg == true) {
							x = xPot + 0;
						}
						if (yneg == true) {
							y = yPot + 0;
						}

						GameManager.instance.map[x][y].transform.GetComponent<Renderer> ().material.color = Color.white;
					}
					for (int j = 0; j <= 3; j++) {
						bool xneg = (xPot + i) > (GameManager.instance.mapSizeX - 1);
						bool yneg = (yPot - j) < 0;
						x = xPot + i;
						y = yPot - j;

						if (xneg == true) {
							x = xPot + 0;
						}
						if (yneg == true) {
							y = yPot + 0;
						}

						GameManager.instance.map[x][y].transform.GetComponent<Renderer> ().material.color = Color.white;
					}
				}
			}
		}
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
				rangedHighlight ();
				movingHighlight ();
				collisionCheck ();
			} else {
				moving = false;
				attacking = false;
				rangedHighlight ();
				movingHighlight ();
				collisionCheck ();
				colDelete ();
			}
		}

		//attack button
		buttonRect = new Rect (0, Screen.height - buttonHeight * 2, buttonWidth, buttonHeight);

		if (GUI.Button (buttonRect, "Attack")) {
			if (!attacking) {
				moving = false;
				attacking = true;
				rangedHighlight ();
				collisionCheck ();
				colDelete ();
			} else {
				moving = false;
				attacking = false;
				rangedHighlight ();
				collisionCheck ();
				colDelete ();
			}
		}

		// end turn button
		buttonRect = new Rect (0, Screen.height - buttonHeight * 1, buttonWidth, buttonHeight);

		if (GUI.Button (buttonRect, "End Turn")) {
			actionPoints = 3;
			moving = false;
			attacking = false;
			colDelete ();
			rangedHighlight ();
			movingHighlight ();
			GameManager.instance.nextTurn();
		}

		base.TurnOnGUI ();
	}
}
