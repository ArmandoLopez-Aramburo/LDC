using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;

public class SaveSystem : MonoBehaviour
{
    // Function that Saves the World data
    public void SaveWorld(WorldData data, int saveSlot)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "worlddata" + saveSlot + ".poop");
        FileStream file = File.Create(path);

        try
        {
            formatter.Serialize(file, data);
        }
        catch (SerializationException e)
        {
            Debug.Log("FAILED TO SERIALIZE: " + e.Message);
        }
        finally
        {
            file.Close();
        }
    }

    // Function used to load the data from the file
    public WorldData LoadWorld(int loadSlot)
    {
        string path = Path.Combine(Application.persistentDataPath, "worlddata" + loadSlot + ".poop");
        using (FileStream stream = File.Open(path, FileMode.Open))
        {
            try
            {
                if (File.Exists(path))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(stream) as WorldData;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Save File Not Found in " + path);
            }
        }
        return null;
    }
}