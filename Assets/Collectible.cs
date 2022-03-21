using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectible : MonoBehaviour
{
    Inventory inv;
    public GameObject dollarCollectPoint;
    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("inventory").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (this.gameObject.CompareTag("money") && other.gameObject.CompareTag("player"))
        {
            GameManager.currentMoney++;
            gameObject.transform.DOMove(GameObject.Find("dollarCollectPoint").transform.position, 0.3f).OnComplete(() => Destroy(gameObject)); 
            //Destroy(gameObject);

        } else if (other.gameObject.CompareTag("player"))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            inv.AddItem(gameObject);
        }
    }
}
