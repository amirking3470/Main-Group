//* BASE FILE FOR OUR TILES, SHOULDENT NEED TO BE CHANGED MUCH FOR NOW *//
using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	public Vector2 gridPosition = Vector2.zero;
	public bool badTile = false;
	
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter() {
		if (badTile != true) {
			//Highlighting a selected tile based on the current button selection
			if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].moving) {
				transform.GetComponent<Renderer> ().material.color = Color.blue;
			} else if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].attacking) {
				transform.GetComponent<Renderer> ().material.color = Color.red;
			}
		}
		//Debug.Log("my position is (" + gridPosition.x + "," + gridPosition.y); ADD THIS IF YOU NEED TO BE ABLE TO READ GRID POSITIONS EASILY
	}
	
	void OnMouseExit() {
		if (badTile != true) {
			if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].attacking == true && GameManager.instance.players [GameManager.instance.currentPlayerIndex].ranged == true) {
				if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.x >= this.gridPosition.x - 3 &&
				   GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.x <= this.gridPosition.x + 3 &&
				   GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.y >= this.gridPosition.y - 3 &&
				   GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.y <= this.gridPosition.y + 3) {
					transform.GetComponent<Renderer> ().material.color = Color.magenta; 
				} else {
					transform.GetComponent<Renderer> ().material.color = Color.white; 
					//changing the color of the tile back to the default white, will have to be changed when we add textures to tiles
				}
			} else if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].moving == true) {
				if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.x >= this.gridPosition.x - 3 &&
				   GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.x <= this.gridPosition.x + 3 &&
				   GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.y >= this.gridPosition.y - 3 &&
				   GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.y <= this.gridPosition.y + 3) {
					transform.GetComponent<Renderer> ().material.color = Color.cyan; 
				} else {
					transform.GetComponent<Renderer> ().material.color = Color.white; 
					//changing the color of the tile back to the default white, will have to be changed when we add textures to tiles
				}
			} else {
				transform.GetComponent<Renderer> ().material.color = Color.white; 
				//changing the color of the tile back to the default white, will have to be changed when we add textures to tiles
			}
		}
	}
	
	
	void OnMouseDown() {
		if (badTile != true) {
			if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].moving) {
				int x = (int)GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.x;
				int y = (int)GameManager.instance.players [GameManager.instance.currentPlayerIndex].gridPosition.y;
				GameManager.instance.players [GameManager.instance.currentPlayerIndex].ClearMoveHighlight (x, y);
				GameManager.instance.moveCurrentPlayer (this);
			} else if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].attacking) {
				GameManager.instance.attackWithCurrentPlayer (this);
			}
			//when clicking the tile based on what is currently selected it will either call the moving function or the attacking function in GameManager
		}
	}
	
}
