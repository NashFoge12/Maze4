using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    [Header("Configuración")]
    public float scrollSpeed = 50f;
    public float startPositionY = -300f; // Debe coincidir con el Pos Y que pusiste en el RectTransform

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        // Cada vez que el panel se activa, reinicia la posición automáticamente
        ResetScroll();
    }

    void Update()
    {
        rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
    }

    public void ResetScroll()
    {
        rectTransform.anchoredPosition = new Vector2(0f, startPositionY);
    }
}