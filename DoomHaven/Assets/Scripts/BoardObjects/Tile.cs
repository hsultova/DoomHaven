using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public Action<Tile, bool> TileHovered;
	public Action<Tile> TileClicked;

	public GameObject Visuals;
	public TMP_Text DebugText;

	public SpriteRenderer BackgroundSprite;
	public SpriteRenderer ForegroundSprite;
	public SpriteRenderer InteractibleSprite;

	public SpriteRenderer HoverSprite;
	public Animator HoverAnimator;

	public TileWalkableState WalkableState = TileWalkableState.NonWalkable;

	public int X;
	public int Y;

	public IController CurrentController;

	public bool IsEmpty => CurrentController != null;

	// Start is called before the first frame update
	void Start()
	{
		HoverAnimator.keepAnimatorControllerStateOnDisable = false;

		HoverSprite.enabled = false;
		HoverTile(false);
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void HoverTile(bool isTileHovered)
	{
		if (isTileHovered)
		{
			//reset animation when the tile is hovered
			HoverAnimator.Play("TileHighlightAnim", 0, 0f);
		}

		HoverSprite.enabled = isTileHovered;
		HoverAnimator.enabled = isTileHovered;
	}

	private void OnMouseEnter()
	{
		HoverTile(true);

		TileHovered?.Invoke(this, true);
	}

	private void OnMouseExit()
	{
		HoverTile(false);

		TileHovered?.Invoke(this, false);
	}

	private void OnMouseDown()
	{
		TileClicked?.Invoke(this);
	}
}
