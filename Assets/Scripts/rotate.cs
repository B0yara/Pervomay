using UnityEngine;

public class rotate : MonoBehaviour
{
    public float speed = 5f; // �������� ��������

    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}