using UnityEngine;
using System.Collections;

public class Objetivo : MonoBehaviour
{
    public float vidaMin = 1.0f; 
    public float vidaMax = 3.0f;  

    [HideInInspector] public SpawnersObjetivos spawner; 
    private Coroutine rutina;

    void OnEnable()
    {
        if (vidaMax < vidaMin) vidaMax = vidaMin;
        float vida = Random.Range(vidaMin, vidaMax);
        rutina = StartCoroutine(Autodestruir(vida));
    }

    IEnumerator Autodestruir(float t)
    {
        yield return new WaitForSeconds(t);
        Desaparecer();
    }

    public void Hit()
    {
        Puntaje.SumarPunto();
        Timer.SumarTiempo(Timer.bonusPorObjetivo);
        Desaparecer();
    }

    void Desaparecer()
    {
        if (rutina != null)
        {
            StopCoroutine(rutina);
            rutina = null;
        }
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.AvisarObjetivoMurio();
        }
    }
}
