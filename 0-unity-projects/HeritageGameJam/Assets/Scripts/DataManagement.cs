using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Text.RegularExpressions;
// hansoo 
// this script will be used to load csv files for auto management of level data for example
// also to save and export player data
// "singleton"
public class DataManagement : MonoBehaviour
{
    //static string SceneManagementResource = "Assets/Resources/Data/SceneManagement.csv";
    static Dictionary<string, List<string>> SceneData;
    // Start is called before the first frame update
    public static void InitData()
    {
        SceneData = new Dictionary<string, List<string>>();
        var csvFile = Resources.Load<TextAsset>("Data/SceneManagement");
        //StreamReader read = new StreamReader(SceneManagementResource);
        //while (!read.EndOfStream)
        //{
        //    string line = read.ReadLine();
        //    string[] array = line.Split(',');
        //    if (array.Length == 0) // ensure there is data
        //        continue;
        //    if (array[0].Length == 0) // ensure there is key name
        //        continue;
        //    SceneData[array[0]] = new List<string>(); // add a key with container values
        //    for (int i = 1; i < array.Length; ++i) // add its data
        //    {
        //        string content = array[i].Replace("|", ",").Replace("\"\"\"", "\"");
        //        SceneData[array[0]].Add(content);
        //    }
        //}
        //var arrayString = csvFile.text.Split("\n"[0]);
        string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
        var arrayString = Regex.Split(csvFile.text, LINE_SPLIT_RE);
        foreach (var line in arrayString)
        {
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
//        Debug.Log("Debugging");
//        foreach(var i in SceneData)
//        {
//            Debug.Log("key -> " + i.Key);
//            foreach (var j in i.Value)
//                Debug.Log(i.Key + " -> " + j + "<-" + j.Length);
//        }
    }
    public static void debugdata()
    {
        foreach (var i in SceneData)
            Debug.Log(i.Key + "  " + i.Value[1]);
    }
    public static List<string> GetSceneData(string key)
    {
        if(SceneData == null)
        {
            Debug.Log("you have to play from master scene");
            return new List<string>();
        }
        return SceneData.ContainsKey(key) ? SceneData[key] : new List<string>();
    }
}
