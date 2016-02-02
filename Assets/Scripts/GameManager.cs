using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	
	public GameObject TilePrefab;
	public GameObject UserPlayerPrefab;
	public GameObject AIPlayerPrefab;
	
	public int mapSize = 11;
	
	List <List<Tile>> map = new List<List<Tile>>();
	public List <Player> players = new List<Player>();
	public int currentPlayerIndex = 0;
	
	void Awake() {
		instance = this;
	}
	
	// Use this for initialization
	void Start () {		
		generateMap();
		generatePlayers();
	}
	
	// Update is called once per frame
	void Update () {
		if (players [currentPlayerIndex].HP > 0) {
			players [currentPlayerIndex].TurnUpdate ();
		} else nextTurn ();
	}

	void OnGUI () {
		players[currentPlayerIndex].TurnOnGUI ();
	}
	
	public void nextTurn() {
		if (currentPlayerIndex + 1 < players.Count) {
			currentPlayerIndex++;
		} else {
			currentPlayerIndex = 0;
		}
	}
	
	public void moveCurrentPlayer(Tile destTile) {
		players [currentPlayerIndex].gridPosition = destTile.gridPosition;
		players [currentPlayerIndex].moveDestination = destTile.transform.position + 1.5f * Vector3.up;
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
		map = new List<List<Tile>>();
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
		
		//AIPlayer aiplayer = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(6 - Mathf.Floor(mapSize/2),1.5f, -4 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();
		
		//players.Add(aiplayer);
	}
}