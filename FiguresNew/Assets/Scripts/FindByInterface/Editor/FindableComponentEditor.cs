using System.Linq;
using System.Reflection;
using Simulation;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FindByInterface.Editor
{
    [CustomEditor(typeof(FindableComponents))]
    public class FindableComponentEditor : UnityEditor.Editor
    {
        private bool _justUseAllComponentsFromGameObject;

        public override void OnInspectorGUI()
        {
            UnityEditor.EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Just Use All Components From GameObject");
            _justUseAllComponentsFromGameObject =
                EditorGUILayout.Toggle(_justUseAllComponentsFromGameObject);
            UnityEditor.EditorGUILayout.EndHorizontal();
            if (_justUseAllComponentsFromGameObject == false)
                base.OnInspectorGUI();
            else
            {
                var targetAsFindableComponents = target as FindableComponents;
                var type = targetAsFindableComponents.GetType();
                var field = type.GetField("_componentsToAddToListOfFoundableComponents", BindingFlags.Default | BindingFlags.NonPublic | BindingFlags.Instance);
                field.SetValue(targetAsFindableComponents, targetAsFindableComponents.transform.GetComponents<Component>());
            }
            if (GUI.changed == false) return;
            EditorUtility.SetDirty(target);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            
        }
    }
}