using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, IFace
{
    [SerializeField] Rigidbody rb;

    private void OnEnable()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        rb.isKinematic = true;
    }
    public void onObjectSpawn()
    {
        rb.isKinematic = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ZigZagTileManager.Instance.currentPlatform++;

            rb.isKinematic = false;

            StartCoroutine(WaitAndDessable());
        }
    }
    void LateUpdate()
    {
        if (GameManager.Instance.currentGameState == GameManager.GameState.GAMEOVER && gameObject.activeInHierarchy)
        {
            StartCoroutine(WaitAndDessable());
        }
    }
    private IEnumerator WaitAndDessable()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
