using System.Collections;
using UnityEngine;

public class ZigZagTileManager : Manager<ZigZagTileManager>
{
    [SerializeField]GameObject platformPrifab;

    [SerializeField]public Vector3 lastPos;
    [SerializeField]float size = 1f;

    [SerializeField] bool isFirstTime = true;
    [SerializeField] int forwardSizeLength = 8;
    [SerializeField] public bool stopORstart = false;
    [SerializeField] private ObjPool objPool;

    public int allPlatforms, livePlatforms;
    public int spawnedPlatforms;
    public int currentPlatform;

    private void Start()
    {
        lastPos = this.transform.position;

        //InvokeRepeating("ControllTileSpawn", 1f, .2f);
    }
    private void Update()
    {
        
        if (stopORstart == true)
        {
            while (spawnedPlatforms < allPlatforms)
            {
                ControllTileSpawn();
                if (allPlatforms == spawnedPlatforms) { stopORstart = false; return; }
               
            }
        }
        if (currentPlatform == livePlatforms - 5)
        {
            //currentPlatform = currentPlatform == allPlatforms ? currentPlatform - (allPlatforms + ((livePlatforms / 2))) : currentPlatform;
           // currentPlatform =- allPlatforms;
            spawnedPlatforms = 0;
            stopORstart = true;
        }
    }
    void ControllTileSpawn()
    {
        
       if (isFirstTime) doForward(forwardSizeLength);
       SpawnPlatforms();
    }
    private void doForward(int index)
    {

        for (int x = 0; x < index; x++) SpawnForward();

        isFirstTime = false;
    }
    private void SpawnPlatforms()
    {
        
        int rand = UnityEngine.Random.Range(0, 6);

        if (rand < 3) SpawnRight();

        else if (rand >= 3) SpawnForward();
    }
    private void SpawnRight()
    {
        //platformPrifab = ObjecPool.Instance.SpawnFromPool("pla", lastPos + new Vector3(size, 0, 0), this.transform.rotation);
        platformPrifab = objPool.GetAvailablePrifabFromDictionary("Left");
        platformPrifab.SetActive(true);
        platformPrifab.transform.position = lastPos + new Vector3(size, 0, 0);
        lastPos = platformPrifab.transform.position;
        spawnedPlatforms++;
        livePlatforms++;
    }
    private void SpawnForward()
    {
        //platformPrifab = ObjecPool.Instance.SpawnFromPool("pla", lastPos + new Vector3(0, 0, size), this.transform.rotation);
        platformPrifab = objPool.GetAvailablePrifabFromDictionary("Forward");
        platformPrifab.SetActive(true);
        size = platformPrifab.transform.localScale.x;
        platformPrifab.transform.position = lastPos + new Vector3(0, 0, size);
        lastPos = platformPrifab.transform.position;
        spawnedPlatforms++;
        livePlatforms++;
    }
    public void ResetPlatforms()
    {
        stopORstart = false;
        lastPos = this.transform.position;
        isFirstTime = true;
        currentPlatform = 0;
        spawnedPlatforms = 0;
        livePlatforms = 0;
        stopORstart = true;
    }
    
}
