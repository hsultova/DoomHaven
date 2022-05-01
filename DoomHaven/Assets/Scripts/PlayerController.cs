using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ILivable
{
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
}
