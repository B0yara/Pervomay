using UnityEngine;

public class ModelColorChanger : MonoBehaviour
{
    [SerializeField] private Color color = Color.white;  // Поле появится в инспекторе
    [SerializeField] private Renderer modelRenderer;    // Перетащите сюда Renderer

    private void OnValidate()  // Автоматически вызывается при изменении в инспекторе
    {
        if (modelRenderer != null)
        {
            modelRenderer.material.color = color;
        }
    }
}