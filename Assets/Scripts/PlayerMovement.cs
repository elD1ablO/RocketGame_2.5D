using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class PlayerMovement : MonoBehaviour
{
    
    Rigidbody rb;
    AudioSource audioSource;

    [Title("Thrust")]
    [SerializeField] float mainThrust;
    [SerializeField] float rotationThrust;
    
    [Title("Sounds")]
    [SerializeField] AudioClip mainEngine;

    [Title("ThrustFlames")]
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
       
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {            
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainThrustParticles.isPlaying)
            {
                mainThrustParticles.Play();
            }            
        }
        else
        {
            audioSource.Stop();
            mainThrustParticles.Stop();
        }            
            
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            if (!leftThrustParticles.isPlaying)
            {
                leftThrustParticles.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            if (!rightThrustParticles.isPlaying)
            {
                rightThrustParticles.Play();
            }
        }
        else
        {
            leftThrustParticles.Stop();
            rightThrustParticles.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;  //freezing rotation to rotate manually
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze rotation 
    }
}
