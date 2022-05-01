using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData : IAsset
{
	[field: SerializeField]
	public string ID { get; set; }
	public TextAsset WalkLayer;
	public TextAsset EnvironmentLayer;
	public TextAsset InteractableLayer;
}
