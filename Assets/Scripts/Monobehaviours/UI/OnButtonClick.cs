using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class OnButtonClick : MonoBehaviour
{
	private readonly StringBuilder stringBuilder = new StringBuilder();


	public void OnSaveButtonClick(Text SerialNumber)
	{
		stringBuilder.Append("manualsave");
		stringBuilder.Append(SerialNumber.text);
		stringBuilder.Append(".json");

		string savePath = Path.Combine(Application.persistentDataPath, stringBuilder.ToString());

		stringBuilder.Remove(0, stringBuilder.Capacity);

		// TODO Call DataManager.SaveGame()
	}
}