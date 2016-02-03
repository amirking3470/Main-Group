//* THIS IS THE MAIN GAME MANAGER, BE VERY CAREFUL WHEN EDITING THIS AS CHANGES IN IT MAY HAVE BIG CONSEQUENCES //*
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public static GameManager instance; //an instance of the game manager, use this to call functions of the manager outside of the file itsel
	
	public GameObject TilePrefab; //Main prefab for the tile
	public GameObject UserPlayerPrefab; //Main PLayer prefab for player controlled units, will be divided as we add more units
	public GameObject AIPlayerPrefab; //Main player prefab for the AI
	
	public int mapSize = 11; //Default map size, standard generates an 12/12 grid, but can be made bigger on the game manager object in editor
	//* We need to find out a way to be able to generate custom grid sizes! Ie: 15/10 //*
	
	List <List<Tile>> map = new List<List<Tile>>(); //A 2d list, holding pretty much an array of all grid points
	public List <Player> players = new List<Player>(); //a 1d list, holding each player object's information
	public int currentPlayerIndex = 0; //an int for tracking number of players
	
	void Awake() {
		instance = this; //initilizing the gamemanager instance on line 7
	}
	
	// Use this for initialization
	void Start () {		
		generateMap();
		generatePlayers();
	}
	
	// Update is called once per frame
	void Update () { //If a player's health is lower than zero, it will skip thier turn
		if (players [currentPlayerIndex].HP > 0) {
			players [currentPlayerIndex].TurnUpdate ();
		} else nextTurn ();
	}

	void OnGUI () {
		players[currentPlayerIndex].TurnOnGUI (); //enabling the in game GUI, set out in player, actual code located in UserPlayer
	}
	
	public void nextTurn() {
		if (currentPlayerIndex + 1 < players.Count) { //Current player loop, when going out of bounds swaps back to the first palyer
			currentPlayerIndex++;
		} else {
			currentPlayerIndex = 0;
		}
	}

	public void moveCurrentPlayer(Tile destTile) {
		players [currentPlayerIndex].gridPosition = destTile.gridPosition; //Updating the current player's position as they move
		players [currentPlayerIndex].moveDestination = destTile.transform.position + 1.5f * Vector3.up; //moves the current player the the selected tile
	}


	public void attackWithCurrentPlayer(Tile destTile) {
		Player target = null;
		foreach (Player p in players) {
			if (p.gridPosition == destTile.gridPosition) {
				target = p;
			}
		}

		if (target != null) {

			if (players [currentPlayerIndex].gridPosition.x >= target.gridPosition.x - 1 && players [currentPlayerIndex].gridPosition.x <= target.gridPosition.x + 1 &&
			    players [currentPlayerIndex].gridPosition.y >= target.gridPosition.y - 1 && players [currentPlayerIndex].gridPosition.y <= target.gridPosition.y + 1) {
				//checking to see if the selected target is adjacent, will have to be changed when we add ranged characters
				players [currentPlayerIndex].actionPoints--;
				//attack logic
				//roll to hit
				bool hit = Random.Range (0.0f, 1.0f) <= players [currentPlayerIndex].attackChance;
					
				if (hit) {
					//damage logic
					int amountOfDamage = (int)Mathf.Floor (players [currentPlayerIndex].damageBase + Random.Range (0, players [currentPlayerIndex].damageRollSides));
					target.HP -= amountOfDamage;
						
					Debug.Log (players [currentPlayerIndex].playerName + " successfuly hit " + target.playerName + " for " + amountOfDamage + "damage!");
				} else {
					Debug.Log (players [currentPlayerIndex].playerName + " missed " + target.playerName);
				}
			} else {
				Debug.Log ("Target is not adjacent!");
			}
		}
	}

	void generateMap() {
		map = new List<List<Tile>>(); //generatign the playing field, making a grid of tile prefabs, and storing their positiosn in a 2d list
		for (int i = 0; i < mapSize; i++) {
			List <Tile> row = new List<Tile>();
			for (int j = 0; j < mapSize; j++) {
				Tile tile = ((GameObject)Instantiate(TilePrefab, new Vector3(i - Mathf.Floor(mapSize/2),0, -j + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<Tile>();
				tile.gridPosition = new Vector2(i, j);
				row.Add (tile);
			}
			map.Add(row);
		}
	}
	
	void generatePlayers() {
		UserPlayer player;
		//Adding players, using prefabs with relevant code attached
		player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(0 - Mathf.Floor(mapSize/2),1.7f, -0 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
		player.gridPosition = new Vector2 (0, 0);
		player.playerName = "Bob";
		players.Add(player);
		
		player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3((mapSize-1) - Mathf.Floor(mapSize/2),1.7f, -(mapSize-1) + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
		player.gridPosition = new Vector2 ((mapSize - 1), (mapSize - 1));
		player.playerName = "Hank";
		players.Add(player);
				
		player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(4 - Mathf.Floor(mapSize/2),1.7f, -4 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
		player.gridPosition = new Vector2 (4, 4);
		player.playerName = "Tim";
		players.Add(player);
		//* CURRENTLY COMMENTED OUT AI, NEEDS TO BE WORKED ON BEFORE ITS ADDED BACK IN *//
		//* COMBAT NEEDS TO BE APPLIED TO IT, AND IT NEEDS TO BE ABLE TO ATTACK AS WELL AS MOVE? *//
		AIPlayer aiplayer = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(6 - Mathf.Floor(mapSize/2),1.5f, -4 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();
		
		players.Add(aiplayer);
	}
}
