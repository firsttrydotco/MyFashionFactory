using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class UnloadForTransform : MonoBehaviour
{
    Inventory inv;
    [SerializeField] private float delay;
    [SerializeField] bool loop = true;
    [SerializeField] bool transforming = false;


    [SerializeField] private int itemsInBuffer;
    [SerializeField] private GameObject teePrefab;
    [SerializeField] private GameObject clothPrefab;
    [SerializeField] private GameObject spawnPosition;
    [SerializeField] private float transformationDuration;
    [SerializeField] private GameObject bufferContainer;
    [SerializeField] private GameObject smokeFX;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject pile;
    [SerializeField] private GameObject yield;

    [SerializeField] private int bufferMax;

    public MMFeedback scaleTransform;










    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("inventory").GetComponent<Inventory>();
        itemsInBuffer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (itemsInBuffer == 0)
        {
            smokeFX.SetActive(false);
        } else { smokeFX.SetActive(true); }

        if (!transforming && itemsInBuffer>0)
        {
            transforming = true;
            StartCoroutine(TransformToShirt());
        }
       

       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("player") && loop && GameObject.FindGameObjectWithTag("player").GetComponent<CharaController>().isCarrying)
        {
            loop = false;
            StartCoroutine(Remove());
        }
    }

    private IEnumerator Remove()
    {
        if(itemsInBuffer < bufferMax && Inventory.firstItemInInv == "cloth")
        {
            loop = true;
            inv.RemoveItem(target);
            FillBuffer();
            itemsInBuffer++;

            //scaleTransform.Play(scaleTransform.transform.position);
            yield return new WaitForSeconds(delay);
        }
        loop = true;
    }

    private IEnumerator TransformToShirt()
    {
        if (itemsInBuffer > 0)
        {
            Destroy(pile.transform.GetChild(pile.transform.childCount - 1).gameObject);
            pile.transform.GetComponent<LayoutGroup3D>().RebuildLayout();

            yield return new WaitForSeconds(transformationDuration);
            GameObject item = Instantiate(teePrefab, yield.transform.position, Quaternion.identity);
            item.transform.parent = yield.transform;
            yield.transform.GetComponent<LayoutGroup3D>().RebuildLayout();
            itemsInBuffer--;
        }
        yield return null;
        transforming = false;
    }

    private void FillBuffer()
    {
        print("ok");
        GameObject item = Instantiate(clothPrefab, pile.transform.position, Quaternion.identity);
        Destroy(item.GetComponent<Rigidbody>());
        item.transform.parent = pile.transform;
        pile.transform.GetComponent<LayoutGroup3D>().RebuildLayout();
    }

    

}