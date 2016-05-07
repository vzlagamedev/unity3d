using UnityEngine;
using System.Collections;

public class PrefabKiller : MonoBehaviour {

    public enum Ptype
    {
        A,
        B
    }

    public Ptype Type;

    void OnCollisionEnter(Collision Other)
    {
        if (Other.gameObject.tag != gameObject.tag)
        {
            if (Type == Ptype.A)
            {
                Manager.Instance.PoolA.KillPoolElement(gameObject);
            }else if (Type == Ptype.B)
            {
                Manager.Instance.PoolB.KillPoolElement(gameObject);
            }
        }
    }
	
}
