using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UnityEngine.UI;
using System.Collections;


public class Save : MonoBehaviour
{
	public Text text_b;

	private IEnumerator Changetext(float W_time, Text text_b)
	{
		text_b.text = "Brak zapisu";
		yield return new WaitForSeconds(W_time);
		text_b.text = "Kontynuuj";
	}

	void Start()
	{
		//SaveFile();
		//LoadFile();
	}

	public void SaveFile(ReactiveProperty<int> health, ReactiveProperty<int> food, ReactiveProperty<int> gold, ReactiveProperty<string> quest, ReactiveProperty<int> steps,string level)
	{
		string destination = Application.persistentDataPath + "/save.dat";
		FileStream file;

		if (File.Exists(destination)) file = File.OpenWrite(destination);
		else file = File.Create(destination);

		GameData data = new GameData(health, food, gold,quest,steps,level);
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, data);
		file.Close();
	}

	public void LoadFile()
	{
		string destination = Application.persistentDataPath + "/save.dat";
		FileStream file;

		if (File.Exists(destination)) file = File.OpenRead(destination);
		else
		{
			StartCoroutine(Changetext(5f, text_b));
			return;
		}

		BinaryFormatter bf = new BinaryFormatter();
		GameData data = (GameData)bf.Deserialize(file);
		file.Close();

		SceneManager.LoadScene(data.Level);
	}

	public void LoadDatatoData(ReactiveProperty<int> health, ReactiveProperty<int> food, ReactiveProperty<int> gold,ReactiveProperty<string> quest, ReactiveProperty<int> steps)
	{
		string destination = Application.persistentDataPath + "/save.dat";
		FileStream file;

		if (File.Exists(destination)) file = File.OpenRead(destination);
		else
		{
			Debug.LogError("File not found");
			return;
		}

		BinaryFormatter bf = new BinaryFormatter();
		GameData data = (GameData)bf.Deserialize(file);

		health.Value = data.Health.Value;
		food.Value = data.Food.Value;
		gold.Value = data.Gold.Value;
		quest.Value = data.Quest.Value;
		steps.Value = data.Steps.Value;

		file.Close();
	}
}