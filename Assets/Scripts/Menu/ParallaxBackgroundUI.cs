using UnityEngine;

public class ParallaxBackgroundUI : MonoBehaviour
{
    public RectTransform[] backgrounds; // Las im�genes de fondo en el Canvas.
    public float[] parallaxSpeeds; // Velocidades diferentes para cada capa para crear el efecto parallax.

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Mueve cada imagen hacia la izquierda continuamente.
            backgrounds[i].anchoredPosition += Vector2.left * parallaxSpeeds[i] * Time.deltaTime;
        }
    }
}
