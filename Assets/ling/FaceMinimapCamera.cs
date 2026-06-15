using UnityEngine;

public class FaceMinimapCamera : MonoBehaviour
{
    public Camera minimapCamera;   // Arrástrale tu cámara del minimapa

    private void LateUpdate()
    {
        if (minimapCamera != null)
        {
            transform.LookAt(transform.position + minimapCamera.transform.forward);
        }
    }
}