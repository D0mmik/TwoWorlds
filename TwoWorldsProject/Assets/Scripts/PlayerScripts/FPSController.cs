using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class FPSController : MonoBehaviour
    {
        [SerializeField] float Speed = 10; 
        [SerializeField] float JumpForce = 10f;
        [SerializeField] float GroundDrag = 6f;
        [SerializeField] float AirDrag = 2f;
        [SerializeField] float AirMovement = 0.4f;
        bool isGrounded;    
        Transform transform1;
        Vector3 moveDirection;
        Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            transform1 = transform;
        }
        void Update()
        {  
            isGrounded = Physics.Raycast(transform.position, Vector3.down,1.1f);

            rb.drag = isGrounded ? GroundDrag : AirDrag;
        
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
        
            moveDirection = transform1.forward * vertical + transform1.right * horizontal;
        }
        void FixedUpdate()
        {
            AirMovement = isGrounded ? 0.4f : 1;
        
            rb.AddForce(moveDirection.normalized * (Speed * AirMovement), ForceMode.Acceleration);
        
            if (Input.GetKey(KeyCode.Space) || isGrounded)
                rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        }
    }
}
