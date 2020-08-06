using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
// singleton, not present anywhere except in MasterScene
using Debug = UnityEngine.Debug;

public class GameMaster : MonoBehaviour
{
    //static GameMaster ThisMaster;
    public static bool SceneLoaded = true;
    static Scene MasterScene;
    static List<GameObject> SceneHolders; // maximum of 2 for holding new scene and old scene objects
    static Dictionary<string, List<GameObject>> PersistentGameObjects; // destroy before key (level name)
    static GameObject MasterSceneHolder;
    static int CurrentLevel = -1;
    static string CurrentLevelName;
    // temp testing remove later start
    // temp testing remove later end
    float test = 0.0f;
    float lat = 3.0f;
    bool stat = false;
    bool tat = false;
    float zat = 5.0f;
    bool ztat = false;
    static Action ActionUnloadPrevScene; // for lean tween call back after scene transition
    void Awake()
    {
        PersistentGameObjects = new Dictionary<string, List<GameObject>>();
        ActionUnloadPrevScene += UnloadPrevScene; // action to call function to remove previous scene
        LeanTween.init(1600);
        // DataManagement
        DataManagement.InitData();
        //
        SceneHolders = new List<GameObject>();
        //ThisMaster = this.gameObject.GetComponent<GameMaster>();
        // separates master scene objects
        MasterScene = SceneManager.GetActiveScene();
        MasterSceneHolder = new GameObject("MasterSceneHolderObj");
        List<GameObject> GameMasterRootObjects = new List<GameObject>();
        MasterScene.GetRootGameObjects(GameMasterRootObjects);
        foreach (var i in GameMasterRootObjects)
            i.transform.SetParent(MasterSceneHolder.transform);

        SceneManager.sceneLoaded += OnSceneLoaded;
        GoToNextLevel(); // load menu
    }

