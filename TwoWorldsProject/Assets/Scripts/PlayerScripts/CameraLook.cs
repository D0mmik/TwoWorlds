using UnityEngine;

namespace PlayerScripts
{
    public class CameraLook : MonoBehaviour
    {
        [SerializeField] float Sensitivity;
        [SerializeField] Transform Player;
        [SerializeField] float RecoilSpeed = 20;
        float mouseX;
        float mouseY;
        float xRotation;
        float yRotation;
        float sideRecoil;
        float upRecoil;

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
        
            transform.localRotation = Quaternion.Euler(xRotation,0f, 0f); 
            Player.rotation = Quaternion.Euler(0,yRotation,0);
        }
        public void AddRecoil(float up, float side)
        {
            sideRecoil += side;
            upRecoil += up;
        }
    }
}
