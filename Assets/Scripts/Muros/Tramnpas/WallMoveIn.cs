using UnityEngine;

public class WallMoveIn : MonoBehaviour
{
    public Transform centerPoint;
    public float speed = 3f;

    private bool closing = false;

    void Update()
    {
        if (!closing) return;

        if (centerPoint == null) return;

        // 1. Convertir la posición del centro a coordenadas LOCALES del muro
        Vector3 centroLocal = transform.InverseTransformPoint(centerPoint.position);

        // 2. Determinar si el centro está a la derecha o izquierda local (Eje X local)
        float direccionX = 0f;

        if (centroLocal.x > 0.05f)
        {
            direccionX = 1f;  // El centro está a la derecha, muévete a la derecha
        }
        else if (centroLocal.x < -0.05f)
        {
            direccionX = -1f; // El centro está a la izquierda, muévete a la izquierda
        }
        else
        {
            closing = false;  // Ya llegó al centro, se detiene
            return;
        }

        // 3. Mover el muro ÚNICAMENTE en su propio eje X de lado
        // Al usar transform.right, se moverá recto sin importar cómo lo hayas rotado
        transform.position += transform.right * direccionX * speed * Time.deltaTime;
    }

    public void CloseWall()
    {
        closing = true;
    }
}
