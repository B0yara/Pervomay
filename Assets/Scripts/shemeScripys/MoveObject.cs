using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;     // Скорость движения
    public float sprintSpeed = 8f;   // Скорость при беге
    private float currentSpeed;      // Текущая скорость

    [Header("Mouse Look")]
    public float mouseSensitivity = 100f; // Чувствительность мыши
    public Transform playerCamera;         // Камера (должна быть дочерним объектом)
    private float xRotation = 0f;          // Угол поворота по вертикали

    [Header("Jump")]
    public float jumpForce = 5f;     // Сила прыжка
    public LayerMask groundMask;     // Слой земли (для проверки прыжка)
    public Transform groundCheck;    // Объект для проверки земли
    public float groundDistance = 0.4f; // Дистанция до земли
    private bool isGrounded;         // На земле ли игрок

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Курсор скрыт и в центре
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        // === Поворот камеры мышью ===
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Вертикальный поворот (камера вверх/вниз)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ограничиваем угол
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Горизонтальный поворот (игрок влево/вправо)
        transform.Rotate(Vector3.up * mouseX);

        // === Прыжок ===
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // === Бег (Shift) ===
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
    }

    void FixedUpdate()
    {
        // === Движение WASD ===
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Движение ВПЕРЁД относительно поворота игрока
        Vector3 moveDirection = (transform.forward * moveVertical + transform.right * moveHorizontal).normalized;
        Vector3 moveVelocity = moveDirection * currentSpeed;

        // Применяем движение через Rigidbody (без изменения Y, чтобы не мешать прыжку)
        rb.linearVelocity = new Vector3(moveVelocity.x, rb.linearVelocity.y, moveVelocity.z);
    }
}