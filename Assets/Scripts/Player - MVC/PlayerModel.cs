using System;
using System.Collections;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    // Par�metros ajustables
    public float MoveSpeed = 5f;
    public float JumpForce = 7f;
    public int MaxJumps = 2;
    public float DashSpeed = 12f;
    public float DashDuration = 0.2f;
    public float DashCooldown = 1f;
    public int MaxLife = 3;

    // Estado interno
    public int JumpsLeft { get; private set; }
    public bool CanDash { get; private set; } = true;
    public int Life { get; private set; }

    // Eventos
    public event Action OnJump;           // Salto normal
    public event Action OnDoubleJump;     // Doble salto
    public event Action OnFall;           // Inicio de ca�da
    public event Action OnLand;           // Aterrizaje
    public event Action OnDash;           // Dash
    public event Action OnDamage;         // Da�o recibido

    private void Awake()
    {
        JumpsLeft = MaxJumps;
        Life = MaxLife;
    }

    /// <summary>
    /// Intenta un salto. Dispara OnJump o OnDoubleJump seg�n corresponda.
    /// </summary>
    public bool UseJump()
    {
        if (JumpsLeft <= 0) return false;
        JumpsLeft--;
        if (JumpsLeft == MaxJumps - 1)
            OnJump?.Invoke();        // Primer salto
        else
            OnDoubleJump?.Invoke();  // Segundo salto
        return true;
    }

    /// <summary>
    /// Invocado al caer (por parte del Controller cuando la velocidad vertical es negativa).
    /// </summary>
    public void Fall()
    {
        OnFall?.Invoke();
    }

    /// <summary>
    /// Recarga saltos y dispara OnLand.
    /// </summary>
    public void Land()
    {
        JumpsLeft = MaxJumps;
        OnLand?.Invoke();
    }

    /// <summary>
    /// Intenta hacer dash y dispara OnDash.
    /// </summary>
    public bool UseDash()
    {
        if (!CanDash) return false;
        CanDash = false;
        OnDash?.Invoke();
        StartCoroutine(DashCooldownRoutine());
        return true;
    }

    private IEnumerator DashCooldownRoutine()
    {
        yield return new WaitForSeconds(DashCooldown);
        CanDash = true;
    }

    /// <summary>
    /// El jugador recibe da�o.
    /// </summary>
    public void TakeDamage()
    {
        Life--;
        OnDamage?.Invoke();
    }
}