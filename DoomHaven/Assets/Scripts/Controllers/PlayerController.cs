using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ILivable, IController, IInitiative
{
	public Tile CurrentTile { get; set; }
	public string DisplayName { get; set; }
	public int Health { get; set; }
	public int Attack { get; set; }
	public int Movement { get; set; }
	public int Initiative { get; set; }

	public List<CardData> Cards;
	public List<CardData> DiscardedCards;
	public List<CardData> LostCards;
	public List<CardData> CurrentCards;

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
		var playerData = data as PlayerData;
		if (playerData == null)
		{
			Debug.Log("Trying to set data that is not PlayerData.");
			return;
		}

		DisplayName = playerData.DisplayName;
		Health = playerData.Health;
		Attack = playerData.Attack;
		Movement = playerData.Movement;

		Cards = gameDataContext.GetCardsFromData(playerData.Cards);
	}
}
