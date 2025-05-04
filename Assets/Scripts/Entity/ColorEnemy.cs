using UnityEngine;

public class ModelColorChanger : MonoBehaviour
{
    [SerializeField] private Color color = Color.white;  // ���� �������� � ����������
    [SerializeField] private Renderer modelRenderer;    // ���������� ���� Renderer

    private void OnValidate()  // ������������� ���������� ��� ��������� � ����������
    {
        if (modelRenderer != null)
        {
            modelRenderer.material.color = color;
        }
    }
}