using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;

public static class SaveSystem
{
    // Function that Saves the village data
    public static void SaveWorld(VillageBuilder village)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "worlddata.poop");
        FileStream file = File.Create(path);

        VillageData data = new VillageData(village);

        try
        {
            formatter.Serialize(file, data);
        }
        catch(SerializationException e)
        {
            Debug.Log("FAILED TO SERIALIZE: " + e.Message);
        }
        finally
        {
            file.Close();
        }
    }

    // Function used to load the data from the file
    public static VillageData LoadWorld()
    {
        string path = Path.Combine(Application.persistentDataPath, "worlddata.poop");
        using (FileStream stream = File.Open(path, FileMode.Open))
        {
            try
            {
                if (File.Exists(path))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(stream) as VillageData;
                }
            }
            catch(Exception e)
            {
                Debug.LogError("Save File Not Found in " + path);
            }
        }
        
        return null;
    }
}