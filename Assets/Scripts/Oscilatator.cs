using Unity.Mathematics;
using UnityEngine;
// to move objets

public class Oscilat : MonoBehaviour
{
    [SerializeField] Vector3 MovementVector;
    [SerializeField] float Speed;
    Vector3 StartPosition;
    Vector3 EndPosition;
    float MovementFactor;

    void Start()
    {
        StartPosition = transform.position;
        EndPosition = StartPosition + MovementVector;
    }

    void Update()
    {
        MovementFactor = Mathf.PingPong(Time.time * Speed, 1f);
        transform.position = Vector3.Lerp(StartPosition, EndPosition, MovementFactor);
    }
}
