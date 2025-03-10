using System;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
}
public class Condition : MonoBehaviour, IDamageable
{
    public UICondition uiCondition;

    DisplayCondition health { get { return uiCondition.health; } }
    DisplayCondition stamina { get { return uiCondition.stamina; } }

    public event Action onTakeDamage;

    private void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);
        if (health.curValue == 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("사망");
    }
    public void Heal(float amout)
    {
        health.Add(amout);
    }
    public void TakeDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0f)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }
}
