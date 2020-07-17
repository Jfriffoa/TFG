using UnityEngine;
using UnityEditor;

namespace TFG.Editor {

    [CustomEditor(typeof(SceneChanger))]
    public class SceneChangerEditor : UnityEditor.Editor {

        // Scene Changer Properties
        SerializedProperty _multiple;
        SerializedProperty _scene;
        SerializedProperty _scenes;

        void OnEnable() {
            _multiple = serializedObject.FindProperty("multiplesScenes");
            _scene = serializedObject.FindProperty("sceneName");
            _scenes = serializedObject.FindProperty("scenes");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_multiple);

            if (_multiple.boolValue) {
                EditorGUILayout.PropertyField(_scenes);
            } else {
                EditorGUILayout.PropertyField(_scene);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}