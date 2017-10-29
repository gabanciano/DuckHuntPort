using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckFalling : MonoBehaviour {

	float duck_fall_speed = 5.5f;
	public AudioSource sfx_duck_hit_ground;

	public GameObject DogHitDuck;
	void Start(){
		GameData.game_started = false;
	}
	void Update () {
		transform.Translate (new Vector2 (0, -duck_fall_speed * Time.deltaTime));
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "BounceColBot") {
			Instantiate (DogHitDuck, transform.position, transform.rotation);
			GameData.spawn_duck_once = true;
		}
	}
}
