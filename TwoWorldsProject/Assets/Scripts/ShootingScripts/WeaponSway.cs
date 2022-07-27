using UnityEngine;

namespace ShootingScripts
{
    public class WeaponSway : MonoBehaviour
    {
        [SerializeField] float Smooth;
        [SerializeField] float SwayMultiplierX;
        [SerializeField] float SwayMultiplierY;
        void Update()
        {
            var mouseX = Input.GetAxisRaw("Mouse X") * SwayMultiplierX;
            var mouseY = Input.GetAxisRaw("Mouse Y") * SwayMultiplierY;

            var rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
            var rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

            var targetRotation = rotationX * rotationY;

            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Smooth * Time.deltaTime);

        }
        
    }
}
