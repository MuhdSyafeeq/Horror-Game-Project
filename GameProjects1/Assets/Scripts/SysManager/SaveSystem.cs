using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SaveData(gameManager gm)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "/gamesettings.fun";
        Debug.Log("Setting Up FilePath: " + filePath);
        FileStream stream = new FileStream(filePath, FileMode.Create);

        formatter.Serialize(stream,
            gameManager.Instance.dataSens);

        formatter.Serialize(stream,
            gameManager.Instance.dataSoundVol);

        stream.Close();
    }

    public static void LoadData(gameManager gm)
    {
        string filePath = Application.persistentDataPath + "/gamesettings.fun";
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string[] FileRead = File.ReadAllLines(filePath);
            int value = 0;

            FileStream stream = new FileStream(filePath, FileMode.Open);

            foreach(string line in FileRead)
            {
                Debug.Log($"Data Line {value}: {line}");
                value += 1;
            }

            stream.Close();
        }
        else
        {
            Debug.LogError("Save File Not Existed with name:" + filePath);
        }
    }
}
