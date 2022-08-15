using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    //[SerializeField] private GameObject prifabObj;
   // [SerializeField] private int poolDepth;
    [SerializeField] bool canGrow = false;

   // [SerializeField]private List<GameObject> pools = new List<GameObject>();


    [SerializeField] private List<_Pool> _poolsX;

    [SerializeField]private Dictionary<string, List<GameObject>> _poolDictionary = new Dictionary<string, List<GameObject>>();

    GameObject objParent;
    protected  void Awake()
    {
        // for(int i = 0; i < poolDepth; i++)
        //{
        //    GameObject pooledObject = Instantiate(prifabObj);
        //    pooledObject.SetActive(false);
        //     pool.Add(pooledObject);
        //  }
        objParent = new GameObject("All_PRIFABS");
        objParent.transform.SetParent(this.transform);
        foreach (_Pool pol in _poolsX)
        {
            List<GameObject> poolsList = new List<GameObject>();

            for (int i = 0; i < pol.size; i++)
            {
                GameObject pooledPrifab = Instantiate(pol.prifab);
                pooledPrifab.transform.SetParent(objParent.transform);
                pooledPrifab.SetActive(false);
                poolsList.Add(pooledPrifab);
            }
            _poolDictionary.Add(pol.tag, poolsList);
        }
    }

   /* public GameObject GetAvailablePrifab()
    {
        for(int i = 0; i < pools.Count; i++)
        {
            if (pools[i].activeInHierarchy == false)
                return pools[i];
        }
        if (canGrow)
        {
            GameObject pooledObject = Instantiate(prifabObj);
            pooledObject.SetActive(false);
            pools.Add(pooledObject);
            return pooledObject;
        }
        else
        {
            return null;
        }
    }
   */
    public GameObject GetAvailablePrifabFromDictionary(string tag)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("plool with tag " + tag + "not found");

            return null;
        }
        for (int i = 0; i < _poolDictionary[tag].Count; i++)
        {
            if (_poolDictionary[tag][i].activeInHierarchy == false) return _poolDictionary[tag][i];
        }
        if (canGrow)
        {
                List<GameObject> poolsList = new List<GameObject>();

                GameObject pooledPrifab = Instantiate(_poolsX[0].prifab);
                pooledPrifab.transform.SetParent(objParent.transform);
                pooledPrifab.SetActive(false);
                poolsList.Add(pooledPrifab);

                _poolDictionary.Add(tag, poolsList);

                return pooledPrifab;
        }
        else
        {
            return null;
        }
        
    }
}
