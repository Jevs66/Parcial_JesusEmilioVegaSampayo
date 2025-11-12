using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo;   
    public float tiempoInicial = 15f;    
    public float tiempoMaximo = 20f;
    public static float bonusPorObjetivo = 2f; 

    private static Timer instancia;
    private float tiempo;

    void Awake()
    {
        instancia = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tiempo = Mathf.Max(0f, tiempoInicial);
        ActualizarTexto();
    }

    // Update is called once per frame
    void Update()
    {
        if (tiempo > 0f)
        {
            tiempo -= Time.deltaTime;
            if (tiempo <= 0f)
            {
                tiempo = 0f;
                ReiniciarEscena();
            }
            ActualizarTexto();
        }
    }

    void ActualizarTexto()
    {
        if (textoTiempo != null)
        {            
            int seg = Mathf.CeilToInt(tiempo);
            textoTiempo.text = "Tiempo: " + seg;
        }
    }
   
    public static void SumarTiempo(float cantidad)
    {
        if (instancia == null) return;

        instancia.tiempo += Mathf.Max(0f, cantidad);
        if (instancia.tiempo > instancia.tiempoMaximo)
            instancia.tiempo = instancia.tiempoMaximo;

        instancia.ActualizarTexto();
    }

    void ReiniciarEscena()
    {
        Puntaje.puntos = 0;

        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}
