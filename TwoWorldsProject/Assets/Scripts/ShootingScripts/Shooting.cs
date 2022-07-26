using System.Collections;
using PlayerScripts;
using UnityEngine;

namespace ShootingScripts
{
    [RequireComponent(typeof(CameraLook))]
    public class Shooting : MonoBehaviour
    {
        [SerializeField] int Ammo = 15;
        [SerializeField] float Damage = 10f;
        [SerializeField] Transform ShootPoint; 
        [SerializeField] GameObject Impact;
        [SerializeField] float UpRecoil = 2;
        [SerializeField] ParticleSystem MuzzleFlash;
        [SerializeField] Animator AnimGun;
        [SerializeField] AudioSource ShootSound;
        [SerializeField] AudioSource ClipOut;  
        [SerializeField] AudioSource ClipIn;
        bool canShoot;
        bool isAds;
        Target target;
        CameraLook cameraLook;
        float sideRecoil;

        void Update()
        {
            isAds = Input.GetMouseButtonDown(1);
            AnimGun.SetBool("ADS", isAds);
       
            Reload();

            if (!Input.GetMouseButtonDown(0) || Ammo == 0 || !canShoot) 
                return;
            Shoot();
            MuzzleFlash.Play();
            AnimGun.SetTrigger("Shooting");
            ShootSound.Play();
            Ammo--;
            //ammoCount.text = ammo.ToString(); 

            UpRecoil = isAds ? 2 : 10;
 
            cameraLook.AddRecoil(UpRecoil / 10, Random.Range(0,2)*2-1);
        }

        void Reload()
        {
            if ((Ammo != 0 && !Input.GetKeyDown(KeyCode.R)) || !canShoot) 
                return;
       
            canShoot = false;
            Ammo = 15;
            ClipOut.Play();

            AnimGun.SetTrigger("Reloading");
            StartCoroutine(WaitForReload());
        }
        IEnumerator WaitForReload()
        {
            yield return new WaitForSeconds(2);
            canShoot = true;
            //ammoCount.text = ammo.ToString();
            ClipIn.Play();
            AnimGun.SetTrigger("Shooting");
        }
        void Shoot()
        {
            if (!Physics.Raycast(ShootPoint.position, ShootPoint.forward, out var hit, 100f)) 
                return;
       
            hit.transform.GetComponent<Target>()?.TakeDamage(Damage);
       
            Instantiate(Impact, hit.point,Quaternion.LookRotation(hit.normal));
        }
    }
}
