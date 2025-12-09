using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class EntradaOleada
{
    public PerfilEnemigo perfilEnemigo;  
    public int cantidad;                 
    public float intervaloEntreEnemigos; 
}

[CreateAssetMenu(menuName = "SistemaDeOleadas/Configuracion Oleada")]
public class ConfiguracionOleada : ScriptableObject
{
    public string nombreOleada;
    public List<EntradaOleada> listaDeEnemigos = new List<EntradaOleada>();

    [Tooltip("Tiempo de espera antes de que empiece la siguiente oleada")]
    public float tiempoAntesSiguienteOleada = 3f;
}
