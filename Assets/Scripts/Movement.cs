using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction ProcesThrust;
    [SerializeField] InputAction Rotation;
    [SerializeField] float ThrustStength = 1000f;
    [SerializeField] float RotationStength = 20f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();   
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
