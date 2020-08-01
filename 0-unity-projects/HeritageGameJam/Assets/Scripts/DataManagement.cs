using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
// hansoo 
// this script will be used to load csv files for auto management of level data for example
// also to save and export player data
// "singleton"
public class DataManagement : MonoBehaviour
{
    static string SceneManagementResource = "Assets/Data/SceneManagement.csv";
    static Dictionary<string, List<string>> SceneData;
    // Start is called before the first frame update
    public static void InitData()
    {
        SceneData = new Dictionary<string, List<string>>();
        StreamReader read = new StreamReader(SceneManagementResource);
        while (!read.EndOfStream)
        {
            string line = read.ReadLine();
            string[] array = line.Split(',');
            if (array.Length == 0) // ensure there is data
                continue;
            if (array[0].Length == 0) // ensure there is key name
                continue;
            SceneData[array[0]] = new List<string>(); // add a key with container values
            for (int i = 1; i < array.Length; ++i) // add its data
            {
                string content = array[i].Replace("|", ",").Replace("\"\"\"", "\"");
                SceneData[array[0]].Add(content);
            }
        }
    }
    public static void debugdata()
    {
        foreach (var i in SceneData)
            Debug.Log(i.Key + "  " + i.Value[1]);
    }
    public static List<string> GetSceneData(string key)
    {
        return SceneData.ContainsKey(key) ? SceneData[key] : new List<string>();
    }
}
