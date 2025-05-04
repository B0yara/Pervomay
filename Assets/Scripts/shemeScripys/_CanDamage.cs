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

    public void GetDamage(int damage)
    {
        if (canDamage)
        {
            if (hp > 0)
            {
                animator.SetBool("TakeDamage", true);
                try
                {
                    particleSystem.Play();
                }
                catch
                {
                    Debug.LogWarning("[Boss] No particle system");
                }

                hp -= damage;
                GetHp();
            }
            CheckHP();
        }
    }
    protected virtual void GetHp()
    {

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
            animator.SetInteger("Hp", 0);
            hp = 0;
            Die(2f);
            try
            {
                deathParticleSystem.Play();
            }
            catch
            {
                Debug.Log("[_canDamage] No Particle System on object" + gameObject.name);
            }

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