using System.Collections;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeedX;
    [SerializeField] private float rotationSpeedY;
    [SerializeField] private float rotationSpeedZ;

    void Update()
    {
        RotateObject();
    }

    private void RotateObject()
    {
        float rotationX = 360 * rotationSpeedX * Time.deltaTime;
        float rotationY = 360 * rotationSpeedY * Time.deltaTime;
        float rotationZ = 360 * rotationSpeedZ * Time.deltaTime;

        transform.Rotate(rotationX , rotationY , rotationZ , Space.World);
    }
}
