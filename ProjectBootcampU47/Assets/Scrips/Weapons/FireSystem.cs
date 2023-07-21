using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FireSystem : MonoBehaviour
{



    public float reloadCooldown;
    public float ammoInGun;
    public float ammoInPocket;
    public float ammoMax;
    float addableAmmo;
    float reloadTimer;

    public TextMeshProUGUI ammoCounter;
    public TextMeshProUGUI pocketAmmoCounter;

    RaycastHit hit;
    RaycastHit TakeHit;

    public GameObject impactEffect;
    public float damage = 10f;
    public float impactForce = 30f;
    public GameObject rayPoint;
    public GameObject MainRayPoint;

    public CharacterController characterController;

    Animator gunAnimSet;
    public bool canFire;
    float gunTimer;
    public float gunCooldown;
    public float range;
    public float takeRange;

    public ParticleSystem muzzleFlash;

    AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip reloadSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gunAnimSet = GetComponent<Animator>();
    }

    void Update()
    {
        gunAnimSet.SetFloat("Speed", characterController.velocity.magnitude);

        Sprinting();
        Walking();

        if (Physics.Raycast(MainRayPoint.transform.position, MainRayPoint.transform.forward, out TakeHit, takeRange))
        {
            if (TakeHit.collider.gameObject.tag == "Ammo" && ammoInPocket < 120)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ammoInPocket = ammoInPocket + 60;
                    Destroy(TakeHit.collider.gameObject);
                }
            }

        }
        ammoCounter.text = ammoInGun.ToString();
        pocketAmmoCounter.text = ammoInPocket.ToString();

        addableAmmo = ammoMax - ammoInGun;

        if (addableAmmo > ammoInPocket)
        {
            addableAmmo = ammoInPocket;
        }

        if (Input.GetKeyDown(KeyCode.R) && addableAmmo > 0 && ammoInPocket > 0 && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.W))
        {
            if (Time.time > reloadTimer)
            {
                StartCoroutine(Reload());
                reloadTimer = Time.time + reloadCooldown;
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && canFire && Time.time > gunTimer && ammoInGun > 0 && !Input.GetKey(KeyCode.LeftShift))
        {
            Fire();
            gunTimer = Time.time + gunCooldown;
        }
    }

    IEnumerator Reload()
    {
        gunAnimSet.SetBool("isReloading", true);
        canFire = false;

        audioSource.clip = reloadSound;
        audioSource.Play();

        yield return new WaitForSeconds(0.3f);
        gunAnimSet.SetBool("isReloading", false);

        yield return new WaitForSeconds(2.2f);
        ammoInGun = ammoInGun + addableAmmo;
        ammoInPocket = ammoInPocket - addableAmmo;
        canFire = true;
    }

    void Fire()
    {


        ammoInGun--;

        if (Physics.Raycast(rayPoint.transform.position, rayPoint.transform.forward, out hit, range))
        {
            muzzleFlash.Play();


            audioSource.clip = fireSound;
            audioSource.Play();

            gunAnimSet.Play("Fire", -1, 0f);


            Target target = hit.transform.GetComponent<Target>();
            

            //target.health>0 is added to control death animation properly -- added
            if (target != null && target.health>0)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
            Debug.Log(hit.transform.name);




        }

    }

    void Walking()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            gunAnimSet.SetBool("isWalking", true); // Set the "W" parameter to true in the Animator
        }
        // Check for shift key release
        if (Input.GetKeyUp(KeyCode.W))
        {
            gunAnimSet.SetBool("isWalking", false); // Set the "W" parameter to false in the Animator
        }
    }

    void Sprinting()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            gunAnimSet.SetBool("isSprinting", true); // Set the "isSprinting" parameter to true in the Animator
        }
        else
        {
            gunAnimSet.SetBool("isSprinting", false); // Set the "isSprinting" parameter to false in the Animator
        }


    }
}


