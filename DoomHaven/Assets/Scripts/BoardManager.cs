using UnityEngine;

public class BoardManager : MonoBehaviour
{
	public GameObject DebugTile;
	public GameObject DefaultTile;
	public GameObject Root;
	public int Rows;
	public int Columns;

	private Tile[,] _tiles;

	public void InitBoardWithTiles(GameObject tilePrefab)
	{
		_tiles = new Tile[Rows, Columns];

		for (int x = 0; x < Rows; x++)
		{
			for (int y = 0; y < Columns; y++)
			{
				GameObject newTileVisuals = Instantiate(tilePrefab, new Vector3(y, Rows - x, 0), tilePrefab.transform.rotation, Root.transform);

				_tiles[x, y] = newTileVisuals.GetComponent<Tile>();
				_tiles[x, y].X = x;
				_tiles[x, y].Y = y;
				_tiles[x, y].Visuals = newTileVisuals;
				_tiles[x, y].DebugText.text = $"{_tiles[x, y].X}x{_tiles[x, y].Y}";
			}
		}
	}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
