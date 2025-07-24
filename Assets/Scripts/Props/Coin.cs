using UnityEngine;

public class Coin : CollectibleBase
{
    protected override CollectibleType GetCollectibleType() => CollectibleType.Coin;

    // Opcional: puedes sobreescribir para comportamiento adicional
    protected override void OnCollected()
    {
        base.OnCollected(); // Llama a la implementaci�n base que dispara el evento

        // Comportamiento adicional espec�fico de monedas
        Debug.Log("Moneda recolectada!");
    }
}