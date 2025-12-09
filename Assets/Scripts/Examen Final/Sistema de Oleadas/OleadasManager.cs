using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum TipoInicioOleadas
{
    Automatico,
    Manual
}

public enum EstadoJuego
{
    EsperandoOleada,
    GenerandoOleada,
    OleadaEnCurso,
    Terminado
}


public class OleadasManager : MonoBehaviour
{
    [Header("Configuración de Oleadas")]
    public List<ConfiguracionOleada> listaOleadas = new List<ConfiguracionOleada>();
    public Transform spawnPoint;

    [Header("Inicio de Oleadas")]
    public TipoInicioOleadas tipoInicio = TipoInicioOleadas.Automatico;

    [Tooltip("Segundos antes de iniciar la primera oleada (solo en modo Automático).")]
    public float retardoInicio = 1f;

    [Header("Estado del Juego")]
    public EstadoJuego estadoJuego;

    [Header("Debug")]
    public int indiceOleadaActual = 0;
    public int enemigosVivos = 0;

    bool corutinaActiva = false;

    void Start()
    {
        if (tipoInicio == TipoInicioOleadas.Automatico)
        {
            IniciarOleadasDesdeCodigo();
        }
    }

    public void IniciarOleadasDesdeCodigo()
    {
        if (corutinaActiva) return;
        if (listaOleadas == null || listaOleadas.Count == 0)
        {
            Debug.LogWarning("No hay oleadas en OleadasManager.");
            return;
        }

        StopAllCoroutines();
        StartCoroutine(IniciarConRetardo());
    }

    IEnumerator IniciarConRetardo()
    {
        corutinaActiva = true;

        if (tipoInicio == TipoInicioOleadas.Automatico && retardoInicio > 0f)
        {
            yield return new WaitForSeconds(retardoInicio);
        }

        yield return StartCoroutine(EjecutarOleadas());

        corutinaActiva = false;
    }

    IEnumerator EjecutarOleadas()
    {
        estadoJuego = EstadoJuego.EsperandoOleada;
        indiceOleadaActual = 0;

        while (indiceOleadaActual < listaOleadas.Count)
        {
            ConfiguracionOleada oleadaActual = listaOleadas[indiceOleadaActual];

            estadoJuego = EstadoJuego.GenerandoOleada;

            yield return StartCoroutine(GenerarOleada(oleadaActual));

            estadoJuego = EstadoJuego.OleadaEnCurso;

            while (enemigosVivos > 0)
            {
                yield return null;
            }

            indiceOleadaActual++;

            yield return new WaitForSeconds(oleadaActual.tiempoAntesSiguienteOleada);

            estadoJuego = EstadoJuego.EsperandoOleada;
        }

        estadoJuego = EstadoJuego.Terminado;
        Debug.Log("Todas las oleadas han terminado.");
    }

    IEnumerator GenerarOleada(ConfiguracionOleada oleada)
    {
        for (int i = 0; i < oleada.listaDeEnemigos.Count; i++)
        {
            EntradaOleada entrada = oleada.listaDeEnemigos[i];

            for (int j = 0; j < entrada.cantidad; j++)
            {
                GenerarEnemigo(entrada.perfilEnemigo);

                yield return new WaitForSeconds(entrada.intervaloEntreEnemigos);
            }
        }
    }

    void GenerarEnemigo(PerfilEnemigo perfil)
    {
        if (perfil == null || perfil.prefabEnemigo == null)
        {
            Debug.LogWarning("Perfil de enemigo vacío en OleadasManager.");
            return;
        }

        GameObject nuevoEnemigo =
            Instantiate(perfil.prefabEnemigo, spawnPoint.position, spawnPoint.rotation);

        EnemigoBase enemigoBase = nuevoEnemigo.GetComponent<EnemigoBase>();

        if (enemigoBase != null)
        {
            enemigoBase.Configurar(perfil, this);
        }

        enemigosVivos++;
    }

    public void NotificarMuerteEnemigo()
    {
        enemigosVivos--;
        if (enemigosVivos < 0) enemigosVivos = 0;
    }
}
