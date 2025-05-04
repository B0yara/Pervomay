using UnityEngine;

public class Boss : Entity
{
    [HideInInspector] GameController gcm;
    protected override void Start()
    {
        base.Start();
        gcm = FindFirstObjectByType<GameController>();

    }

    protected override void Die(float time)
    {
        if (GameController.Instance != null && !GameController.Instance.VirusIsLoaded())
        {
            animator.SetBool("EndDeath", true);
            base.Die(time);
        }

        else
        {
            animator.SetBool("Death", true);
            animator.SetBool("EndDeath", false);
            GetComponent<Collider>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
        }
    }

}
