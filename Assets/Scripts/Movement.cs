using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction ProcesThrust;
    [SerializeField] InputAction Rotation;
    [SerializeField] float ThrustStength = 1000f;
    [SerializeField] float RotationStength = 20f;
    [SerializeField] AudioClip MainEngine;
    [SerializeField] ParticleSystem MainEngineParticals;
    [SerializeField] ParticleSystem RightThrustParticals;
    [SerializeField] ParticleSystem LeftThrustParticals;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        audioSource = GetComponent<AudioSource>();

    }

    void OnEnable()
    {
        ProcesThrust.Enable();
        Rotation.Enable();
    }

    void FixedUpdate()
    {
        ProcessTrust();
        ProcessRotation();
    }

    private void ProcessTrust()
    {
        if (ProcesThrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * ThrustStength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(MainEngine);
        }
        if (!MainEngineParticals.isPlaying)
        {
            MainEngineParticals.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        MainEngineParticals.Stop();
    }

    private void ProcessRotation()
    {
        float RotationInput = Rotation.ReadValue<float>();

        if(RotationInput < 0)
        {
            RotateRight();
        }
        else if(RotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotation();
        }
    }

    private void StopRotation()
    {
        RightThrustParticals.Stop();
        LeftThrustParticals.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(-RotationStength);
        if (!LeftThrustParticals.isPlaying)
        {
            LeftThrustParticals.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(RotationStength);
        if (!RightThrustParticals.isPlaying)
        {
            RightThrustParticals.Play();
        }
    }

    private void ApplyRotation(float RotationthisFram)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * RotationthisFram * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
