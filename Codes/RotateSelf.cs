using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    public float rotationSpeed = 30f; // Adjust the speed as needed

    void Update()
    {
        // Rotate the GameObject continuously around the Z-axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
