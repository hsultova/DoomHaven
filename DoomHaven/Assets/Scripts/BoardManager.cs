using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	public GameObject DebugTile;
	public GameObject DefaultTile;
	public GameObject BoardRoot;
	public GameObject CharactersRoot;
	public int Rows;
	public int Columns;

	private Tile[,] _tiles;
	private GameDataContext _gameData;
	private AssetDataContext _assetData;

	public void InitBoardWithTiles(GameObject tilePrefab)
	{
		foreach (Transform child in BoardRoot.transform) {
			Destroy(child.gameObject);
		}

		for (int x = 0; x < Rows; x++)
		{
			for (int y = 0; y < Columns; y++)
			{
				GameObject newTileVisuals = Instantiate(tilePrefab, new Vector3(y, Rows - x, 0), tilePrefab.transform.rotation, BoardRoot.transform);

				_tiles[x, y] = newTileVisuals.GetComponent<Tile>();
				_tiles[x, y].X = x;
				_tiles[x, y].Y = y;
				_tiles[x, y].Visuals = newTileVisuals;
				_tiles[x, y].DebugText.text = $"{_tiles[x, y].X}x{_tiles[x, y].Y}";
			}
		}
	}

	public void LoadLevel(LevelData level, GameDataContext gameData, AssetDataContext assetData)
	{
		_assetData = assetData;
		_gameData = gameData;

		List<List<string>> parsedWalkLayer = ParseTextToRows(level.WalkLayer.text);
		List<List<string>> parsedEnvLayer = ParseTextToRows(level.EnvironmentLayer.text);
		List<List<string>> parsedInterLayer = ParseTextToRows(level.InteractableLayer.text);

		//Init the grid with the proper dimensions and empty cells
		Rows = Columns = parsedWalkLayer.Count;
		_tiles = new Tile[Rows, Columns];

		InitBoardWithTiles(DefaultTile);

		//Parse the layer to get proper tile states for walkability
		ParseWalkLayer(parsedWalkLayer);
		ParseEnvLayer(parsedEnvLayer);
		ParseInteractableLayer(parsedInterLayer);
	}

	private void ParseWalkLayer(List<List<string>> layer)
	{
		string nonWalkableTile = "0";
		string walkableTile = "1";

		for (int x = 0; x < Rows; x++)
		{
			for (int y = 0; y < Columns; y++)
			{
				string state = layer[x][y];
				if (state == nonWalkableTile) {
					_tiles[x, y].WalkableState = TileWalkableState.NonWalkable;
				}
				else if (state == walkableTile) {
					_tiles[x, y].WalkableState = TileWalkableState.Walkable;
				}
			}
		}
	}

	private void ParseEnvLayer(List<List<string>> layer)
	{
		for (int x = 0; x < Rows; x++)
		{
			for (int y = 0; y < Columns; y++)
			{
				string state = layer[x][y];

				AssetData data = _assetData.Assets.GetDataByID(state);
				if (data != null)
				{
					Sprite sprite = data.GetSpriteAtRandom();
					if (sprite == null) {
						continue;
					}

					_tiles[x, y].BackgroundSprite.sprite = sprite;
				}
			}
		}
	}

	private void ParseInteractableLayer(List<List<string>> layer)
	{
		for (int x = 0; x < Rows; x++)
		{
			for (int y = 0; y < Columns; y++)
			{
				string state = layer[x][y];

				PlayerData player = _gameData.Players.GetDataByID(state);
				if (player != null)
				{
					GameObject playerPrefab = _assetData.GetAssetPrefabByID(player.PrefabID);
					CreateInteractableElementAt(x, y, playerPrefab, CharactersRoot);
					//Instantiate(playerPrefab, _tiles[x, y].transform.position, playerPrefab.transform.rotation, CharactersRoot.transform);

					continue;
				}

				EnemyData enemy = _gameData.Enemies.GetDataByID(state);
				if (enemy != null)
				{
					GameObject enemyPrefab = _assetData.GetAssetPrefabByID(enemy.PrefabID);
					CreateInteractableElementAt(x, y, enemyPrefab, CharactersRoot);
					//Instantiate(enemyPrefab, _tiles[x, y].transform.position, enemyPrefab.transform.rotation, CharactersRoot.transform);

					continue;
				}
			}
		}
	}

	private void CreateInteractableElementAt(int x, int y, GameObject prefab, GameObject root)
	{
		Instantiate(prefab, _tiles[x, y].transform.position, prefab.transform.rotation, root.transform);
	}

	private List<List<string>> ParseTextToRows(string text)
	{
		var rows = new List<List<string>>();
		List<string> tempRows = text.Split(Environment.NewLine).ToList();
		tempRows.Remove(tempRows.Last());
		
		foreach (string row in tempRows)
		{
			List<string> tempRow = row.Split(',').ToList();
			tempRow.Remove(tempRow.Last());

			rows.Add(tempRow);
		}

		return rows;
	}
}
