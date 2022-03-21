using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyRunners : MonoBehaviour
{
    Inventory inv;
    GameManager gm;
    [SerializeField] private int price;
    [SerializeField] private int paid;
    [SerializeField] private GameObject machine;
    [SerializeField] private TMP_Text priceDisplay;
    [SerializeField] private Slider slider;


    [SerializeField] private float delay;
    [SerializeField] private GameObject target;



    bool loop = true;

    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("inventory").GetComponent<Inventory>();
        gm = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<GameManager>();
        slider.maxValue = price;
    }

    void Update()
    {
        slider.value = paid;
        priceDisplay.text = (price - paid).ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("player") && loop == true)
        {
            StartCoroutine(Buy());
            loop = false;
        }
    }


    private IEnumerator Buy()
    {
        if (GameManager.currentMoney > 0)
        {
            if (paid == price)
            {
                machine.SetActive(true);
                this.gameObject.SetActive(false);
                GameManager.inputSpeed -= GameManager.inputSpeed * 0.3f;
            }
            else
            {
                paid++;
                GameManager.currentMoney--;
                yield return new WaitForSeconds(delay);
                loop = true;
            }
        }

    }
}
