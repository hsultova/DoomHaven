using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "AssetDataRepository", menuName = "Data/AssetDataRepository", order = 1)]
public class AssetDataContext : ScriptableObject
{
	public List<AssetData> Assets;
}
