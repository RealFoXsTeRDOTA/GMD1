using DefaultNamespace;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameSaver
{
  public static void SaveData(int levelIndex, int collectibles)
  {
    BinaryFormatter formatter = new BinaryFormatter();
    string path = Application.persistentDataPath + "/savedata.fun";
    FileStream stream = new FileStream(path, FileMode.Create);

    var saveData = new SaveData()
    {
      Level = levelIndex,
      Collectibles = collectibles
    };
    formatter.Serialize(stream, saveData);
    stream.Close();
  }

  public static SaveData LoadData()
  {
    string path = Application.persistentDataPath + "/savedata.fun";
    if (File.Exists(path))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(path, FileMode.Open);
      var savedData = formatter.Deserialize(stream) as SaveData;
      stream.Close();
      return savedData;
    }

    return new SaveData()
    {
      Level = 1,
      Collectibles = 0
    };
  }
}
