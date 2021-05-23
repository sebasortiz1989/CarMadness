using UnityEngine;
using Madness.SceneManagement;

public class ManagerLocator : MonoBehaviour
{
    //monobehaviours
    public SceneManager _sceneManager;

    //singletons

    public static ManagerLocator Instance
    {
        get
        {
            if (_instance == null) 
                _instance = new ManagerLocator();
            return _instance;
        }
        set { _instance = value; }
    }

    private static ManagerLocator _instance;

    public ManagerLocator()
    {

    }
}