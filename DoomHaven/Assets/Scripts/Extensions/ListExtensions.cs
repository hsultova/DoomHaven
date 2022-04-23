using System.Collections.Generic;

public static class ListExtentions
{
	public static T GetDataByID<T>(this List<T> assetCollection, string ID) where T: IAsset
	{
		foreach (T asset in assetCollection)
		{
			if (asset.ID.ToLower() == ID.ToLower())
			{
				return asset;
			}
		}

		return default;
	}
}
