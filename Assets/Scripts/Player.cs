using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Vector2 gridPosition = Vector2.zero;

	public Vector3 moveDestination;
	public float moveSpeed = 10.0f;

	public bool moving = false;
	public bool attacking = false;

	public int HP = 25;
	public string playerName = "JohnMadden";
	public float attackChance = 0.75f;
	public float defenseReduction = 0.15f;
	public int damageBase = 5;
	public float damageRollSides = 6; //d6

	public int actionPoints = 2;
	
	void Awake () {
		moveDestination = transform.position;
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
	}

	public virtual void TurnOnGUI () {

	}
}