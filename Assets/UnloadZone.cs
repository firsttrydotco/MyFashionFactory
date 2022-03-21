using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UnloadZone : MonoBehaviour
{
    Inventory inv;
    GameManager gm;
    [SerializeField] private float delay;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject moneyPile;
    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private GameObject ATMStartPoint;
    [SerializeField] private GameObject ATM;

    public MMFeedback scaleATM;
    public MMFeedback scaleTruck;
    public MMFeedback truckParticles;


    bool loop = true;
    [SerializeField] bool canUnload = true;

    [Header("Lvls manager")]
    [SerializeField] private bool currentObjectiveCompleted;
    [SerializeField] private int ojectiveIndex;
    [SerializeField] private int amountCollected;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text amount;
    [SerializeField] private Slider slider;
    [SerializeField] private Animation truckLeaves;
    [SerializeField] private GameObject billsFX;
    [SerializeField] private GameObject starsFX;
    [SerializeField] private GameObject emojisFX;
    [SerializeField] private float moneyMultiplier;








    [Header("Objectives")]
    [SerializeField] public List<Objective> objList = new List<Objective>();

    [Header("Resources")]
    [SerializeField] private Sprite cloth;
    [SerializeField] private Sprite shirt;





    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("inventory").GetComponent<Inventory>();
        gm = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<GameManager>();
        currentObjectiveCompleted = false;
        ojectiveIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLvlDisplay();
        if (currentObjectiveCompleted)
        {
            StartCoroutine(objectiveCompleted());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("player") && loop == true && GameObject.FindGameObjectWithTag("player").GetComponent<CharaController>().isCarrying)
        {
            StartCoroutine(Remove());
            loop = false;
        }
    }

    private IEnumerator Remove()
    {
        if (canUnload) { 
            amountCollected++;
            inv.RemoveItem(target);
            scaleTruck.Play(scaleTruck.transform.position);
            truckParticles.Play(truckParticles.transform.position);
            yield return new WaitForSeconds(delay);
            loop = true;
        }
    }

    private IEnumerator ATMWorks(int amount)
    {
        billsFX.SetActive(true);
        int i = 0;
        while(i < amount * moneyMultiplier)
        {
            GameObject item = Instantiate(moneyPrefab, ATMStartPoint.transform.position, moneyPrefab.transform.rotation) ;
            item.transform.DOMove(moneyPile.transform.position, 0.1f);
            yield return new WaitForSeconds(0.1f);
            item.transform.parent = moneyPile.transform;
            moneyPile.GetComponent<LayoutGroup3D>().RebuildLayout();
            i++;

        }
    }

    private void UpdateLvlDisplay()
    {
        if (amountCollected == objList[ojectiveIndex].amount)
        {
            currentObjectiveCompleted = true;
            StartCoroutine(ATMWorks(amountCollected));
            amountCollected = 0;
        }

        slider.maxValue = objList[ojectiveIndex].amount;
        slider.value = amountCollected;

        icon.sprite = objList[ojectiveIndex].resource;
        amount.text = (objList[ojectiveIndex].amount - amountCollected).ToString();

    }

    private IEnumerator objectiveCompleted()
    {
        canUnload = false;
        currentObjectiveCompleted = false;
        truckLeaves.Play();
        yield return new WaitForSeconds(2f);
        amountCollected = 0;
        ojectiveIndex++;
        canUnload = true;
        loop = true;
    }
}

