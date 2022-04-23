using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class GridLayoutEditor : EditorWindow
{
	private string _fileName;

	private int _dimensions = 0;
	private string _defaultValue = string.Empty;
	private string[,] _grid;

	private bool _isGridGenerated = false;
	private bool _isFileLoaded = false;
	private bool _isFileSelected = false;

	private bool _showHelpBoxEnterDimensions = false;
	private bool _showHelpBoxSavedFile = false;
	private bool _showHelpBoxLoadedFile = false;

	[MenuItem("Window/Tools/Grid Layout Editor")]
	public static void ShowWindow()
	{
		GetWindow<GridLayoutEditor>(typeof(GridLayoutEditor));
	}

	void OnGUI()
	{
		ShowMessages();

		GUILayout.Space(20);

		_fileName = EditorGUILayout.TextField("File name:", _fileName);
		if (GUILayout.Button("Choose a file...", GUILayout.Width(100)))
		{
			_isFileSelected = true;
		}

		if (_isFileSelected)
		{
			_fileName = EditorUtility.OpenFilePanel("Choose file", _fileName, "txt");
			_isFileLoaded = false;
			_isFileSelected = false;
		}

		if (!string.IsNullOrEmpty(_fileName) && !_isFileLoaded)
		{
			if (File.Exists(_fileName))
			{
				LoadFile();
				_isFileLoaded = true;
				_showHelpBoxLoadedFile = true;
			}
		}

		GUILayout.Space(20);

		GUILayout.Label("Text field settings");
		_dimensions = EditorGUILayout.IntField("Dimensions:", _dimensions);
		_defaultValue = EditorGUILayout.TextField("Default value:", _defaultValue);

		if (GUILayout.Button("Generate", GUILayout.Width(100)))
		{
			_isGridGenerated = true;
			if (_dimensions <= 0)
			{
				_showHelpBoxEnterDimensions = true;
				return;
			}

			_showHelpBoxEnterDimensions = false;
			_showHelpBoxSavedFile = false;
			_showHelpBoxLoadedFile = false;

			InitializeGrid();
		}

		if (_isGridGenerated)
		{
			DrawGrid();

			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Save", GUILayout.Width(100)))
			{
				SaveGridToFile();
			}
		}
	}

	private void ShowMessages()
	{
		if (_showHelpBoxEnterDimensions)
		{
			EditorGUILayout.HelpBox("Enter positive value for dimensions.", MessageType.Error);
		}

		if (_showHelpBoxSavedFile)
		{
			EditorGUILayout.HelpBox("File saved successfully.", MessageType.Info);
		}

		if (_showHelpBoxLoadedFile)
		{
			EditorGUILayout.HelpBox("File loaded successfully.", MessageType.Info);
		}
	}

	private void LoadFile()
	{
		string text = File.ReadAllText(_fileName);
		string[] line = text.Trim().Split(Environment.NewLine);

		_dimensions = line.Length;
		_grid = new string[_dimensions, _dimensions];

		for (var row = 0; row < line.Length; row++)
		{
			var trimmedRow = line[row].Trim().Split(',').SkipLast(1).ToList();
			for (var column = 0; column < trimmedRow.Count; column++)
			{
				_grid[row, column] = trimmedRow[column].Trim();
			}
		}
	}

	private void InitializeGrid()
	{
		if (_grid == null || !File.Exists(_fileName))
		{
			_grid = new string[_dimensions, _dimensions];

			for (int row = 0; row < _dimensions; row++)
			{
				for (int column = 0; column < _dimensions; column++)
				{
					_grid[row, column] = _defaultValue;
				}
			}
		}
	}

	private void DrawGrid()
	{
		for (int row = 0; row < _dimensions; row++)
		{
			EditorGUILayout.BeginHorizontal();
			for (int column = 0; column < _dimensions; column++)
			{
				_grid[row, column] = EditorGUILayout.TextField(_grid[row, column], GUILayout.Width(50));
			}
			EditorGUILayout.EndHorizontal();
		}
	}

	private void SaveGridToFile()
	{
		var sb = new StringBuilder();

		for (int row = 0; row < _dimensions; row++)
		{
			for (int column = 0; column < _dimensions; column++)
			{
				sb.Append(_grid[row, column]);
				sb.Append(",");
			}
			sb.Append(Environment.NewLine);
		}

		string filePath = _isFileLoaded ? _fileName : $"{Directory.GetCurrentDirectory()}/Assets/Data/Levels/{_fileName}.txt";
		using FileStream fs = File.Create(filePath);
		fs.Write(Encoding.ASCII.GetBytes(sb.ToString()));
		_showHelpBoxSavedFile = true;
	}
}
