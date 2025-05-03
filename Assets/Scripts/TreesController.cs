using System.Collections;
using UnityEngine;

public class TreesController : MonoBehaviour
{
    [SerializeField] Transform TreesParent;
    [SerializeField] float TargerAngle = -35f;
    [SerializeField] float RotationDuration = 1f;
    private bool _treesActive = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_treesActive)
        {
            _treesActive = true;
            // Activate trees or perform any other action
            Debug.Log("Trees activated!");
            StartCoroutine(ActivateTrees());
        }
    }

    IEnumerator ActivateTrees()
    {
        for (float t = 0; t < RotationDuration; t += Time.deltaTime)
        {
            TreesParent.localRotation = Quaternion.Lerp(TreesParent.localRotation, Quaternion.Euler(TargerAngle, 0, 0), t / RotationDuration);
            yield return null;
        }

    }
}
