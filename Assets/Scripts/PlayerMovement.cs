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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }  

    void StartThrusting()
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
    void StopThrusting()
    {
        audioSource.Stop();
        mainThrustParticles.Stop();
    }
    void ProcessRotation()
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
    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }
    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }
    void StopRotating()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
    }
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;  //freezing rotation to rotate manually
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze rotation 
    }    
}
