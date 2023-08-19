using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GlideGame.Utils;

namespace GlideGame.Editors
{
    [CustomEditor(typeof(RandomObjectPlacement))]
    public class RandomObjectPlacementEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            RandomObjectPlacement placementScript = (RandomObjectPlacement)target;

            if (GUILayout.Button("Place Random Objects"))
            {
                placementScript.PlaceRandomObjects();
            }
            if (GUILayout.Button("Delete Objects"))
            {
                placementScript.DeleteObjects();
            }
        }
    }
}
