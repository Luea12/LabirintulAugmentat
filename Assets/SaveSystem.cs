using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(ProfileData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        string path = Application.persistentDataPath + "/profile.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ProfileData Load()
    {
        string path = Application.persistentDataPath + "/profile.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ProfileData data = formatter.Deserialize(stream) as ProfileData;
            stream.Close();

            return data;
        } else
        {
            // Create a default profile
            ProfileData defaultProfile = ProfileData.Default();
            Save(defaultProfile);

            return defaultProfile;
        }
    }
}
