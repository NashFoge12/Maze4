using UnityEngine;

public class DayNight : MonoBehaviour

{
    public Light sun;
    public float speed = 10f;

    void Update()
    {
        sun.transform.Rotate(Vector3.right * speed * Time.deltaTime);

        float angle = sun.transform.eulerAngles.x;

        // baja intensidad en la noche
        sun.intensity = Mathf.Clamp01(Vector3.Dot(sun.transform.forward, Vector3.down)) * 1.2f;
    }
}