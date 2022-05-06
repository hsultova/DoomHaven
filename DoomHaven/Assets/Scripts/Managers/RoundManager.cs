using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
	public BoardManager BoardManager;

	[HideInInspector]
	public GameDataContext GameData;

	public int Rounds = 0;

	public void StartNewRound()
	{
		Rounds++;

		RollForInitiative();
	}

	private void RollForInitiative()
	{
		var initiativeCharacters = new List<IInitiative>();
		foreach (Transform child in BoardManager.CharactersRoot.transform)
		{
			var character = child.gameObject.GetComponent<IInitiative>();
			if (character == null)
			{
				continue;
			}

			initiativeCharacters.Add(character);
		}

		foreach (IInitiative character in initiativeCharacters)
		{

		}

		//sort descending
	}
}
