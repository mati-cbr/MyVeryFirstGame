using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class HeroKnightBehaviour : MonoBehaviour
{
    public InputActionAsset InputActions;

    private float horizontalMovement;

    private InputAction m_moveAction;
    private InputAction m_lookAction;
    private InputAction m_jumpAction;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_lookAction = InputSystem.actions.FindAction("Look");
        m_jumpAction = InputSystem.actions.FindAction("Jump");

        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        m_moveAction.performed += MoveAction;
        m_moveAction.canceled += MoveAction;
    }

    // Pozbyć się tego brzydkiego Update
    void Update()
    {
        if (m_jumpAction.WasPressedThisFrame())
        {
            Jump();
        }
    }

    private void MoveAction(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * 5f, rb.linearVelocity.y);
    }

    // Zamienić na handler tak jak w MoveAction
    private void Jump()
    {
        rb.AddForceAtPosition(new Vector3(0, 5f, 0), Vector3.up, ForceMode2D.Impulse);
    }
}
