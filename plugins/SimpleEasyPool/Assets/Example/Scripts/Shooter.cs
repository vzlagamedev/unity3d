using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

    public float BulletSpeed;
    public float ShootTime;
    private float Counter;
    private float Timer;

    public PrefabKiller.Ptype Type;
	
	void Start ()
    {
        Counter = ShootTime;
	}
	
	
	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Space))
            Type = (Type == PrefabKiller.Ptype.B) ? PrefabKiller.Ptype.A : PrefabKiller.Ptype.B;


        Timer += Time.deltaTime; 
        if (Timer >= Counter )
        {
            if (Type == PrefabKiller.Ptype.A)
            {
                GameObject _clone = Manager.Instance.PoolA.GetGameObject(transform.position, Quaternion.identity);
                if (_clone != null)
                {
                    Rigidbody rb = _clone.GetComponent<Rigidbody>();
                    rb.AddForce(transform.TransformDirection(Vector3.forward * BulletSpeed));
                }
            }else if (Type == PrefabKiller.Ptype.B)
            {
                GameObject _clone = Manager.Instance.PoolB.GetGameObject(transform.position, Quaternion.identity);
                if (_clone != null)
                {
                    Rigidbody rb = _clone.GetComponent<Rigidbody>();
                    rb.AddForce(transform.TransformDirection(Vector3.forward * BulletSpeed));
                }
            }
            Counter += ShootTime;
        }
	}
}
