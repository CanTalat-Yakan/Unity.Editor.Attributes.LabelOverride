using UnityEngine;

namespace UnityEssentials
{
    public class LabelOverrideAttribute : PropertyAttribute
    {
        public string Label;

        public LabelOverrideAttribute(string label) =>
            Label = label;
    }
}
