using UnityEngine;

namespace PlayerScripts
{
    public class CameraLook : MonoBehaviour
    {
        [SerializeField] float Sensitivity;
        [SerializeField] Transform Player;
        [SerializeField] float RecoilSpeed = 20;
        [SerializeField] Transform GunCamera;
        float mouseX;
        float mouseY;
        float xRotation;
        float yRotation;
        float sideRecoil;
        float upRecoil;
        float rot = 0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        void Update()
        {
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");

            xRotation -= mouseY * Sensitivity + upRecoil;
            yRotation += mouseX * Sensitivity + sideRecoil;

            upRecoil -= RecoilSpeed * Time.deltaTime;

            if (upRecoil < 0)
                upRecoil = 0;
        
            sideRecoil = 0;

            xRotation = Mathf.Clamp(xRotation,-65f,60f);
        
            transform.localRotation = Quaternion.Euler(xRotation,0f, rot); 
            Player.rotation = Quaternion.Euler(0,yRotation,0);

        }
        public void AddRecoil(float up, float side)
        {
            sideRecoil += side;
            upRecoil += up;
        }

        public void InvertCamera()
        {
            rot = 180f;
            GunCamera.localRotation = Quaternion.Euler(0f,0f, rot);
        }
    }
}
