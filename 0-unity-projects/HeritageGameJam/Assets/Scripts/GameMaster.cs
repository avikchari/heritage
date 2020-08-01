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
    static Scene MasterScene;
    static List<GameObject> SceneHolders; // maximum of 2 for holding new scene and old scene objects
    static GameObject MasterSceneHolder;
    static int CurrentLevel = -1;
    // temp testing remove later start
    // temp testing remove later end
    float test = 1.0f;
    float lat = 3.0f;
    bool stat = false;
    bool tat = false;
    static Action ActionUnloadPrevScene; // for lean tween call back after scene transition
    void Awake()
    {
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
        if ((test -= Time.deltaTime) < 0 && stat == false)
        {
            stat = true;
            Debug.Log("next");
            GoToNextLevel();
            //SceneManager.LoadScene("TestScene2", LoadSceneMode.Additive);
        }
        if ((lat -= Time.deltaTime) < 0 && tat == false)
        {
            tat = true;
            Debug.Log("next");
            GoToNextLevel();
            //SceneManager.LoadScene("TestScene3", LoadSceneMode.Additive);
        }
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
        //4) level loaded right side or below, move it there
        
        string Transitiontype = DataManagement.GetSceneData("TransitionInto_Type")[CurrentLevel];
        bool destroyprevsceneinleantween = false;
        switch (Transitiontype)
        {
            case "Horizontal":
                if (SceneHolders.Count > 1)
                {
                    SceneHolders[SceneHolders.Count - 1].transform.position = new Vector3(10.0f, 0.0f, SceneHolders[SceneHolders.Count - 1].transform.position.z);
                    SceneHolders[1].transform.SetParent(SceneHolders[0].transform);
                }
                //LeanTween.moveLocalX(SceneHolders[SceneHolders.Count - 1], -10.0f, 1f).setEaseOutQuad();
                LeanTween.moveLocalX(SceneHolders[0], -10.0f, 1f).setEaseOutQuad().setOnComplete(ActionUnloadPrevScene);
                destroyprevsceneinleantween = true;
                break;
            case "Vertical":
                if (SceneHolders.Count > 1)
                {
                    SceneHolders[SceneHolders.Count - 1].transform.position = new Vector3(0.0f, -5.0f, SceneHolders[SceneHolders.Count - 1].transform.position.z);
                    SceneHolders[1].transform.SetParent(SceneHolders[0].transform);
                }
                //LeanTween.moveLocalY(SceneHolders[SceneHolders.Count - 1], 5.0f, 1f).setEaseOutQuad();
                LeanTween.moveLocalY(SceneHolders[0], 5.0f, 1f).setEaseOutQuad().setOnComplete(ActionUnloadPrevScene);
                destroyprevsceneinleantween = true;
                break;
            case "Fade":
                break;
            default:
                break;
        }
        if (destroyprevsceneinleantween == false)
            UnloadPrevScene();
        //5) perform transition based on command
        //6) destroy previous level after transition 
        // -> called in lean tween complete
    }
    static void UnloadPrevScene()
    {
        if (SceneHolders.Count > 1)
        {
            SceneHolders[1].transform.SetParent(null);
            Destroy(SceneHolders[0]);
            SceneHolders.RemoveAt(0);
        }
    }
    public static void GoToNextLevel()
    {
        List<string> LevelName = DataManagement.GetSceneData("Unity_SceneName");
        if((CurrentLevel + 1) < LevelName.Count)
            SceneManager.LoadScene(LevelName[++CurrentLevel], LoadSceneMode.Additive);
    }
}
