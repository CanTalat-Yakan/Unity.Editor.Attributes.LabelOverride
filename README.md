# Unity Essentials

This module is part of the Unity Essentials ecosystem and follows the same lightweight, editor-first approach.
Unity Essentials is a lightweight, modular set of editor utilities and helpers that streamline Unity development. It focuses on clean, dependency-free tools that work well together.

All utilities are under the `UnityEssentials` namespace.

```csharp
using UnityEssentials;
```

## Installation

Install the Unity Essentials entry package via Unity's Package Manager, then install modules from the Tools menu.

- Add the entry package (via Git URL)
    - Window → Package Manager
    - "+" → "Add package from git URL…"
    - Paste: `https://github.com/CanTalat-Yakan/UnityEssentials.git`

- Install or update Unity Essentials packages
    - Tools → Install & Update UnityEssentials
    - Install all or select individual modules; run again anytime to update

---

# Label Override Attribute

> Quick overview: Override the display label of a serialized field in the Inspector using `[LabelOverride("Your Label")]`. Works on fields; array elements are not supported.

A tiny PropertyDrawer that swaps the default nicified label with a custom string you provide. Great for making fields clearer to designers and for matching in‑game terminology.

![screenshot](Documentation/Screenshot.png)

## Features
- Override any serialized field’s label text with a custom string
- No runtime overhead; editor-only rendering
- Works with most field types (ints, floats, strings, objects, structs, etc.)
- Safe: when applied to an array element, logs a warning and falls back to the default label

## Requirements
- Unity Editor 6000.0+ (Editor-only; attribute lives in Runtime for convenience)
- Depends on the Unity Essentials Inspector Hooks module (utility used to detect array elements)

Tip: If you see warnings about missing utilities, ensure the Inspector Hooks module is installed.

## Usage
Basic

```csharp
using UnityEngine;
using UnityEssentials;

public class PlayerSettings : MonoBehaviour
{
    [LabelOverride("Player Speed (m/s)")]
    public float speed;

    [LabelOverride("Max Health")]
    public int maxHealth = 100;
}
```

Arrays and collections

```csharp
public class Inventory : MonoBehaviour
{
    // The array field’s label can be overridden…
    [LabelOverride("Items")]
    public string[] items;

    // …but array elements themselves will use default element labels.
    // Applying [LabelOverride] on individual elements is not supported.
}
```

Nested/serialized structs

```csharp
[System.Serializable]
public struct CameraRig
{
    [LabelOverride("Pivot Offset")]
    public Vector3 pivot;
}

public class CameraConfig : MonoBehaviour
{
    public CameraRig rig; // Inside, the field label shows as "Pivot Offset"
}
```

## How It Works
- The property drawer intercepts the field’s GUI and replaces `label.text` with your provided label
- If the target is an array element, it logs a warning and leaves the original label
- Finally, it draws the property normally so all standard UI, nesting, and child properties still render

## Notes and Limitations
- Array elements are not supported; only the array field itself can have its label overridden
- Drawer conflicts: Unity can only apply one PropertyDrawer per field; if another attribute also provides a drawer for the same field, only one will take effect
- Multi-object editing is supported; the label override applies uniformly
- Editor-only: This affects Inspector rendering only and does not change serialization or runtime behavior

## Files in This Package
- `Runtime/LabelOverrideAttribute.cs` – `[LabelOverride]` attribute marker with the custom label string
- `Editor/LabelOverrideDrawer.cs` – PropertyDrawer that overrides the label at draw time
- `Runtime/UnityEssentials.LabelOverrideAttribute.asmdef` – Runtime assembly definition
- `Editor/UnityEssentials.LabelOverrideAttribute.Editor.asmdef` – Editor assembly definition (references Inspector Hooks)

## Tags
unity, unity-editor, attribute, propertydrawer, label, inspector, ui, tools, workflow
