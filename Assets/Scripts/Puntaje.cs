using UnityEngine;
using TMPro;

public class Puntaje : MonoBehaviour
{

    public TextMeshProUGUI textoPuntaje;
    public static int puntos = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActualizarTexto();
    }

    public static void SumarPunto()
    {
        puntos++;
        // Busca el objeto que tenga este script y actualiza el texto.
        Puntaje instancia = FindFirstObjectByType<Puntaje>();
        if (instancia != null)
        {
            instancia.ActualizarTexto();
        }
    }

    void ActualizarTexto()
    {
        if (textoPuntaje != null)
            textoPuntaje.text = "Puntaje: " + puntos;
    }
}
