using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ObjecPool : Manager<ObjecPool>
{
    private Dictionary<string , Queue<GameObject>> _PoolDictionery;

    [SerializeField] List<_Pool> _pools;

    public bool isInitialized = false;

    void Start()
    {
        Addressables.InitializeAsync().Completed += AddressablesManager_Done;

    }
    private void AddressablesManager_Done(AsyncOperationHandle<IResourceLocator> obj)
    {
        StartCoroutine(InitializePoolSystem());
        isInitialized = true;
    }

    private IEnumerator InitializePoolSystem()
    {
        _PoolDictionery = new Dictionary<string, Queue<GameObject>>();

        foreach (_Pool pool in _pools)
        {
            GameObject objParent = new GameObject(pool.tag);
            objParent.transform.SetParent(this.transform);
            Queue<GameObject> objPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                Addressables.InstantiateAsync(pool.prifab, objParent.transform).Completed += (c) =>
                {
                    GameObject obj = c.Result.gameObject;

                    obj.SetActive(false);
                    objPool.Enqueue(obj);
                };
            }
            _PoolDictionery.Add(pool.tag, objPool);
        }

        yield return null;
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation=default)
    {
        if (!isInitialized) return null;

        if (!_PoolDictionery.ContainsKey(tag))
        {
            Debug.LogWarning("plool with tag " + tag + "not found");

            return null;
        }
       // if (_PoolDictionery[tag].TryPeek(out GameObject res))
        {
          //  if(res.activeInHierarchy == false)
            {
                GameObject objectTospawn = _PoolDictionery[tag].Dequeue();


                objectTospawn.SetActive(true);
                objectTospawn.transform.position = position;
                objectTospawn.transform.rotation = transform.rotation ;

                IFace iface = objectTospawn.GetComponent<IFace>();

                if (iface != null)
                {
                    iface.onObjectSpawn();
                }

                _PoolDictionery[tag].Enqueue(objectTospawn);

                return objectTospawn;
            }
            //else
            //{
              //  Debug.Log("no deactivated object");
           // }
        }
     
      // return null;
        
    }


}
