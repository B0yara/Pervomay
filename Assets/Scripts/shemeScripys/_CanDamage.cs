using UnityEngine;

public class _CanDamage : MonoBehaviour
{
    [Header("_CanDamage")]
    public int maxHP = 100;
    public int hp = 100;
    public Animator animator;
    public ParticleSystem particleSystem;
    public ParticleSystem deathParticleSystem;
    public bool canDamage = true;
    public bool isDamaged = false;

    private void Start()
    {
        // Инициализация HP при старте
        hp = Mathf.Clamp(hp, 0, maxHP);
    }

    public void GetDamage(int damage)
    {
        if (canDamage)
        {
            if (hp > 0)
            {
                particleSystem.Play();
                hp -= damage;
            }
            CheckHP();
        }   
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
            try
            {
                deathParticleSystem.Play();
            }
            catch
            {
                Debug.Log("СМЕРТЬ АНИМАЦИИ");
            }
            
            hp = 0;
            Die(0);
        }
    }

    public virtual void Die(float time)
    {
        // Уничтожение объекта после анимации
        Destroy(gameObject, time); // 2 секунды - примерное время для анимации
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

    public virtual void CanIsDamage(bool to)
    {
        canDamage = to;
    }
}