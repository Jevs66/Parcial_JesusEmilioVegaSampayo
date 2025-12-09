#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(OleadasManager))]
public class OleadasManagerCI : Editor
{
    OleadasManager gestor;

    void OnEnable()
    {
        gestor = (OleadasManager)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Configuración de Oleadas", EditorStyles.boldLabel);

        SerializedProperty listaOleadasProp = serializedObject.FindProperty("listaOleadas");
        SerializedProperty spawnPointProp = serializedObject.FindProperty("spawnPoint");

        EditorGUILayout.PropertyField(listaOleadasProp, true);
        EditorGUILayout.PropertyField(spawnPointProp);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Inicio de Oleadas", EditorStyles.boldLabel);

        gestor.tipoInicio = (TipoInicioOleadas)EditorGUILayout.EnumPopup("Tipo de inicio", gestor.tipoInicio);

        switch (gestor.tipoInicio)
        {
            case TipoInicioOleadas.Automatico:
                gestor.retardoInicio = EditorGUILayout.FloatField("Retardo inicial", gestor.retardoInicio);
                break;

            case TipoInicioOleadas.Manual:
                EditorGUILayout.HelpBox(
                    "En modo Manual las oleadas no se inician en Start(). " +
                    "Usa el botón de abajo durante Play para iniciarlas.",
                    MessageType.Info);
                break;
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Herramientas", EditorStyles.boldLabel);

        if (Application.isPlaying)
        {
            if (GUILayout.Button("Iniciar oleadas ahora"))
            {
                gestor.IniciarOleadasDesdeCodigo();
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Entra a Play Mode para usar el botón de iniciar oleadas.", MessageType.None);
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Estado", EditorStyles.boldLabel);
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.EnumPopup("Estado del juego", gestor.estadoJuego);
        EditorGUILayout.IntField("Índice oleada actual", gestor.indiceOleadaActual);
        EditorGUILayout.IntField("Enemigos vivos", gestor.enemigosVivos);
        EditorGUI.EndDisabledGroup();

        serializedObject.ApplyModifiedProperties();
    }

}
#endif
