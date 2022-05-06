using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInformation : MonoBehaviour
{
	public BoardManager BoardManager;
	public Text Name;
	public Text Health;
	public Text Attack;

	// Start is called before the first frame update
	void Start()
	{
		gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Initialize()
	{
		BoardManager.BoardReady += OnBoardReady;
	}

	private void OnBoardReady()
	{
		for (int x = 0; x < BoardManager.Rows; x++)
		{
			for (int y = 0; y < BoardManager.Columns; y++)
			{
				BoardManager.Tiles[x, y].TileHovered += OnTileHovered;
			}
		}
	}

	private void OnTileHovered(Tile tile, bool isHovered)
	{
		if(isHovered)
		{
			IController controller = tile.CurrentController;
			if (controller == null)
			{
				gameObject.SetActive(false);
				return;
			}
			else if (controller is PlayerController player)
			{
				Name.text = player.DisplayName;
				Health.text = player.Health.ToString();
				Attack.text = player.Attack.ToString();
			}
			else if (controller is EnemyController enemy)
			{
				Name.text = enemy.DisplayName;
				Health.text = enemy.Health.ToString();
				Attack.text = enemy.Attack.ToString();
			}

			gameObject.SetActive(true);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}
}
