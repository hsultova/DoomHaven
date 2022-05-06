using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
	public TextAsset GameDataRepository;
	public AssetDataContext AssetData;
	public GameDataContext GameData;

	public BoardManager BoardManager;
	public RoundManager RoundManager;
	public UIManager UIManager;

	// Start is called before the first frame update
	void Start()
	{
		LoadDebugLevel();
	}

	// Update is called once per frame
	void Update()
	{
	}

	public void LoadLevel(string levelID)
	{
		LevelData levelData = GameData.Levels.GetDataByID(levelID);
		if (levelData == null)
		{
			Debug.Log($"Couldn't find level by the specified ID: {levelID}");
		}

		BoardManager.LoadLevel(levelData, GameData, AssetData);
	}

#if UNITY_EDITOR
	[ContextMenu("Serialize Context")]
	public void SerializeContext()
	{
		string jsonGameData = JsonUtility.ToJson(GameData, true);
		File.WriteAllText(AssetDatabase.GetAssetPath(GameDataRepository), jsonGameData);
		EditorUtility.SetDirty(GameDataRepository);
	}
#endif

	[ContextMenu("Deserialize Context")]
	public void DeserializeContext()
	{
		if (!string.IsNullOrEmpty(GameDataRepository.text))
		{
			GameData = JsonUtility.FromJson<GameDataContext>(GameDataRepository.text);
		}
		else
		{
			GameData = new GameDataContext();
		}
	}

	[ContextMenu("Load Debug Level")]
	public void LoadDebugLevel()
	{
		LoadLevel("lvl0");
	}
}
