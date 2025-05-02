using UnityEngine;

public class _CanDamage : MonoBehaviour
{
    [Header("_CanDamage")]
    public int maxHP = 100;
    public int hp = 100;
    public Animation deathAnimation;
    public Animator animator;

    private void Start()
    {
        // Инициализация HP при старте
        hp = Mathf.Clamp(hp, 0, maxHP);
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
        CheckHP();
    }

    public void CheckHP()
    {
        // Ограничение HP максимальным значением
        if (hp > maxHP)
        {
            hp = maxHP;
        }

        // Проверка на смерть
        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
    }

    public virtual void Die()
    {
        // Воспроизведение анимации смерти, если она есть
        if (deathAnimation != null)
        {
            deathAnimation.Play();
        }

        // Уничтожение объекта после анимации
        Destroy(gameObject, 2f); // 2 секунды - примерное время для анимации
    }

    // Дополнительный метод для лечения
    public void Heal(int amount)
    {
        hp += amount;
        CheckHP();
    }

    public void SetMaxHP(int newMaxHP, bool healToFull = false)
    {
        maxHP = newMaxHP;
        if (healToFull)
        {
            hp = maxHP;
        }
        else
        {
            hp = Mathf.Min(hp, maxHP);
        }
    }
}