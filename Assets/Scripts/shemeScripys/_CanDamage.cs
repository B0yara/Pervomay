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
        // ������������� HP ��� ������
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
        // ����������� HP ������������ ���������
        if (hp > maxHP)
        {
            hp = maxHP;
        }

        // �������� �� ������
        if (hp <= 0)
        {
            try
            {
                deathParticleSystem.Play();
            }
            catch
            {
                Debug.Log("������ ��������");
            }
            
            hp = 0;
            Die(0);
        }
    }

    public virtual void Die(float time)
    {
        // ����������� ������� ����� ��������
        Destroy(gameObject, time); // 2 ������� - ��������� ����� ��� ��������
    }

    // �������������� ����� ��� �������
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