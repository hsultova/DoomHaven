using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelParser
{
	public List<List<string>> WalkLayer = new List<List<string>>();
	public List<List<string>> EnvLayer = new List<List<string>>();
	public List<List<string>> InteractableLayer = new List<List<string>>();

	public void ParseLevel(LevelData levelData)
	{
		WalkLayer = ParseTextToRows(levelData.WalkLayer.text);
		EnvLayer = ParseTextToRows(levelData.EnvironmentLayer.text);
		InteractableLayer = ParseTextToRows(levelData.InteractableLayer.text);
	}

	public string GetStateForXY(LevelLayer layer, int x, int y)
	{
		if (layer == LevelLayer.Walkable)
		{
			return WalkLayer[x][y];
		}
		else if (layer == LevelLayer.Interactions)
		{
			return InteractableLayer[x][y];
		}
		else if (layer == LevelLayer.Environment)
		{
			return EnvLayer[x][y];
		}

		Debug.LogError($"{layer.ToString()} is non supported.");
		return null;
	}

	public int GetLevelDimensions()
	{
		if (WalkLayer.Count == 0) {
			//Level is not yet parsed
			return -1;
		}

		return WalkLayer.Count;
	}

	private List<List<string>> ParseTextToRows(string text)
	{
		var rows = new List<List<string>>();
		List<string> tempRows = text.Split(Environment.NewLine).ToList();
		tempRows.Remove(tempRows.Last());

		foreach (string row in tempRows) {
			List<string> tempRow = row.Split(',').ToList();
			tempRow.Remove(tempRow.Last());

			rows.Add(tempRow);
		}

		return rows;
	}
}

public enum LevelLayer
{
	Walkable,
	Environment,
	Interactions,
}
