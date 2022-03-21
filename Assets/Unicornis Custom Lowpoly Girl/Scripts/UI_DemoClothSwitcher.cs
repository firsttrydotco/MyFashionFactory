using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unicornis
{
    public class UI_DemoClothSwitcher : MonoBehaviour
    {
        [SerializeField] Button buttonBack;
        [SerializeField] Button buttonNext;
        [SerializeField] Text labelName;
        [SerializeField] List<GameObject> objects = new List<GameObject>();

        int index = 0;

        private void Start()
        {
            buttonBack.onClick.RemoveAllListeners();
            buttonBack.onClick.AddListener(Back);

            buttonNext.onClick.RemoveAllListeners();
            buttonNext.onClick.AddListener(Next);

            foreach (var obj in objects)
                if (obj != null)
                    obj.SetActive(false);
            index = 0;

            if (objects.Count < 0) return;
            if (objects[index] != null)
            {
                objects[index].SetActive(true);
                labelName.text = objects[index].name;
            }
            else
            {
                labelName.text = "";
            }
        }

        void Next()
        {
            if (objects.Count < 1) return;

            index++;
            if (index < 0 || index >= objects.Count)
                index = 0;
            foreach (var obj in objects)
                if (obj != null)
                    obj.SetActive(false);
            if (objects[index] != null)
            {
                objects[index].SetActive(true);
                labelName.text = objects[index].name;
            }
            else
            {
                labelName.text = "";
            }
        }

        void Back()
        {
            if (objects.Count < 1) return;

            index--;
            if (index < 0 || index >= objects.Count)
                index = objects.Count - 1;
            foreach (var obj in objects)
                if (obj != null)
                    obj.SetActive(false);
            if (objects[index] != null)
            {
                objects[index].SetActive(true);
                labelName.text = objects[index].name;
            }
            else
            {
                labelName.text = "";
            }
        }
    }
}
