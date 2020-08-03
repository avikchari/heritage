using UnityEngine;

//Disable the whole gameobject if not running in editor mode
public class DisableOnRelease : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (!Application.isEditor)
        {
            this.gameObject.SetActive(false);
        }
    }
}
