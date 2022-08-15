using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    ObjecPool objPooler;

    private void Start()
    {

    }


    void FixedUpdate()
    {
        objPooler.SpawnFromPool("cube", transform.position, Quaternion.identity);
    }
}
