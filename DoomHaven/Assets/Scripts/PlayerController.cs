using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ILivable, IController
{
	public Tile CurrentTile { get; set; }
	public int Health { get; set; }
	public int Attack { get; set; }
	public int Movement { get; set; }
	public List<string> Cards { get; set; }
	public List<string> DiscardedCards { get; set; }
	public List<string> LostCards { get; set; }
	public List<string> CurrentCards { get; set; }

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	public void SetData(IAsset data)
	{
		var playerData = data as PlayerData;
		if (playerData == null)
		{
			Debug.Log("Trying to set data that is not PlayerData.");
			return;
		}

		Health = playerData.Health;
		Attack = playerData.Attack;
		Movement = playerData.Movement;
		Cards = playerData.Cards;
	}
}
