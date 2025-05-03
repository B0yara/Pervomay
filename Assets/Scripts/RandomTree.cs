using UnityEngine;

public class RandomTree : MonoBehaviour
{
    [SerializeField] Material[] Materials;
    [SerializeField] Vector3[] Scales;
    MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        // Выбираем случайный индекс материала
        int randomIndex = Random.Range(0, Materials.Length);
        // Применяем случайный материал к MeshRenderer
        _meshRenderer.material = Materials[randomIndex];
        _meshRenderer.transform.localScale = Scales[randomIndex];
        _meshRenderer.transform.localPosition = new Vector3(0, Scales[randomIndex].y / 2, 0);
    }
}
