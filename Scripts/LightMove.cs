using UnityEngine;
using System.Collections.Generic;

public class LightMove : MonoBehaviour {
	
	public float minTime = 2;
	public float maxTime = 4;
	
	public float horizScale = 4;
	public float vertScale = 2;
	
	public float speed = 2;
	public float score = 10;
	public float tickTime = 0.5f;
	
	float randomTime;
	Vector3 randomPosition;
	
	GameController gameController;
	
	Dictionary<string,float> playersInArea;
	
	void Start(){
		randomTime = Random.Range(minTime,maxTime);
		randomPosition = new Vector3(Random.value * vertScale, transform.position.y, Random.value * horizScale);
		
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent <GameController>();
        }
        if (gameController == null)
        {
            Debug.Log ("Cannot find 'GameController' script");
        }
		
		playersInArea = new Dictionary<string,float>();
	}
	
	void Update(){
		if(randomTime <= 0){
			randomTime = Random.Range(minTime,maxTime);
			randomPosition = new Vector3(Random.Range(-1,1) * vertScale, transform.position.y, Random.Range(-1,1) * horizScale);
		}else{
			randomTime -= Time.deltaTime;
			Vector3 offset = (randomPosition - transform.position).magnitude < .5f ? Vector3.zero : randomPosition - transform.position;
			transform.position += offset.normalized * speed * Time.deltaTime;
		}
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Player")){
			if(!playersInArea.ContainsKey(other.gameObject.name)){
				playersInArea.Add(other.gameObject.name,0);
			}
		}
	}
	
	void OnTriggerExit(Collider other){
		if(other.gameObject.CompareTag("Player")){
			if(!playersInArea.ContainsKey(other.gameObject.name)){
				playersInArea.Remove(other.gameObject.name);
			}
		}
	}
	
	void OnTriggerStay(Collider other){
		List<string> keys = new List<string>(playersInArea.Keys);
		foreach(string key in keys){
			playersInArea[key] += Time.deltaTime;
			if(playersInArea[key] >= tickTime){
				playersInArea[key] -= tickTime;
				if(gameController != null){
					gameController.AddScore(key,score);
					gameController.ChangeColor();
				}
				else
					Debug.Log("Tick Score");
			}
		}
	}
}
