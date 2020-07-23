using UnityEditor;

namespace TFG.Dialog.Editor {

    [CustomEditor(typeof(DialogueManager))]
    [CanEditMultipleObjects]
    public class DialogueManagerEditor : UnityEditor.Editor {

        // DialogueManager Properties
        SerializedProperty _file;
        SerializedProperty _load;
        SerializedProperty _begin;
        SerializedProperty _useTmp;
        SerializedProperty _uiText;
        SerializedProperty _tmp;
        SerializedProperty _p1;
        SerializedProperty _p2;
        SerializedProperty _onload;
        SerializedProperty _start;
        SerializedProperty _next;
        SerializedProperty _finish;

        void OnEnable() {
            _file = serializedObject.FindProperty("rawFile");
            _load = serializedObject.FindProperty("loadOnStart");
            _begin = serializedObject.FindProperty("beginOnLoad");
            _useTmp = serializedObject.FindProperty("useTMP");
            _uiText = serializedObject.FindProperty("text");
            _tmp = serializedObject.FindProperty("tmp");
            _p1 = serializedObject.FindProperty("P1");
            _p2 = serializedObject.FindProperty("P2");
            _onload = serializedObject.FindProperty("onLoad");
            _start = serializedObject.FindProperty("onStart");
            _next = serializedObject.FindProperty("onNextDialog");
            _finish = serializedObject.FindProperty("onFinish");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_file);
            EditorGUILayout.PropertyField(_load);
            EditorGUILayout.PropertyField(_begin);
            EditorGUILayout.PropertyField(_useTmp);

            if (_useTmp.boolValue) {
                EditorGUILayout.PropertyField(_tmp);
            } else {
                EditorGUILayout.PropertyField(_uiText);
            }

            EditorGUILayout.PropertyField(_p1);
            EditorGUILayout.PropertyField(_p2);
            EditorGUILayout.PropertyField(_onload);
            EditorGUILayout.PropertyField(_start);
            EditorGUILayout.PropertyField(_next);
            EditorGUILayout.PropertyField(_finish);
            serializedObject.ApplyModifiedProperties();
        }
    }
}