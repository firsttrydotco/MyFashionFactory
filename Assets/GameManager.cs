using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Feedbacks;

public class GameManager : MonoBehaviour
{


    [Header("Money")]
    [SerializeField] public static int currentMoney;
    [SerializeField] private int startMoney;


    [Header("Infos")]
    [SerializeField] public static float inputSpeed;
    [SerializeField] private float startInputSpeed;
    [SerializeField] private GameObject inputPile;
    [SerializeField] private int maxInPile;




    public MMFeedbackFlicker blink;
    private bool loop;


    [Header("Machines")]
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject runnersContainer;

    [Header("Resources")]
    [SerializeField] private GameObject cloth;

    [Header("Display")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject runnerPanel;
    [SerializeField] public TMP_Text moneyDisplay;




    void Start()
    {
        currentMoney = startMoney;
        loop = true;
        inputSpeed = startInputSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        moneyDisplay.text = currentMoney.ToString();
        if (loop && inputPile.transform.childCount < maxInPile)
        {
            StartCoroutine(SpawnMainResource());
        }
    }

    private IEnumerator SpawnMainResource()
    {
        loop = false;
        GameObject item = Instantiate(cloth, inputPile.transform.position, Quaternion.identity);
        item.transform.parent = inputPile.transform;
        inputPile.GetComponent<LayoutGroup3D>().RebuildLayout();
        yield return new WaitForSeconds(inputSpeed);
        loop = true;
    }


    

   
    

    
}

