using UnityEngine;

public class Arma : MonoBehaviour
{
    public Camera cam;

    void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }
    }

    void Disparar()
    {
        if (cam == null) return;

        Ray rayo = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(rayo, out hit, 1000f))
        {
            Objetivo o = hit.collider.GetComponent<Objetivo>();
            if (o != null)
            {
                o.Hit(); 
            }
        }
    }
}
