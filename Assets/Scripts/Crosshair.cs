using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public float interactDistance = 3f;

    void OnGUI()
    {
        Color crosshairColor = Color.white;

        Ray ray = new Ray(
            Camera.main.transform.position,
            Camera.main.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            Interactable interactable =
                hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                crosshairColor = Color.green;
            }
        }

        GUI.color = crosshairColor;

        GUIStyle style = new GUIStyle();
        style.fontSize = 24; // Tamaño del punto
        style.normal.textColor = crosshairColor;
        style.alignment = TextAnchor.MiddleCenter;

        GUI.Label(
            new Rect(
                Screen.width / 2 - 15,
                Screen.height / 2 - 15,
                30,
                30
            ),
            "●",
            style
        );
    }
}