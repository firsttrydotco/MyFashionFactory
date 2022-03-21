using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unicornis
{
    public class Demo_FaceController : MonoBehaviour
    {
        [SerializeField] Animator faceAnimator;
        [SerializeField] float minWaitTime = 2.0f;
        [SerializeField] float maxWaitTime = 3.5f;


        private void Start()
        {
            StartCoroutine(Blink());
        }

        IEnumerator Blink()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
                faceAnimator.SetTrigger("Blink");
            }
        }
    }
}
