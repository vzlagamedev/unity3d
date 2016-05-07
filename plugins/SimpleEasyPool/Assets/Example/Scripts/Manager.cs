using UnityEngine;
using System.Collections;
using SimpleEasyPool;

public class Manager : MonoBehaviour {

    // Singleton -->

    private static Manager _manager;
    public static Manager Instance
    {
        get
        {
            if (_manager == null)
                _manager = FindObjectOfType<Manager>();
            return _manager;
        }
    }
    // <--


    public GameObject gameObjectA;
    public GameObject gameObjectB;

    public EasyPool PoolA;
    public EasyPool PoolB;

    public int MaxItems;

	void Awake ()
    {
        PoolA = new EasyPool(MaxItems, gameObjectA);
        PoolB = new EasyPool(MaxItems, gameObjectB);
	}
}
