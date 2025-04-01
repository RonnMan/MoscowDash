using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5f;          // Скорость бега вперед (м/с)
    private float laneDistance = 2f;   // Расстояние между полосами (2 м)
    private int currentLane = 1;       // Текущая полоса (0 - левая, 1 - центр, 2 - правая)
    private float jumpHeight = 6.5f;   // Сила для прыжка на 2 м
    private bool isGrounded = true;    // На земле ли персонаж
    private Rigidbody rb;              // Компонент Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Автоматическое движение вперед с учетом физики
        rb.MovePosition(transform.position + Vector3.forward * speed * Time.deltaTime);
        Debug.Log("Z position: " + transform.position.z); // Отладка движения

        // Управление свайпами
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchDelta = touch.deltaPosition;
                if (touchDelta.x < -50 && currentLane > 0) currentLane--;
                else if (touchDelta.x > 50 && currentLane < 2) currentLane++;
                else if (touchDelta.y > 50 && isGrounded) Jump();
                else if (touchDelta.y < -50) Crouch();
            }
        }

        // Управление клавишами
        if (Input.GetKeyDown(KeyCode.A) && currentLane > 0) currentLane--;
        if (Input.GetKeyDown(KeyCode.D) && currentLane < 2) currentLane++;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) Jump();
        if (Input.GetKeyDown(KeyCode.S)) Crouch();

        // Перемещение между полосами
        Vector3 targetPosition = new Vector3(currentLane * laneDistance - laneDistance, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
    }

    void Jump()
    {
        isGrounded = false;
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

    void Crouch()
    {
        transform.localScale = new Vector3(1f, 0.5f, 1f);
        Invoke("StandUp", 0.5f);
    }

    void StandUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}
