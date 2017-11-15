using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBehavior : MonoBehaviour {

	float birdSpeed;
	float flyAwayTimer;
	Vector3 DuckBounceDirection;

	void Awake(){
		CheckCurrentSpeed ();

		flyAwayTimer = 4f;
		GameData.game_started = true;
		GameData.round_bullets = 3;
		GameData.no_more_bullets = false;
		GameData.duck_hit = false;
		GameData.bird_flew = false;

		GameData.round_duck_number++;

		GetComponent<Rigidbody2D>().AddForce(new Vector2(birdSpeed,birdSpeed));
	}

	void Start(){
		if (GameData.round_duck_number > 10) {
			Destroy (gameObject);
		}
	}

	void Update(){
		FlyAway ();
	}

	void CheckCurrentSpeed(){
		if (GameData.current_round == 1) {
			birdSpeed = 150f;
		} else if (GameData.current_round == 2) {
			birdSpeed = 250f;
		} else if (GameData.current_round == 3) {
			birdSpeed = 350f;
		} else if (GameData.current_round == 4) {
			birdSpeed = 450f;
		} else if (GameData.current_round == 5) {
			birdSpeed = 550f;
		} else if (GameData.current_round == 6) {
			birdSpeed = 650f;
		} else if (GameData.current_round == 7) {
			birdSpeed = 750f;
		} else if (GameData.current_round == 8) {
			birdSpeed = 850f;
		} else if (GameData.current_round == 9) {
			birdSpeed = 950f;
		} else if (GameData.current_round == 10) {
			birdSpeed = 1050f;
		} else if (GameData.current_round == 11) {
			birdSpeed = 1150f;
		} else if (GameData.current_round == 12) {
			birdSpeed = 1250f;
		} else if (GameData.current_round == 13) {
			birdSpeed = 1350f;
		} else if (GameData.current_round == 14) {
			birdSpeed = 1450f;
		} else if (GameData.current_round == 15) {
			birdSpeed = 1550f;
		} else if (GameData.current_round == 16) {
			birdSpeed = 1650f;
		} else if (GameData.current_round == 17) {
			birdSpeed = 1750f;
		} else if (GameData.current_round == 18) {
			birdSpeed = 1850f;
		} else if (GameData.current_round == 19) {
			birdSpeed = 1950f;
		} else if (GameData.current_round == 20) {
			birdSpeed = 2050f;
		} else {
			birdSpeed = 2150f;
		}
	}

	public void FlyAway(){
		if (GameData.round_bullets <= 0) {
			flyAwayTimer -= Time.deltaTime;
			if (flyAwayTimer <= 0) {
				GameData.bird_flew = true;
				GameData.spawn_duck_once = true;
				flyAwayTimer = 4f;
				Destroy (gameObject);
			}
		}
	}

}
