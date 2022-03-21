using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPanelsOnCam : MonoBehaviour
{
    [SerializeField] private GameObject target;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);
    }
}
