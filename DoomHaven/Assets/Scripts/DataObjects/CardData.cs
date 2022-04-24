using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardData : IAsset
{
	[field: SerializeField]
	public string ID { get; set; }
	public string DisplayName;
	public string ArtID;
	public List<string> Abilities;
}
