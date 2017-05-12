using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;


public class DataManager : MonoBehaviour {

	public GameObject mouse;

	private PlayerData playerData;
	private SystemData systemData;
	private SaveDataWithHash saveDataWithHash;

	private string manualSavePath;
	private string quickSavePath;

	private SHA256Managed crypt;
	private StringBuilder stringBuilder;

	private const int totalTempSaveNum = 3;


	private void Awake()
	{
		playerData = new PlayerData();
		systemData = new SystemData();
		saveDataWithHash = new SaveDataWithHash();

		manualSavePath = Path.Combine(Application.persistentDataPath, "save.json");
		quickSavePath = Path.Combine(Application.persistentDataPath, "quick_save0.json");

		crypt = new SHA256Managed();
		stringBuilder = new StringBuilder();
	}

	private void OnApplicationPause(bool isPaused)
	{
		if (isPaused)
		{
			Debug.Log("Pause");
			SaveGame(quickSavePath);
		}
	}

	private void OnApplicationQuit()
	{ 
		Debug.Log("Quit");
		SaveGame(quickSavePath);
	}

	public void OnSaveButtonClick()
	{
		Debug.Log("Save Button Click");
		SaveGame(manualSavePath);
	}

	public void OnLoadButtonClick()
	{		
		Debug.Log("Load Button Click");
		LoadGame();
	}

	private void SaveGame(string savePath)
	{
		playerData.position = mouse.transform.position;
		playerData.rotation = mouse.transform.rotation;

		systemData.savedTime = DateTime.Now.ToString();

		saveDataWithHash.playerData = playerData;
		saveDataWithHash.systemData = systemData;
		saveDataWithHash.hashOfContents = GenerateHash(playerData);

		File.WriteAllText(savePath, JsonUtility.ToJson(saveDataWithHash, true));


		// TODO savepath for screenshot
		Application.CaptureScreenshot(Path.Combine(Application.persistentDataPath, "save.png"));


		Debug.Log("Game Saved: " + savePath);
	}

	private void LoadGame()
	{
		saveDataWithHash = JsonUtility.FromJson<SaveDataWithHash>(File.ReadAllText(manualSavePath));
		playerData = saveDataWithHash.playerData;
		systemData = saveDataWithHash.systemData;

		string hash = GenerateHash(playerData);

		if (hash != saveDataWithHash.hashOfContents)
			Debug.Log("playerData has been modified!");		

		mouse.transform.position = playerData.position;
		mouse.transform.rotation = playerData.rotation;

		Debug.Log("Game Loaded!");
	}

	private string GenerateHash(PlayerData _saveData)
	{
		string saveDataString = JsonUtility.ToJson(_saveData, true);
		string hash = string.Empty;

		byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(saveDataString), 0, Encoding.UTF8.GetByteCount(saveDataString));

		foreach (byte bit in crypto)
		{
			hash += bit.ToString("x2");
		}

		return hash;
	}
}