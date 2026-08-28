using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Fuel":
                Debug.Log("Fuel object Enter");
                break;
        }
    }
}