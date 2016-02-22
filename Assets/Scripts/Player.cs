//* THIS IS THE BASE OF THE BASE FOR ALL OF OUR MOVING PLAYER UNITS, ALL OUR GAME UNITS WILL BE AN EXTENSION OF THIS FILE *//
//* CAUTION WHEN EDITING, CHANGES HERE CAN EFFECT ALOT!!! *//
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Vector2 gridPosition = Vector2.zero;

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
	//Default base stats for all units, should be able to change in the extension of this file, if the unit will have different stats


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
}
