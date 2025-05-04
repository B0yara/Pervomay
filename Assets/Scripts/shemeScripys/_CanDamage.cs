using Unity.VisualScripting;
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

    protected virtual void GetDamage(int damage)
    {
        if (canDamage)
        {
            if (hp > 0)
            {
                animator.SetBool("TakeDamage", true);

                particleSystem.Play();
                hp -= damage;
            }
            CheckHP();
        }
    }

    protected virtual int GetHp()
    {
        return hp;
    }
    public void AnimDamage()
    {
        animator.SetBool("IsDamaged", true);
    }
    public void EndAnimDamage()
    {
        animator.SetBool("IsDamaged", false);
    }
    public void EndTakeDamage()
    {
        animator.SetBool("TakeDamage", false);
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
                animator.SetInteger("Hp", 0);
                deathParticleSystem.Play();
                Die(2f); // 2 ������� - ��������� ����� ��� �������� 
            }
            catch
            {
                Debug.Log("������ ��������");
            }
            hp = 0;

        }
    }

    protected virtual void Die(float time)
    {
        animator.SetBool("Death", true);
        GetComponent<Collider>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
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

    protected virtual void dmg(bool to)
    {
        canDamage = to;
    }
}