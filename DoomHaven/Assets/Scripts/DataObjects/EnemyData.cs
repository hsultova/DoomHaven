using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData : IAsset
{
	[field: SerializeField]
	public string ID { get; set; }
	public string DisplayName;
	public int Health;
	public int Attack;
	public int Movement;
	public List<string> Cards;

	public string PrefabID;
}
