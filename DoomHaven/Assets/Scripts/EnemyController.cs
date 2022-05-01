using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ILivable
{
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
}
