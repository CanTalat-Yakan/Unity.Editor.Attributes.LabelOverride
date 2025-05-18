#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace UnityEssentials
{
    [CustomPropertyDrawer(typeof(LabelOverrideAttribute))]
    public class LabelOverrideDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            try
            {
                var propertyAttribute = this.attribute as LabelOverrideAttribute;
                if (!IsPropertyAnArrayElement(property))
                    label.text = propertyAttribute.Label;
                else
                {
                    Debug.LogWarningFormat(
                        "{0}(\"{1}\") doesn't support arrays ",
                        typeof(LabelOverrideAttribute).Name,
                        propertyAttribute.Label);
                }
                EditorGUI.PropertyField(position, property, label);
            }
            catch (System.Exception e) { Debug.LogException(e); }
        }

        private bool IsPropertyAnArrayElement(SerializedProperty property)
        {
            string path = property.propertyPath;

            int dotIndex = path.IndexOf('.');
            if (dotIndex == -1)
                return false;

            string propertyName = path.Substring(0, dotIndex);
            return property.serializedObject.FindProperty(propertyName).isArray;
        }
    }
}
#endif