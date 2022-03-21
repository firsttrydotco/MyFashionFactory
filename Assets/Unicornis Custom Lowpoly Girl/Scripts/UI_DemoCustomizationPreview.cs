using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unicornis
{
    public class UI_DemoCustomizationPreview : MonoBehaviour
    {
        [SerializeField] Transform prefabTransform;
        [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
        [SerializeField] Slider footFormSlider;


        private void Start()
        {
            footFormSlider.onValueChanged.RemoveAllListeners();
            footFormSlider.onValueChanged.AddListener(OnSliderValueChanged);
            footFormSlider.value = 0.0f;
        }

        private void Update()
        {
            prefabTransform.RotateAround(prefabTransform.position, Vector3.up, Time.deltaTime * 50.0f);
        }

        void OnSliderValueChanged(float new_value)
        {
            int indexBS = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("FootForm1");
            skinnedMeshRenderer.SetBlendShapeWeight(indexBS, new_value * 100.0f);
        }
    }
}
