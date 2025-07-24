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

    protected abstract CollectibleType GetCollectibleType();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Sonido y part�culas
        PlayCollectionFeedback();

        // L�gica concreta de cada collectible
        OnCollected();

        // Destruir el objeto
        Destroy(gameObject);
    }

    protected virtual void PlayCollectionFeedback()
    {
        if (collectSFX != null) collectSFX.Play();
        if (collectVFX != null)
            Instantiate(collectVFX, transform.position, Quaternion.identity);
    }

    protected virtual void OnCollected()
    {
        // Disparar evento con informaci�n completa
        GameEventManager.TriggerCollectibleEvent(
            GetCollectibleType(),
            pointValue,
            transform.position
        );
    }
}