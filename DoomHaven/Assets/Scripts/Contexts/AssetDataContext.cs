using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "AssetDataRepository", menuName = "Data/AssetDataRepository", order = 1)]
public class AssetDataContext : ScriptableObject
{
	public List<AssetData> Assets;

	public AssetData GetAssetDataByID(string ID)
	{
		AssetData data = null;
		data = Assets.GetDataByID(ID);

		return data;
	}

	public Sprite GetAssetSpriteByID(string ID)
	{
		AssetData asset = GetAssetDataByID(ID);
		if (asset == null)
		{
			return null;
		}

		return asset.GetSpriteAtRandom();
	}

	public GameObject GetAssetPrefabByID(string ID)
	{
		AssetData asset = GetAssetDataByID(ID);
		if (asset == null)
		{
			return null;
		}

		return asset.Prefab;
	}
}
