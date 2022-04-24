using System;
using UnityEngine;

[Serializable]
public class EnemyData : IAsset
{
	[field: SerializeField]
	public string ID { get; set; }
	public int Health;
	public int Damage;
	public string PrefabID;
}
