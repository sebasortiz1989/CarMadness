using UnityEngine;

public class ManagerLocator : MonoBehaviour
{
    //monobehaviours
    public UIManager _uiManager;

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