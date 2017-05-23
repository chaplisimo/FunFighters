using System;

[Serializable]
public class PlayerData : IComparable<PlayerData>, IEquatable<PlayerData> {
	
	public string playerName;
	public float playerScore;
	
	public PlayerData(string playerName, float playerScore){
		this.playerName = playerName;
		this.playerScore = playerScore;
	}
	
	//Override del CompareTo para obtener el PlayerScore ordenado
	public int CompareTo(PlayerData other) {
		return this.playerScore.Equals(other.playerScore) ? 1 : 0;
    }
    
    public bool Equals(PlayerData other) {
		return this.playerName.Equals(other.playerName);
    }
}
