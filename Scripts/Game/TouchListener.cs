using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchListener : MonoBehaviour {

	int SCORE_GREEN_DUCK = 500;
	int SCORE_BLUE_DUCK = 1000;
	int SCORE_RED_DUCK = 1500;

	public GameUIManager GameUI;
	public PrefabStorage Prefab;
	public AudioManager GameAudio;

	Ray2D FingerRay;
	RaycastHit2D hit;

	void Awake()
	{
		GameData.round_bullets = 3;
	}

	void Update () 
	{
		TouchScreen ();
	}

	public void TouchScreen(){
		//if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) 
		if (Input.GetMouseButtonDown(0))
		{
			if (GameData.game_started) 
			{
				GameData.round_bullets--;
				Debug.Log ("Ammo: " + GameData.round_bullets);
				if (GameData.round_bullets >= 0) 
				{
					GameData.no_more_bullets = false;
					GameAudio.sfx_duck_hit.Play ();
					//hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position), Vector2.zero);
					hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
					if (hit) 
					{
						GameData.round_kills++; 
						Debug.Log ("Round Kills: " + GameData.round_kills);
						GameData.duck_hit = true;
						GameUI.CheckRoundProgress ();
						CheckRoundScore ();
						if (hit.transform.gameObject.tag == "DuckRed") 
						{
							Destroy (hit.transform.gameObject);
							GameAudio.sfx_duck_hit.Play ();
							GameData.game_score += SCORE_RED_DUCK;
							Instantiate (Prefab.DuckRedDead, hit.transform.position, hit.transform.rotation);
						}
						if (hit.transform.gameObject.tag == "DuckGreen") 
						{
							Destroy (hit.transform.gameObject);
							GameAudio.sfx_duck_hit.Play ();
							GameData.game_score += SCORE_GREEN_DUCK;
							Instantiate (Prefab.DuckGreenDead, hit.transform.position, hit.transform.rotation);
						}
						if (hit.transform.gameObject.tag == "DuckBlue") 
						{
							Destroy (hit.transform.gameObject);
							GameAudio.sfx_duck_hit.Play ();
							GameData.game_score += SCORE_BLUE_DUCK;
							Instantiate (Prefab.DuckBlueDead, hit.transform.position, hit.transform.rotation);
						}
					}
					if (GameData.round_bullets <= 0 && !GameData.duck_hit) 
					{
						GameData.no_more_bullets = true;
						GameData.game_started = false;
					}
				} 
			}
		}
	}

	void CheckRoundScore(){
		if (GameData.current_round <= 5) {
			SCORE_GREEN_DUCK = 500;
			SCORE_BLUE_DUCK = 1000;
			SCORE_RED_DUCK = 1500;
		} else if (GameData.current_round <= 10) {
			SCORE_GREEN_DUCK = 800;
			SCORE_BLUE_DUCK = 1600;
			SCORE_RED_DUCK = 2400;
		} else {
			SCORE_GREEN_DUCK = 1000;
			SCORE_BLUE_DUCK = 2000;
			SCORE_RED_DUCK = 3000;
		}
	}
}
