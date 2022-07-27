using System;
using Enemies;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        [SerializeField] bool IsGrounded;
        [SerializeField] Terrain Terrain1;
        public int KillCount;
        Transform transform1;
        Vector3 moveDirection;
        Rigidbody rb;
        CameraLook cameraLook;
        [SerializeField] Texture Texture1;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            transform1 = transform;
            cameraLook = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<CameraLook>();
        }
        void Update()
        {  
            IsGrounded = Physics.Raycast(transform.position, Vector3.down,1.01f);

            rb.drag = IsGrounded ? GroundDrag : AirDrag;
        
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
        
            moveDirection = transform1.forward * vertical + transform1.right * horizontal;

            if (KillCount >= 3)
                ChangeWorld();
        }
        void FixedUpdate()
        {
            AirMovement = IsGrounded ? 1 : 0.4f;
        
            rb.AddForce(moveDirection.normalized * (Speed * AirMovement), ForceMode.Acceleration);
        
            if (Input.GetKey(KeyCode.Space) && IsGrounded)
                rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
                SceneManager.LoadScene("Game");
        }

        void ChangeWorld()
        {
            cameraLook.InvertCamera();
            Terrain1.terrainData.terrainLayers.SetValue(1,1);
        }
    }
}
