using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffects : MonoBehaviour
{
    public GameObject DeathParticles;

    public bool shakeCamera;
    [Range(0f, 1f)]
    public float duration;
    [Range(0f, 1f)]
    public float magnitude;
    void Start()
    {
        //    if(shakeCamera)
        //   StartCoroutine(FindObjectOfType<CameraShake>().Shake(duration, magnitude));
         
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))

        {
            Instantiate(DeathParticles, transform.position, Quaternion.identity);
            StartCoroutine(FindObjectOfType<CameraShake>().Shake(duration, magnitude));
        }


    }
}
