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

	public Tile CurrentHoveredTile;
	public Tile LastClickedTile;

	private Tile[,] _tiles;
	private GameDataContext _gameData;
	private AssetDataContext _assetData;

	private LevelParser _levelParser;

	public Tile[,] Tiles => _tiles;
	public Action BoardReady;

	public void LoadLevel(LevelData level, GameDataContext gameData, AssetDataContext assetData)
	{
		_assetData = assetData;
		_gameData = gameData;

		_levelParser = new LevelParser();
		_levelParser.ParseLevel(level);

		//Init the grid with the proper dimensions and empty cells
		int levelDimensions = _levelParser.GetLevelDimensions();
		if (levelDimensions < 0)
		{
			Debug.LogError($"Level is not yet parsed. Level dimensions are {levelDimensions}.");

			return;
		}

		Rows = Columns = levelDimensions;
		_tiles = new Tile[Rows, Columns];

		InitBoardWithTiles(DefaultTile);

		//Prepare the layer to get proper tile states for walkability
		PrepareWalkLayer();
		PrepareEnvLayer();
		PrepareInteractableLayer();
		BoardReady?.Invoke();
	}

	private void InitBoardWithTiles(GameObject tilePrefab)
	{
		foreach (Transform child in BoardRoot.transform)
		{
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

				_tiles[x, y].TileClicked += OnTileClicked;
				_tiles[x, y].TileHovered += OnTileHovered;
			}
		}
	}

	private void OnTileHovered(Tile tile, bool isTileHovered)
	{
		if (isTileHovered)
		{
			CurrentHoveredTile = tile;
		}
	}

	private void OnTileClicked(Tile tile)
	{
		LastClickedTile = tile;
		IController controller = tile.CurrentController;
		if (controller == null)
		{
			Debug.Log("Empty tile.");
		}
		else if (controller is PlayerController player)
		{
			Debug.Log($"Tile with palyer.{Environment.NewLine} Name: {player.DisplayName} Health: {player.Health} Attack: {player.Attack}");
		}
		else if (controller is EnemyController enemy)
		{
			Debug.Log($"Tile with enemy.{Environment.NewLine}Name: {enemy.DisplayName} Health: {enemy.Health} Attack: {enemy.Attack}");
		}
	}

	private void PrepareWalkLayer()
	{
		string nonWalkableTile = "0";
		string walkableTile = "1";

		for (int x = 0; x < Rows; x++)
		{
			for (int y = 0; y < Columns; y++)
			{
				string state = _levelParser.GetStateForXY(LevelLayer.Walkable, x, y);

				if (state == nonWalkableTile)
				{
					_tiles[x, y].WalkableState = TileWalkableState.NonWalkable;
				}
				else if (state == walkableTile)
				{
					_tiles[x, y].WalkableState = TileWalkableState.Walkable;
				}
			}
		}
	}

	private void PrepareEnvLayer()
	{
		for (int x = 0; x < Rows; x++)
		{
			for (int y = 0; y < Columns; y++)
			{
				string state = _levelParser.GetStateForXY(LevelLayer.Environment, x, y);

				AssetData data = _assetData.Assets.GetDataByID(state);
				if (data != null)
				{
					Sprite sprite = data.GetSpriteAtRandom();
					if (sprite == null)
					{
						continue;
					}

					_tiles[x, y].BackgroundSprite.sprite = sprite;
				}
			}
		}
	}

	private void PrepareInteractableLayer()
	{
		for (int x = 0; x < Rows; x++)
		{
			for (int y = 0; y < Columns; y++)
			{
				string state = _levelParser.GetStateForXY(LevelLayer.Interactions, x, y);

				PlayerData player = _gameData.Players.GetDataByID(state);
				if (player != null)
				{
					GameObject playerPrefab = _assetData.GetAssetPrefabByID(player.PrefabID);
					CreateControllerAt<PlayerController>(x, y, playerPrefab, CharactersRoot, player);

					continue;
				}

				EnemyData enemy = _gameData.Enemies.GetDataByID(state);
				if (enemy != null)
				{
					GameObject enemyPrefab = _assetData.GetAssetPrefabByID(enemy.PrefabID);
					CreateControllerAt<EnemyController>(x, y, enemyPrefab, CharactersRoot, enemy);

					continue;
				}
			}
		}
	}

	private void CreateControllerAt<T>(int x, int y, GameObject prefab, GameObject root, IAsset data)
		where T : IController
	{
		GameObject gameObject = Instantiate(prefab, _tiles[x, y].transform.position, prefab.transform.rotation, root.transform);

		T controller = gameObject.GetComponent<T>();
		controller.CurrentTile = _tiles[x, y];
		controller.SetData(data, _gameData);

		_tiles[x, y].CurrentController = controller;
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
