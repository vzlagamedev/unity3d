/*
    Simple Easy Pool v1.0
    Creado por: Pedro Josue Duran Medina 
    Este Plugin Permite Manejare el Object Pooling ( https://es.wikipedia.org/wiki/Object_pool_(patr�n_de_dise�o ) 
    entre GameObjects en unity 3d Facilmente.  
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SimpleEasyPool
{
    public class EasyPool
    {
        public int PoolMaxItems;
        public GameObject Defaultprefab;

        List<GameObject> Aviable = new List<GameObject>();
        List<GameObject> Unaviable = new List<GameObject>();

        public delegate void OnGetPoolEvent(GameObject Go);
        /// <summary>
        /// Se llama una vez cuando se obtiene un GameObject del pool.
        /// </summary>
        public event OnGetPoolEvent OnGetPool;

        public delegate void OnDestroyPoolElementEvent(GameObject Go);
        /// <summary>
        /// Se llama una vez cuando se "Destruye" un GameObject del pool.
        /// </summary>
        public event OnDestroyPoolElementEvent OnDestroyPoolElement;

        /// <summary>
        /// Obtiene un Game Object Manejado por el Pooling
        /// </summary>
        /// <param name="Position">Posicion</param>
        /// <param name="Rotation">Rotacion</param>
        /// <returns>Devuelve un Gameobject manejado por el pool si se encuentra disponible para su uso</returns>
        public GameObject GetGameObject( Vector3 Position, Quaternion Rotation)
        {
            GameObject current = null;
            if (Aviable.Count > 0)
            {
                int Ramdom = UnityEngine.Random.Range(0, Aviable.Count - 1);
                GameObject clone = Aviable[Ramdom];
                if (clone != null)
                {
                    clone.SetActive(true);
                    clone.transform.position = Position;
                    clone.transform.rotation = Rotation;
                    Aviable.Remove(Aviable[Ramdom]);
                    Unaviable.Add(clone);
                    current = clone;
                }
            }
            else
            {
                int Total = (Aviable.Count + Unaviable.Count);
                if (Total < PoolMaxItems)
                {
                    GameObject Myclone = UnityEngine.Object.Instantiate(Defaultprefab, Position, Rotation) as GameObject;
                    Unaviable.Add(Myclone);
                    current = Myclone;
                }
            }
            if (current != null && OnGetPool != null)
                OnGetPool(current);
            return current;
        }

        /// <summary>
        /// "Destruye" aparentemente un GameObject en el pool, quiere decir que lo pone en la lista de disponibles.
        /// </summary>
        /// <param name="obj">GameObject a destruir</param>
        public void KillPoolElement(GameObject obj)
        {
            if (Unaviable.Contains(obj))
            {
                Unaviable.Remove(obj);
            }
            Aviable.Add(obj);
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;
            obj.SetActive(false);

            if (OnDestroyPoolElement != null)
                OnDestroyPoolElement(obj);
        }

        public EasyPool ( int _MaxItems , GameObject _prefab )
        {
            PoolMaxItems = _MaxItems;
            Defaultprefab = _prefab;
        }
    }
}
