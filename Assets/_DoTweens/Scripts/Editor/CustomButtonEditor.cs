using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace _DoTweens.Scripts.Editor
{
    [CustomEditor(typeof(CustomButton))]
    public class CustomButtonEditor : ButtonEditor
    {
        private SerializedProperty _interactableProperty;

        protected override void OnEnable()
        {
            _interactableProperty = serializedObject.FindProperty("m_Interactable");
        }

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            var changeButtonType = new PropertyField(serializedObject.FindProperty(CustomButton.ChangeButtonType));
            var curveEase = new PropertyField(serializedObject.FindProperty(CustomButton.CurveEase));
            var duration = new PropertyField(serializedObject.FindProperty(CustomButton.Duration));
            var gameState = new PropertyField(serializedObject.FindProperty(CustomButton.GameState));

            var settingLabel = new Label("Setting Button");

            root.Add(settingLabel);
            root.Add(changeButtonType);
            root.Add(curveEase);
            root.Add(duration);
            root.Add(gameState);

            root.Add(new IMGUIContainer(OnInspectorGUI));

            return root;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_interactableProperty);
            EditorGUI.BeginChangeCheck();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
