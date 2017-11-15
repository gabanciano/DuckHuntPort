using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour {

	readonly int DEFAULT_MAX_ROUND = 11;

	public GameUIManager GameUI;
	public GameObject[] Ducks;
	float spawnStart = 3.8f;

	bool once_checkroundresult = false;

	void Awake(){
		GameData.spawn_duck_once = true;	
	}

	void Update () {
		CheckSpawnDelay ();
		CheckRoundResults ();
	}

	void CheckSpawnDelay(){
		spawnStart -= Time.deltaTime;
		if (spawnStart <= 0) {
			StartCoroutine (SpawnDuck ());
			GameData.spawn_duck_once = false;
		}
	}

	void CheckRoundResults(){
		if (once_checkroundresult) {
			StartCoroutine (GameUI.CheckRoundLimit ());
			once_checkroundresult = false;
		}
	}

	IEnumerator SpawnDuck(){
		if (GameData.spawn_duck_once && GameData.round_duck_number < DEFAULT_MAX_ROUND) {
			if (!GameData.round_over) {
				yield return new WaitForSeconds (2.5f);
				Instantiate (Ducks [Random.Range (0, 3)], new Vector3 (Random.Range (-7.5f, 7.1f), transform.position.y, transform.position.z), transform.rotation);
			}
		} else if (GameData.round_duck_number >= DEFAULT_MAX_ROUND) {
			once_checkroundresult = true;

		}
	}
}
