using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieController : MonoBehaviour
{
    //Change FPS controller
    [SerializeField] private Transform player;

    [SerializeField] private AudioClip[] roamAudioClipList;
    [SerializeField] private AudioClip[] chaseAudioClipList;
    [SerializeField] private AudioClip[] footStepAudioClipList;

    private ZombieAI zombieIA;
    private Animator animator;
    private CharacterController controller;
    private AudioSource audioSource;
    private AudioClip currentClip;

    private float roamSoundVolume;
    private float footStepSoundVolume;
    private float chaseSoundVolume = 0.1f;

    //Roam Sound Time Intervals
    private float minRoamTimeBetweenPlay = 5f;
    private float maxRaomTimeBetweenPlay = 10f;

    //Chase Sound Time Intervals
    private float minChaseTimeBetweenPlay = 1f;
    private float maxChaseTimeBetweenPlay = 5f;

    private float timeCountDown;

    private Vector3 zombieDistance;

    private void Awake()
    {
        zombieIA = GetComponent<ZombieAI>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        timeCountDown = UnityEngine.Random.Range(minRoamTimeBetweenPlay, maxRaomTimeBetweenPlay);
    }

    private void Update()
    {
        zombieDistance = player.position - transform.position;
        
        ControlSoundVolume();
        SoundManager();
        AnimationManager();
    }


    private void AnimationManager()
    {
        if (zombieIA.isRoaming)
        {
            animator.SetBool("Roam", true);
            animator.SetBool("Attack", false);
            animator.SetBool("Chase", false);

        }
        else if (zombieIA.isChasing)
        {
            animator.SetBool("Chase", true);
            animator.SetBool("Attack", false);
        }
        else if(zombieIA.isAttacking)
        {
            animator.SetBool("Chase", false);
            animator.SetBool("Attack", true);
        }
    }

    private void SoundManager()
    {
        //Roaming Sound
        if (!audioSource.isPlaying && zombieIA.isRoaming)
        {
            if (timeCountDown <= 0f)
            {
                currentClip = roamAudioClipList[UnityEngine.Random.Range(0, roamAudioClipList.Length)];
                audioSource.clip = currentClip;
                audioSource.volume = roamSoundVolume;
                audioSource.Play();
                timeCountDown = UnityEngine.Random.Range(minRoamTimeBetweenPlay, maxRaomTimeBetweenPlay);
            }
            else
            {
                timeCountDown -= Time.deltaTime;
            }
        } //Chasing Sound
        else if (!audioSource.isPlaying && zombieIA.isChasing)
        {
            if (timeCountDown <= 0f)
            {
                currentClip = chaseAudioClipList[UnityEngine.Random.Range(0, chaseAudioClipList.Length)];
                audioSource.clip = currentClip;
                audioSource.volume = chaseSoundVolume;
                audioSource.Play();
                timeCountDown = UnityEngine.Random.Range(minChaseTimeBetweenPlay, maxChaseTimeBetweenPlay);
            }
            else
            {
                timeCountDown -= Time.deltaTime;
            }
        }
    }

    //To control roam and foot step sound volume thanks to distance between zombie and player
    private Tuple<float, float> ControlSoundVolume()
    {
        if (zombieDistance.magnitude > zombieIA.sightRange * 3)
        {
            return Tuple.Create(footStepSoundVolume = 0.05f, roamSoundVolume = 0.01f);
        }
        else if (zombieDistance.magnitude <= zombieIA.sightRange * 3 && zombieDistance.magnitude > zombieIA.sightRange * 2)
        {
            return Tuple.Create(footStepSoundVolume = 0.1f, roamSoundVolume = 0.05f);
        }
        else if (zombieDistance.magnitude <= zombieIA.sightRange * 2 && zombieDistance.magnitude > zombieIA.sightRange)
        {
            return Tuple.Create(footStepSoundVolume = 0.2f, roamSoundVolume = 0.1f);
        }
        //In sightrange not roaming anymore. It is chasing, so roamSoundVolume = 0
        return Tuple.Create(footStepSoundVolume = 0.2f, roamSoundVolume = 0f);
    }


    //Animation event sound (foot steps)
    private void OnFootStepZombie(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (footStepAudioClipList.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, footStepAudioClipList.Length);
                AudioSource.PlayClipAtPoint(footStepAudioClipList[index], transform.TransformPoint(controller.center), footStepSoundVolume);
            }
        }
    }

}
