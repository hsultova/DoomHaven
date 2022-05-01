public interface IController
{
	Tile CurrentTile { get; set; }
	void SetData(IAsset data);
}
