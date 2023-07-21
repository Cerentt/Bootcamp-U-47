//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ShootFire : MonoBehaviour
//{
//    RaycastHit hit;
//    public GameObject RayPoint;
//    public bool CanFire
//    public float gunTimer;
//    public float gunCooldown;
//    public particleSystem MuzzleFlash;

//    AudioSource audioSource;
//    public AudioClip FireSound
//        public float range;

//    // Start is called before the first frame update
//    void Start()
//    {
//        audioSource = GetComponent<AudioSourca>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKey(KeyCode.Mouse0) && CanFire == true && Time.time > reloadtimer)
//        {
//            Fire();
//            gunTimer = Time.time + gunCooldown;
//        }
//    }

//    void Fire()
//    {


//        if (Physics.Raycast(RayPoint.transform.position, RayPoint.transform.forward, out hit, range))
//        {
//            MuzzleFlash.Play();
//            audioSource.Play();

//            audioSource.clip = FireSound;
//        }
//    }
//}
