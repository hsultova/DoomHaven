using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ILivable, IController, IInitiative, IComparable
{
	public Tile CurrentTile { get; set; }
	public string DisplayName;
	public int Health { get; set; }
	public int Attack { get; set; }
	public int Movement { get; set; }
	public int Initiative { get; set; }

	public List<CardData> Cards;
	public List<CardData> DiscardedCards;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	public void SetData(IAsset data, GameDataContext gameDataContext)
	{
		var enemyData = data as EnemyData;
		if (enemyData == null)
		{
			Debug.Log("Trying to set data that is not EnemyData.");
			return;
		}

		DisplayName = enemyData.DisplayName;
		Health = enemyData.Health;
		Attack = enemyData.Attack;
		Movement = enemyData.Movement;
		Cards = gameDataContext.GetCardsFromData(enemyData.Cards);
	}

	public int CompareTo(object compareTo)
	{
		if (compareTo is PlayerController pc)
		{
			if (Initiative > pc.Initiative)
				return -1;

			if (Initiative < pc.Initiative)
				return 1;

			if (Initiative == pc.Initiative)
				return 0;
		}
		else if (compareTo is EnemyController ec)
		{
			if (Initiative > ec.Initiative)
				return -1;

			if (Initiative < ec.Initiative)
				return 1;

			if (Initiative == ec.Initiative)
				return 0;
		}

		return 0;
	}
}
