using System;
using UnityEngine;

public class ModelColorChanger : MonoBehaviour
{
    [SerializeField] private Color color = Color.white;  // ���� �������� � ����������
    [SerializeField] private Renderer modelRenderer;    // ���������� ���� Renderer

    private void OnValidate()  // ������������� ���������� ��� ��������� � ����������
    {
        if (modelRenderer != null)
        {
            try
            {
                modelRenderer.materials[0].color = color;
            }
            catch (Exception e)
            {
                Debug.LogWarning("[ModelColor] OnValidate " + e.StackTrace);
            }
        }
    }
}