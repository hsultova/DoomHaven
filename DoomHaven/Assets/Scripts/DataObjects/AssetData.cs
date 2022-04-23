using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AssetData : IAsset
{
	public string ID { get; set; }
	public Sprite Sprite;
	public GameObject Prefab;
}
