using UnityEngine;


public enum EstadoEnemigo
{
    Quieto,
    Caminando,
    Atacando,
    Muerto
}

public class EnemigoBase : MonoBehaviour, IDaniable
{
    [Header("Referencia de datos")]
    public PerfilEnemigo perfilEnemigo;

    [Header("Componentes 3D")]
    public Rigidbody rigidbodyEnemigo;
    public Collider colliderEnemigo;
    public Renderer rendererEnemigo;

    [Header("Estado actual (solo lectura)")]
    public EstadoEnemigo estadoEnemigo;

    protected int vidaActual;
    protected float velocidadMovimiento;
    protected OleadasManager oleadasManager;

    public void Configurar(PerfilEnemigo nuevoPerfil, OleadasManager nuevoGestor)
    {
        perfilEnemigo = nuevoPerfil;
        oleadasManager = nuevoGestor;

        vidaActual = perfilEnemigo.vidaMaxima;
        velocidadMovimiento = perfilEnemigo.velocidadMovimiento;

        estadoEnemigo = EstadoEnemigo.Caminando;
    }

    void Update()
    {
        if (estadoEnemigo == EstadoEnemigo.Caminando)
        {
            transform.Translate(Vector3.forward * velocidadMovimiento * Time.deltaTime);
        }
    }

    void OnMouseDown()
    {
        RecibirDanio(vidaActual);
    }
  
    public virtual void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0 && estadoEnemigo != EstadoEnemigo.Muerto)
        {
            Morir();
        }
    }

    protected virtual void Morir()
    {
        estadoEnemigo = EstadoEnemigo.Muerto;

        if (oleadasManager != null)
        {
            oleadasManager.NotificarMuerteEnemigo();
        }

        Destroy(gameObject);
    }
}
