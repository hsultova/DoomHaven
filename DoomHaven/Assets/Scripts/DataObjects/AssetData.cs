using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AssetData : IAsset
{
	[field: SerializeField]
	public string ID { get; set; }
	public List<Sprite> Sprites = new List<Sprite>();
	public GameObject Prefab;

	public Sprite GetSpriteAtRandom()
	{
		if (Sprites.Count == 0) {
			return null;
		}

		int rand = UnityEngine.Random.Range(0, Sprites.Count);
		Sprite sprite = Sprites[rand];

		return sprite;
	}
}
