using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour {
	
	public Text playersScoreText;
	
	List<PlayerData> playersScore;
	public GameObject player;
	
	void Start(){
		playersScore = new List<PlayerData>();
		
		//Override del Sort para ordenar por score
		playersScore.Sort(delegate (PlayerData a,PlayerData b) { return a.playerScore.CompareTo(b.playerScore); });
		
	}
	
	public void AddScore(String player, float score){
		bool found = false;
		foreach(PlayerData pd in playersScore){
			if(pd.playerName.Equals(player)){
				pd.playerScore += score;
				found = true;
				break;
			}
		}		
		if(!found){
			playersScore.Add(new PlayerData(player,score));
		}
		UpdateScore();

	}
	
	void UpdateScore(){
		//SORT THE DICTIONARY BY VALUES
		playersScoreText.text = "Players:\n";
		playersScore.Sort();
		foreach(PlayerData pd in playersScore){
			playersScoreText.text += "* "+ pd.playerName + " : " + pd.playerScore + "\n";
		}
		
	}
	
	public void ChangeColor(){
		player.GetComponent<SkinnedMeshRenderer>().material.color = Color.blue;
	}
	
	
}
