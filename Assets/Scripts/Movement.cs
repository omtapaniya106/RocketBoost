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
            rb.AddRelativeForce(Vector3.up * ThrustStength * Time.fixedDeltaTime);
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(MainEngine);   
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
        float RotationInput = Rotation.ReadValue<float>();

        if(RotationInput < 0)
        {
            ApplyRotation(RotationStength);
        }
        else if(RotationInput > 0)
        {
            ApplyRotation(-RotationStength);
        }
    }

    private void ApplyRotation(float RotationthisFram)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * RotationthisFram * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
