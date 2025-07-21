using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class CollectibleBase : MonoBehaviour
{
    [Header("Collectible Settings")]
    [Tooltip("Cu�ntos puntos da al recoger")]
    [SerializeField] protected int pointValue = 1;

    [Header("Feedback")]
    [SerializeField] private AudioSource collectSFX;
    [SerializeField] private ParticleSystem collectVFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Sonido y part�culas
        if (collectSFX != null) collectSFX.Play();
        if (collectVFX != null)
            Instantiate(collectVFX, transform.position, Quaternion.identity);

        // L�gica concreta de cada collectible
        OnCollected();

        // Destruir el objeto (o pool�alo si usas pooling)
        Destroy(gameObject);
    }

    /// <summary>
    /// Cada subclase dispara su evento: CoinCollected o FruitCollected
    /// </summary>
    protected abstract void OnCollected();
}
