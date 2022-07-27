using System;
using System.Collections;
using PlayerScripts;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootingScripts
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] int Ammo = 15;
        [SerializeField] TMP_Text AmmoCount;
        [SerializeField] float Damage = 10f;
        [SerializeField] Transform ShootPoint; 
        [SerializeField] GameObject Impact;
        [SerializeField] float UpRecoil = 2;
        [SerializeField] ParticleSystem MuzzleFlash;
        [SerializeField] Animator AnimGun;
        [SerializeField] AudioSource ShootSound;
        [SerializeField] AudioSource ClipOut;  
        [SerializeField] AudioSource ClipIn;
        [SerializeField] CameraLook CameraLook;
        bool canShoot = true;
        bool isAds;
        Target target;
        float sideRecoil;

        void Start()
        {
            AmmoCount.text = Ammo.ToString(); 
        }

        void Update()
        {
            isAds = Input.GetMouseButton(1);
            AnimGun.SetBool("ADS", isAds);

            if (Input.GetMouseButtonDown(0) && Ammo != 0 && canShoot)
            {
                Shoot();
                MuzzleFlash.Play();
                AnimGun.SetTrigger("Shooting");
                ShootSound.Play();
                Ammo--;
                AmmoCount.text = Ammo.ToString(); 

                UpRecoil = isAds ? 2 : 10;

                CameraLook.AddRecoil(UpRecoil / 10, Random.Range(0, 2) * 2 - 1);
            }

            if ((Ammo == 0 || Input.GetKeyDown(KeyCode.R)) && canShoot)
            {
                canShoot = false;
                Ammo = 15;
                ClipOut.Play();

                AnimGun.SetTrigger("Reloading");
                StartCoroutine(WaitForReload());
            }
        }

        IEnumerator WaitForReload()
        {
            yield return new WaitForSeconds(2);
            canShoot = true;
            AmmoCount.text = Ammo.ToString();
            ClipIn.Play();
            AnimGun.SetTrigger("Shooting");
        }
        void Shoot()
        {
            if (!Physics.Raycast(ShootPoint.position, ShootPoint.forward, out var hit, 100f)) 
                return;
       
            hit.transform.GetComponent<Target>()?.TakeDamage(Damage);
       
            //Instantiate(Impact, hit.point,Quaternion.LookRotation(hit.normal));
        }
    }
}
