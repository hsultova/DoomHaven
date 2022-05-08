using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
	public BoardManager BoardManager;

	[HideInInspector]
	public GameDataContext GameData;

	public int Rounds = 0;

	public int Turns = 0;
	public List<IController> ActingControllersInTurn = new List<IController>();
	public List<IController> AlreadyActedControllersInTurn = new List<IController>();
	public IController CurrentActingController;
	private int _currentActingTurn = -1;

	public void StartNewRound()
	{
		Rounds++;

		ActingControllersInTurn.Clear();
		AlreadyActedControllersInTurn.Clear();

		_currentActingTurn = -1;
		CurrentActingController = null;

		RollForInitiative();

		Debug.Log($"Starting round: {Rounds}");
	}

	public void StartNewTurn()
	{
		Turns++;
		_currentActingTurn++;

		CurrentActingController = ActingControllersInTurn[_currentActingTurn];

		Debug.Log($"Starting Turn: {Turns}{Environment.NewLine}Acting controllers {_currentActingTurn+1}/{ActingControllersInTurn.Count}");
	}

	public void FinishTurn()
	{
		AlreadyActedControllersInTurn.Add(CurrentActingController);

		CurrentActingController = null;

		Debug.Log($"Finishing turn: {Turns}");
	}

	private void RollForInitiative()
	{
		var initiativeCharacters = new List<IInitiative>();
		foreach (Transform child in BoardManager.CharactersRoot.transform) {
			var character = child.gameObject.GetComponent<IInitiative>();
			if (character == null) {
				continue;
			}

			initiativeCharacters.Add(character);
		}

		foreach (IInitiative character in initiativeCharacters) {
			character.Initiative = RollD20();
		}

		initiativeCharacters.Sort();

		ActingControllersInTurn = initiativeCharacters.Cast<IController>().ToList();
	}

	private int RollD20()
	{
		int roll = UnityEngine.Random.Range(1, 21);
		return roll;
	}
}
