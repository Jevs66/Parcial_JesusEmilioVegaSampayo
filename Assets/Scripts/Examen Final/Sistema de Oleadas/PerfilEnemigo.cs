using UnityEngine;


[CreateAssetMenu(menuName = "SistemaDeOleadas/Perfil Enemigo")]
public class PerfilEnemigo : ScriptableObject
{
    public string nombreEnemigo;
    public GameObject prefabEnemigo;
    public int vidaMaxima;
    public float velocidadMovimiento;
    public int recompensaAlMorir;
}
