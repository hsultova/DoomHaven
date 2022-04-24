using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : IAsset
{
	[field: SerializeField]
	public string ID { get; set; }
	public int Health;
	public int Attack;
	public int Movement;
	public string PrefabID;
}
