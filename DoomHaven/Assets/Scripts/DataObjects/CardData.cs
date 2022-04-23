using System;
using System.Collections.Generic;

[Serializable]
public class CardData : IAsset
{
	public string ID { get; set; }
	public string DisplayName;
	public string ArtID;
	public List<string> Abilities;
}
