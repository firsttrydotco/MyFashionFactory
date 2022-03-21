using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharaController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float playerSpeed;

    [Header("Infos")]
    [SerializeField] public bool isCarrying;
    [SerializeField] private bool isStopped;



    public GameObject winUI;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public Animation truckLeaving;
    private bool winLoop;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        winLoop = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(UltimateJoystick.GetHorizontalAxis("Jstick") == 0 && UltimateJoystick.GetVerticalAxis("Jstick") == 0)
        {
            isStopped = true;
        } else { isStopped = false; }

        if(isStopped)
        {
            if (isCarrying)
            {
                animator.SetBool("CarryIdle", true);
            }
            else
            {
                animator.SetBool("Idle", true);
            }
        }
        else
        {
            animator.SetBool("CarryIdle", false);
            animator.SetBool("Idle", false);
        }
        Move();


    }

    void Move()
    {
        float h = UltimateJoystick.GetHorizontalAxis("Jstick");
        float v = UltimateJoystick.GetVerticalAxis("Jstick");
        navMeshAgent.velocity = new Vector3(h, 0, v) * playerSpeed;


        if (!isStopped)
        {
            if (isCarrying)
            {
                animator.SetBool("CarryRun", true);
            }
            else
            {
                animator.SetBool("Run", true);
            }
        }
        else
        {
            animator.SetBool("CarryRun", false);
            animator.SetBool("Run", false);
        }
    }


    private IEnumerator winScreen()
    {
        animator.SetTrigger("Win");
        truckLeaving.Play();
        yield return new WaitForSeconds(2f);
        winUI.SetActive(true);
    }
}
