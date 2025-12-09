using UnityEngine;
using System.Collections;

public class SpawnersObjetivos : MonoBehaviour
{
    public GameObject prefabObjetivo;     
    public Transform[] puntosSpawn;     
    public float tiempoEntreSpawns = 1.5f; 
    public int maxObjetivosVivos = 5;   

    private int vivos = 0;

    void Awake()
    {
        if (tiempoEntreSpawns < 0.1f) tiempoEntreSpawns = 0.1f;
        if (maxObjetivosVivos < 1) maxObjetivosVivos = 1;

        if (prefabObjetivo == null)
            Debug.LogError("Asigna targetPrefab en el Spawner.");

        if (puntosSpawn == null || puntosSpawn.Length < 10)
            Debug.LogWarning("Se recomiendan al menos 10 puntos de spawn.");
    }

    void OnEnable()
    {
        StartCoroutine(BucleSpawn());
    }

    IEnumerator BucleSpawn()
    {
        while (true)
        {
            if (PuedeSpawnear())
            {
                SpawnUno();
            }
            yield return new WaitForSeconds(tiempoEntreSpawns);
        }
    }

    bool PuedeSpawnear()
    {
        return prefabObjetivo != null
               && puntosSpawn != null
               && puntosSpawn.Length > 0
               && vivos < maxObjetivosVivos;
    }

    void SpawnUno()
    {
        int i = Random.Range(0, puntosSpawn.Length);
        Transform p = puntosSpawn[i];

        GameObject go = Instantiate(prefabObjetivo, p.position, p.rotation);
        vivos++;

        Objetivo o = go.GetComponent<Objetivo>();
        if (o != null) o.spawner = this;
    }

    public void AvisarObjetivoMurio()
    {
        vivos--;
        if (vivos < 0) vivos = 0;
    }
}
