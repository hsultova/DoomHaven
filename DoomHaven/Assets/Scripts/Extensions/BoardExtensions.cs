using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class BoardExtensions
{
	public static List<Tile> GetOrthogonalNeighbours(this BoardManager boardManager, int x, int y)
	{
		var tiles = new List<Tile>();

		if (x + 1 < boardManager.Rows)
		{
			tiles.Add(boardManager.Tiles[x + 1, y]);
		}
		
		if (x - 1 >= 0)
		{
			tiles.Add(boardManager.Tiles[x - 1, y]);
		}

		if (y + 1 < boardManager.Columns)
		{
			tiles.Add(boardManager.Tiles[x, y + 1]);
		}

		if (y - 1 >= 0)
		{
			tiles.Add(boardManager.Tiles[x, y + 1]);
		}

		return tiles;
	}
}
