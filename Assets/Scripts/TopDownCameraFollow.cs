using UnityEngine;

public class TopDownCameraFollow : MonoBehaviour
{
    public Transform target;
    public float height = 10f;
    public float distance = 5f;
    public float smoothSpeed = 0.125f;

    private Vector3 offset; // Смещение камеры относительно цели

    void Start()
    {
        // Рассчитываем начальное смещение
        CalculateOffset();
    }

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not assigned for TopDownCameraFollow script!");
            return;
        }
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

    }

    void CalculateOffset()
    {
        // Рассчитываем смещение на основе высоты, расстояния и угла
        offset = new Vector3(0, 0, -distance);
        offset.y = height;
    }

    // Метод для ручного изменения параметров камеры во время выполнения
    public void SetCameraParameters(float newHeight, float newDistance, float newAngle)
    {
        height = newHeight;
        distance = newDistance;
        CalculateOffset();
    }
}