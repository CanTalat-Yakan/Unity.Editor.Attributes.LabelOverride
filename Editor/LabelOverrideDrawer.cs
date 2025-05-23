#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace UnityEssentials
{
    /// <summary>
    /// Provides a custom property drawer for fields decorated with the <see cref="LabelOverrideAttribute"/>.
    /// </summary>
    /// <remarks>This drawer replaces the default label of a serialized property with the label specified in
    /// the <see cref="LabelOverrideAttribute"/>. It does not support array elements and will log a warning if applied
    /// to a property that is part of an array.</remarks>
    [CustomPropertyDrawer(typeof(LabelOverrideAttribute))]
    public class LabelOverrideDrawer : PropertyDrawer
    {
        /// <summary>
        /// Draws the custom GUI for a serialized property in the Unity Inspector, applying a label override if
        /// specified.
        /// </summary>
        /// <remarks>If the property is not part of an array, the label is overridden with the value
        /// specified in the <see cref="LabelOverrideAttribute"/>. If the property is part of an array, a warning is
        /// logged, as label overrides are not supported for array elements.</remarks>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            try
            {
                var propertyAttribute = this.attribute as LabelOverrideAttribute;

                if (!InspectorHookUtilities.IsArrayElement(property))
                    label.text = propertyAttribute.Label;
                else Debug.LogWarningFormat(
                    "{0}(\"{1}\") doesn't support arrays ",
                    typeof(LabelOverrideAttribute).Name,
                    propertyAttribute.Label);

                EditorGUI.PropertyField(position, property, label);
            }
            catch (System.Exception ex) { Debug.LogException(ex); }
        }
    }
}
#endif