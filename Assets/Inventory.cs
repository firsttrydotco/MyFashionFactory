using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Inventory : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject cloth;
    

    [Header("Infos")]
    [SerializeField] private int itemAmount;
    [SerializeField] private int maxItems;
    [SerializeField] private float yOffset;
    [SerializeField] private float xOffset;
    [SerializeField] private float maxHeight;
    [SerializeField] public static string firstItemInInv;
    [SerializeField] Vector3 offset;

    void Start()
    {
        offset = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (itemAmount > 0)
        {
            GameObject.FindGameObjectWithTag("player").GetComponent<CharaController>().isCarrying = true;
        } else { GameObject.FindGameObjectWithTag("player").GetComponent<CharaController>().isCarrying = false; }

        firstItemInInv = this.transform.GetChild(transform.childCount -1).gameObject.tag;
        print(firstItemInInv);
    }

    public void AddItem(GameObject item)
    {
        if(itemAmount < maxItems)
        {
            item.transform.parent = gameObject.transform;
            item.transform.rotation = gameObject.transform.rotation;
            transform.GetComponent<LayoutGroup3D>().RebuildLayout();
            itemAmount++;
        }
    }

    public void RemoveItem(GameObject target)
    {
        if (gameObject.transform.childCount > 0)
        {
            StartCoroutine(Fly(target));
            transform.GetComponent<LayoutGroup3D>().RebuildLayout();
            itemAmount--;
        }
    }

    public IEnumerator Fly(GameObject target)
    {
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.transform.DOMove(target.transform.position, 0.2f);
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.transform.parent = target.transform;
        yield return null;
    }

   
}
