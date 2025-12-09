using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;


[CustomEditor(typeof(ConfiguracionOleada))]
public class ConfiguracionOlaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();       
    }
}
#endif
