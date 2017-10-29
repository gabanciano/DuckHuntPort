using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckDead : MonoBehaviour {

	public GameObject DuckFalling;
	float time_before_falling = 0.5f;

	void Start(){
		GameData.game_started = false;
	}

	void Update () {
		time_before_falling -= Time.deltaTime;
		if (time_before_falling <= 0) {
			Instantiate (DuckFalling, this.transform.position, this.transform.rotation);
			Debug.Log ("Current Duck: " + GameData.round_duck_number);
			Destroy (gameObject);
		}

		if (GameData.round_over) {
			Destroy (gameObject);
		}
	}
}
