using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData : IAsset
{
	[field: SerializeField]
	public string ID { get; set; }
	public int Health;
	public int Attack;
	public int Movement;
	public List<string> Cards;
	public string PrefabID;
}