    void Update()
    {
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //0) check additive scene loaded, this does it
        if (scene == MasterScene)
            return;
        SceneHolders.Add(new GameObject());
        //1) save information of current level to external csv file
        //2) push currentlevel objects to prev
        //3) successfully loaded new scene has objects moved to master scene transform
        SceneManager.SetActiveScene(scene);
        List<GameObject> RootObjects = new List<GameObject>();
        SceneManager.GetActiveScene().GetRootGameObjects(RootObjects);
        foreach (var i in RootObjects)
            SceneManager.MoveGameObjectToScene(i, MasterScene);
        SceneManager.SetActiveScene(MasterScene);
        SceneManager.UnloadSceneAsync(scene);
        foreach (var i in RootObjects)
            i.transform.SetParent(SceneHolders[SceneHolders.Count - 1].transform);

        // 
        if (PersistentGameObjects.ContainsKey(CurrentLevelName)) // dont let these objects get into the new level
        {
            if(PersistentGameObjects[CurrentLevelName] != null)
                foreach (var i in PersistentGameObjects[CurrentLevelName])
                    if (i != null)
                        i.transform.SetParent(SceneHolders[0].transform);
            PersistentGameObjects.Remove(CurrentLevelName);
        }
        //4) level loaded right side or below, move it there

        string Transitiontype = DataManagement.GetSceneData("TransitionInto_Type")[CurrentLevel];
        bool destroyprevsceneauto = false;
        switch (Transitiontype)
        {
            case "Horizontal":
                if (SceneHolders.Count > 1)
                {
                    SceneHolders[SceneHolders.Count - 1].transform.position = new Vector3(18.0f, 0.0f, SceneHolders[SceneHolders.Count - 1].transform.position.z);
                    SceneHolders[1].transform.SetParent(SceneHolders[0].transform);
                }
                LeanTween.moveLocalX(SceneHolders[0], -18.0f, 1f).setEaseOutQuad().setOnComplete(ActionUnloadPrevScene);
                destroyprevsceneauto = true;
                break;
            case "Vertical":
                if (SceneHolders.Count > 1)
                {
                    SceneHolders[SceneHolders.Count - 1].transform.position = new Vector3(0.0f, -10.0f, SceneHolders[SceneHolders.Count - 1].transform.position.z);
                    SceneHolders[1].transform.SetParent(SceneHolders[0].transform);
                }
                LeanTween.moveLocalY(SceneHolders[0], 10.0f, 1f).setEaseOutQuad().setOnComplete(ActionUnloadPrevScene);
                destroyprevsceneauto = true;
                break;
            case "FadeBlack":
                SceneHolders[SceneHolders.Count - 1].transform.position = new Vector3(18.0f, 0.0f, SceneHolders[SceneHolders.Count - 1].transform.position.z);
                GameObject FadeBScreen = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/FadeInFadeOutObject"));
                FadeBScreen.GetComponent<FadeInFadeOut>().CallBackFadeToBlack += FadeToBlackOnComplete;
                FadeBScreen.GetComponent<FadeInFadeOut>().CallBackFadeToBlack += ProtectPersistent;
                FadeBScreen.GetComponent<FadeInFadeOut>().fadecolour = Vector3.zero;
                destroyprevsceneauto = true;
                break;
            case "FadeWhite":
                SceneHolders[SceneHolders.Count - 1].transform.position = new Vector3(18.0f, 0.0f, SceneHolders[SceneHolders.Count - 1].transform.position.z);
                GameObject FadeWScreen = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/FadeInFadeOutObject"));
                FadeWScreen.GetComponent<FadeInFadeOut>().CallBackFadeToBlack += FadeToBlackOnComplete;
                FadeWScreen.GetComponent<FadeInFadeOut>().CallBackFadeToBlack += ProtectPersistent;
                FadeWScreen.GetComponent<FadeInFadeOut>().fadecolour = new Vector3(1.0f, 1.0f, 1.0f);
                destroyprevsceneauto = true;
                break;
            case "None":
                SceneHolders[SceneHolders.Count - 1].transform.position = new Vector3(0.0f, 0.0f, SceneHolders[SceneHolders.Count - 1].transform.position.z);
                destroyprevsceneauto = true;
                break;
            case "NoneFadeWhite":
                SceneHolders[SceneHolders.Count - 1].transform.position = new Vector3(0.0f, 0.0f, SceneHolders[SceneHolders.Count - 1].transform.position.z);
                GameObject FadeWScreen2 = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/FadeInFadeOutObject"));
                FadeWScreen2.transform.localScale = new Vector3(30,30,1);
                FadeWScreen2.GetComponent<FadeInFadeOut>().CallBackFadeToBlack += FadeToBlackOnComplete;
                FadeWScreen2.GetComponent<FadeInFadeOut>().CallBackFadeToBlack += ProtectPersistent;
                FadeWScreen2.GetComponent<FadeInFadeOut>().fadecolour = new Vector3(1.0f, 1.0f, 1.0f);
                destroyprevsceneauto = true;
                break;
            default:
                break;
        }
        if (destroyprevsceneauto == false)
        {
            UnloadPrevScene();
            ProtectPersistent();
        }   
        //5) perform transition based on command
        //6) destroy previous level after transition 
        // -> called in lean tween complete
    }
    static void FadeToBlackOnComplete() // callback from fade transition
    {
        SceneHolders[SceneHolders.Count - 1].transform.position = new Vector3(0.0f, 0.0f, SceneHolders[SceneHolders.Count - 1].transform.position.z);
        UnloadPrevScene();
    }
    static void UnloadPrevScene()
    {
        if (SceneHolders.Count > 1)
        {
            SceneHolders[1].transform.SetParent(null);
            Destroy(SceneHolders[0]);
            SceneHolders.RemoveAt(0);
        }
        SceneLoaded = true;
    }
    public static void GoToNextLevel()
    {
        List<string> LevelName = DataManagement.GetSceneData("Unity_SceneName");
        if((CurrentLevel + 1) < LevelName.Count)
        {
            SceneManager.LoadScene(CurrentLevelName = LevelName[++CurrentLevel], LoadSceneMode.Additive);
            SceneLoaded = false;
        }
    }
    public static void NotifyPersistent(GameObject PersistentObject, string DontPersistInThisLevel)
    {
        // check if such a level exist
        //List<string> abc = DataManagement.GetSceneData("Unity_SceneName");
        //foreach (var i in abc)
        //{
        //    Debug.Log("-->" + i + "<--");
        //    if (DontPersistInThisLevel == i)
        //        Debug.Log("match");
        //}
        if ( !DataManagement.GetSceneData("Unity_SceneName").Contains(DontPersistInThisLevel))
        {
            Debug.Log(DontPersistInThisLevel + "<- No such map exist for persistent object this to destroy itself");
            return;
        }
       
        if (PersistentObject == null)
            return;
        if (!PersistentGameObjects.ContainsKey(DontPersistInThisLevel))
            PersistentGameObjects[DontPersistInThisLevel] = new List<GameObject>();
        PersistentGameObjects[DontPersistInThisLevel].Add(PersistentObject);
    }
    static void ProtectPersistent()
    {
        foreach (var i in PersistentGameObjects) // let these objects persist across levels
            if (i.Value != null)
                foreach (var j in i.Value)
                    if (j != null)
                        j.transform.SetParent(null);
    }
}
