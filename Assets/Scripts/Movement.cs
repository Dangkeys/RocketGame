using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] float mainEngineVolume = 0.8f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThurst = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThursterParticles;
    [SerializeField] ParticleSystem rightThursterParticles;

    AudioSource audioSource;
    Rigidbody rb;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }


    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }
    
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThurst);
        if (!rightThursterParticles.isPlaying)
            rightThursterParticles.Play();
    }
    private void RotateRight()
    {
        ApplyRotation(-rotationThurst);
        if (!leftThursterParticles.isPlaying)
            leftThursterParticles.Play();
    }

    private void StopRotating()
    {
        rightThursterParticles.Stop();
        leftThursterParticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    }

    private void StartThrusting()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine, mainEngineVolume);
        }
        //add force relate by local(object) not global direction
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        rb.freezeRotation = true;
        rb.freezeRotation = false;
        if (!mainEngineParticles.isPlaying)
            mainEngineParticles.Play();
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }
}
