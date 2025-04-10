using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    private InputSystem_Actions input;
    private Rigidbody2D rb;
    private Vector2 pointerPos;
    [SerializeField] private float speed = 50, rotationSpeed = 360;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
        input.Enable();
        input.Player.Look.performed += Look_performed;
    }

    private void Look_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        pointerPos = Camera.main.ScreenToWorldPoint(obj.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = pointerPos - (Vector2)transform.position;
        direction.Normalize();
        rb.AddForce(direction * speed * Time.deltaTime);
        ApplyRotation(direction);
    }

    private void ApplyRotation(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public float GetLateralSpeed()
    {
        return Vector3.Dot(rb.linearVelocity, transform.right) + Vector3.Dot(rb.linearVelocity, transform.forward);
    }
}
