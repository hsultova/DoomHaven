using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ILivable, IController
{
	public Tile CurrentTile { get; set; }
	public int Health { get; set; }
	public int Attack { get; set; }
	public int Movement { get; set; }
	public List<string> Cards { get; set; }
	public List<string> DiscardedCards { get; set; }

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
		var enemyData = data as EnemyData;
		if (enemyData == null)
		{
			Debug.Log("Trying to set data that is not EnemyData.");
			return;
		}

		Health = enemyData.Health;
		Attack = enemyData.Attack;
		Movement = enemyData.Movement;
		Cards = enemyData.Cards;
	}
}
