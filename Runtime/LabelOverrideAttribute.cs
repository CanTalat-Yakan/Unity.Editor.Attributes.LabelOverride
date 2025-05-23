using UnityEngine;

namespace UnityEssentials
{
    /// <summary>
    /// Specifies a custom label to display in the Unity Inspector for the associated property.
    /// </summary>
    /// <remarks>Use this attribute to override the default label of a property in the Unity Inspector. This
    /// can be useful for providing more descriptive or user-friendly labels for fields.</remarks>
    public class LabelOverrideAttribute : PropertyAttribute
    {
        public string Label;

        public LabelOverrideAttribute(string label) =>
            Label = label;
    }
}
