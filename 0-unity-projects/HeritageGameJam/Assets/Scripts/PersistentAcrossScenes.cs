using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// han
// this script tells game master he can exist between several scenes.
public class PersistentAcrossScenes : MonoBehaviour
{
    public string DestroyThisBeforeThisLevel = "NonExistentLevelByDefault";
    void Awake()
    {
        GameMaster.NotifyPersistent(this.gameObject, DestroyThisBeforeThisLevel);
    }
}
