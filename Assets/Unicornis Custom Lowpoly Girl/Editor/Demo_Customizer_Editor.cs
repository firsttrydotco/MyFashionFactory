using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Unicornis
{
    [CustomEditor(typeof(Demo_Customizer))]
    public class Demo_Customizer_Editor : Editor
    {
        Demo_Customizer customizer;

        public override void OnInspectorGUI()
        {
            customizer = target as Demo_Customizer;

            base.OnInspectorGUI();

            var styleActive = new GUIStyle(GUI.skin.button);
            styleActive.normal.textColor = Color.green;
            styleActive.fontStyle = FontStyle.Bold;
            styleActive.normal.background = Texture2D.grayTexture;

            var style = new GUIStyle(GUI.skin.button);
            var defaultStyle = style;

            if (customizer != null)
            {
                if (customizer.clothTemplate && GUILayout.Button("Copy Cloth Settings", style))
                {
                    customizer.CopySettingsFromTemplate();
                    EditorUtility.SetDirty(customizer.gameObject);
                }

                if (customizer.boundsTemplate != null && GUILayout.Button("Configure Bounds", style))
                {
                    customizer.ConfigureBounds();
                    EditorUtility.SetDirty(customizer.gameObject);
                }
            }
        }
    }
}