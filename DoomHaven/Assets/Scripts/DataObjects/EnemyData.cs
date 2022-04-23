using System;

[Serializable]
public class EnemyData : IAsset
{
	public string ID { get; set; }
	public int Health;
	public int Damage;
	public string PrefabID;
}
