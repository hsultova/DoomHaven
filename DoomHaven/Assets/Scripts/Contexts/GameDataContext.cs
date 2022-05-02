using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameDataContext
{
	public List<LevelData> Levels;
	public List<PlayerData> Players;
	public List<EnemyData> Enemies;
	public List<CardData> Cards;

	public List<CardData> GetCardsFromData(List<string> cardIDs)
	{
		var cards = new List<CardData>();
		foreach (string cardId in cardIDs) {
			CardData card = Cards.GetDataByID(cardId);
			if (card == null) {
				Debug.LogError($"'{cardId}' could not be found in the game data.");
				continue;
			}

			cards.Add(card);
		}

		return cards;
	}
}
