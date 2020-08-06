using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoNextScene : MonoBehaviour
{
    public void GoNextLevel()
    {
        GameMaster.GoToNextLevel();
        this.gameObject.SetActive(false);
    }
}
