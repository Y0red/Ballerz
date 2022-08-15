using UnityEngine;

public class CubeScriopt : MonoBehaviour,  IFace 
{
    // Start is called before the first frame update

    float upforce = .1f;
    float sidforce = .3f;


    public void onObjectSpawn()
    {
        //float xForce = Random.Range(-sidforce, sidforce);
       // float yForce = Random.Range(upforce / 1f, upforce);
        //float zForce = Random.Range(-sidforce, sidforce);

       // Vector3 _velocity = new Vector3 (xForce, yForce, zForce);

        GetComponent<Rigidbody>().isKinematic = true;
    }

  
}
