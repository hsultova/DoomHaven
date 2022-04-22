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

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

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
		if (!string.IsNullOrEmpty(GameDataRepository.text)) {
			GameData = JsonUtility.FromJson<GameDataContext>(GameDataRepository.text);
		}
		else {
			GameData = new GameDataContext();
		}
	}
}
