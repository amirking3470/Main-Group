//* THIS IS THE BASE OF THE BASE FOR ALL OF OUR MOVING PLAYER UNITS, ALL OUR GAME UNITS WILL BE AN EXTENSION OF THIS FILE *//
//* CAUTION WHEN EDITING, CHANGES HERE CAN EFFECT ALOT!!! *//
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Vector2 gridPosition = Vector2.zero;

	public GameObject colCheckPrefab;

	public Vector3 moveDestination;
	public float moveSpeed = 10.0f; //Default speed the actor will move along the screen

	public bool moving = false; //variable for the attacking and moving buttons
	public bool attacking = false;
	public bool ranged = false;

	public int HP = 25;
	public string playerName = "JohnMadden";
	public float attackChance = 0.75f;
	public float defenseReduction = 0.15f;
	public int damageBase = 5;
	public float damageRollSides = 6; //d6
	public int user = 0; // player 1 & player 2
	//Default base stats for all units, should be able to change in the extension of this file, if the unit will have different stats

	public static Player instance;

	public int actionPoints = 2;
	
	void Awake () {
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public virtual void TurnUpdate () {
		if (actionPoints <= 0) {
			actionPoints = 2;
			moving = false;
			attacking = false;
			GameManager.instance.nextTurn ();
		}
		//resets everything on a new turn
	}

	public virtual void TurnOnGUI () {

	}

	public void collisionCheck () {
		if (moving == true) {
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

					colDetector colliderBlock = ((GameObject)Instantiate (colCheckPrefab, new Vector3 (x - Mathf.Floor (GameManager.instance.mapSizeX / 2), 1.0f, -y + Mathf.Floor (GameManager.instance.mapSizeY / 2)), Quaternion.Euler (new Vector3 ()))).GetComponent<colDetector> ();
					colliderBlock.gridPosition = new Vector2 (x, y);
					colliderBlock.colGridPosition = new Vector2 (-i, -j);
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
					colDetector colliderBlock = ((GameObject)Instantiate (colCheckPrefab, new Vector3 (x - Mathf.Floor (GameManager.instance.mapSizeX / 2), 1.0f, -y + Mathf.Floor (GameManager.instance.mapSizeY / 2)), Quaternion.Euler (new Vector3 ()))).GetComponent<colDetector> ();
					colliderBlock.gridPosition = new Vector2 (x, y);
					colliderBlock.colGridPosition = new Vector2 (i, j);
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
					colDetector colliderBlock = ((GameObject)Instantiate (colCheckPrefab, new Vector3 (x - Mathf.Floor (GameManager.instance.mapSizeX / 2), 1.0f, -y + Mathf.Floor (GameManager.instance.mapSizeY / 2)), Quaternion.Euler (new Vector3 ()))).GetComponent<colDetector> ();
					colliderBlock.gridPosition = new Vector2 (x, y);
					colliderBlock.colGridPosition = new Vector2 (i, -j);
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
					colDetector colliderBlock = ((GameObject)Instantiate (colCheckPrefab, new Vector3 (x - Mathf.Floor (GameManager.instance.mapSizeX / 2), 1.0f, -y + Mathf.Floor (GameManager.instance.mapSizeY / 2)), Quaternion.Euler (new Vector3 ()))).GetComponent<colDetector> ();
					colliderBlock.gridPosition = new Vector2 (x, y);
					colliderBlock.colGridPosition = new Vector2 (-i, j);
				}
			}
		}
	}

	 
	public void movingHighlight () {
			if (moving == true) {
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
						GameManager.instance.map [x] [y].transform.GetComponent<Renderer> ().material.color = Color.cyan;
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
						GameManager.instance.map [x] [y].transform.GetComponent<Renderer> ().material.color = Color.cyan;
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
						GameManager.instance.map [x] [y].transform.GetComponent<Renderer> ().material.color = Color.cyan;
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
						GameManager.instance.map [x] [y].transform.GetComponent<Renderer> ().material.color = Color.cyan;
					}
				}
		} else if (moving == false && ranged == false) {
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
	public void ClearMoveHighlight(int xPot, int yPot)
	{
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
	public void MeleeHighlight() {
		if (attacking == true) {
			int xPot = (int)GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.x;
			int yPot = (int)GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.y;
			int x;
			int y;
			for (int i = 1; i >= 0; i--) {
				for (int j = 1; j >= 0; j--) {
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
				for (int j = 0; j <= 1; j++) {
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
			for (int i = 0; i <= 1; i++) {
				for (int j = 1; j >= 0; j--) {
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
				for (int j = 0; j <= 1; j++) {
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
			for (int i = 1; i >= 0; i--) {
				for (int j = 1; j >= 0; j--) {
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
				for (int j = 0; j <= 1; j++) {
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
			for (int i = 0; i <= 1; i++) {
				for (int j = 1; j >= 0; j--) {
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
				for (int j = 0; j <= 1; j++) {
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
