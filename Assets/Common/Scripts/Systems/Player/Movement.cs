using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float _movespeed = 1f;
    [SerializeField] Transform _cameraTransform;
    Vector3 _moveDirection;
    Camera _camera;
    [SerializeField] LayerMask _floors;

    public bool Jumping = false;
    public bool Grounded = false;
    [SerializeField] float _groundRayDistance = 1f;
    public Vector2 FrameInput { get; private set; } = Vector2.zero;

    void Start()
    {
        _camera = Camera.main;
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.position += _moveDirection * _movespeed * Time.deltaTime;
    }

    void Update()
    {
        Grounded = Physics.Raycast(transform.position, Vector3.down, 1f, _floors);


        if (Grounded)
            Debug.DrawRay(transform.position, Vector3.down * _groundRayDistance, Color.green);
        else
            Debug.DrawRay(transform.position, Vector3.down * _groundRayDistance, Color.red);


        if (Grounded && Jumping)
            Jumping = false;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        FrameInput = new Vector2(h, v);

        Vector3 camRight = _camera.transform.right;
        Vector3 camForward = _camera.transform.forward;

        camRight.y = 0;
        camForward.y = 0;
        camRight.Normalize();
        camForward.Normalize();

        _moveDirection = (camRight * h + camForward * v).normalized;
        transform.LookAt(transform.position + _moveDirection);
        if (Input.GetKeyDown(KeyCode.Space) && !Jumping)
        {
            _rb.linearVelocity += Vector3.up * 5f;
            Jumping = true;
        }
    }
}
