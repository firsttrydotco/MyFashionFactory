using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unicornis
{
    public class Demo_Customizer : MonoBehaviour
    {
        public GameObject clothTemplate;
        public SkinnedMeshRenderer boundsTemplate;


        public void CopySettingsFromTemplate()
        {
            var list_other = clothTemplate.GetComponentsInChildren<SkinnedMeshRenderer>();
            var list_own = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

            int i, j, bs;

            for (i = 0; i < list_own.Length; i++)
            {
                bool isFind = false;
                for (j = 0; j < list_other.Length; j++)
                    if (list_own[i].name == list_other[j].name)
                    {
                        list_own[i].sharedMaterials = list_other[j].sharedMaterials;
                        for (bs = 0; bs < list_own[i].sharedMesh.blendShapeCount; bs++)
                        {
                            list_own[i].SetBlendShapeWeight(bs, list_other[j].GetBlendShapeWeight(bs));
                        }

                        isFind = true;
                        break;
                    }
                if (!isFind)
                    list_own[i].gameObject.SetActive(false);
            }
        }

        public void ConfigureBounds()
        {
            var list_own = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
            int i, j, bs;

            for (i = 0; i < list_own.Length; i++)
            {
                if (list_own[i] != boundsTemplate)
                {
                    //var bounds = list_own[i].localBounds;
                    //bounds.center = boundsTemplate.localBounds.center;
                    //bounds.extents = boundsTemplate.localBounds;
                    list_own[i].localBounds = boundsTemplate.localBounds;
                }
            }
        }
    }
}
