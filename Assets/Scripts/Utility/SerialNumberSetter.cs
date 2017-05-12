using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class SerialNumberSetter : MonoBehaviour
{
	private void Start()
	{
		SerialNumSetter_1();
	}

	public void SerialNumSetter_0()
	{
		GameObject[] SerialNumGOs = GameObject.FindGameObjectsWithTag("SerialNum");

		for (var i = 0; i < SerialNumGOs.Length; i++)
		{
			if (i/10%2 == 0)
				SerialNumGOs[SerialNumGOs.Length - i - 1].GetComponent<Text>().text = (i + 1).ToString();
			else
				SerialNumGOs[SerialNumGOs.Length - i - 1].GetComponent<Text>().text = (i/10*10 + 10 - i%10).ToString();
		}
	}

	public void SerialNumSetter_1()
	{
		GameObject[] SerialNumGOs = GameObject.FindGameObjectsWithTag("SerialNum");

		for (var i = 0; i < SerialNumGOs.Length; i++)
		{
			SerialNumGOs[i].gameObject.GetComponent<Text>().text = (SerialNumGOs.Length - i).ToString();
		}
	}
}