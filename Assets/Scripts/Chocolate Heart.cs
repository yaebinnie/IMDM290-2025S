// IMDM290
// Instructor: Myungin Lee
// Student: Yaebin Park
// Chocolate Heart


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateHeart : MonoBehaviour
{
    GameObject[] spheres;
    static int numSphere = 500;
    float time = 0f;
    Vector3[] initialPosition;
    Vector3[] startPosition, endPosition;
    float lerpFraction;
    float t;
    
    // added material to the original heart
    public Material chocolateMaterial;

    // added velocity and frame size to perform a bounding box
    Vector3 heartVelocity = new Vector3(2f, 1.5f, 1f);
    Vector3 heartPosition = Vector3.zero;
    Vector3 frameMin = new Vector3(-10f, -5f, -5f);
    Vector3 frameMax = new Vector3(10f, 5f, 5f);

    void Start()
    {
        if (chocolateMaterial == null)
        {
            chocolateMaterial = Resources.Load<Material>("ChocolateMaterial");
        }

        spheres = new GameObject[numSphere];
        initialPosition = new Vector3[numSphere];
        startPosition = new Vector3[numSphere];
        endPosition = new Vector3[numSphere];

        for (int i = 0; i < numSphere; i++)
        {
            float r = 15f;
            startPosition[i] = new Vector3(r * Random.Range(-1f, 1f), r * Random.Range(-1f, 1f), r * Random.Range(-1f, 1f));

            t = i * 2 * Mathf.PI / numSphere;
            endPosition[i] = new Vector3(
                        5f * Mathf.Sqrt(2f) * Mathf.Sin(t) * Mathf.Sin(t) * Mathf.Sin(t),
                        5f * (-Mathf.Cos(t) * Mathf.Cos(t) * Mathf.Cos(t) - Mathf.Cos(t) * Mathf.Cos(t) + 2 * Mathf.Cos(t)) + 3f,
                        10f + Mathf.Sin(time));
        }

        for (int i = 0; i < numSphere; i++)
        {
            spheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            initialPosition[i] = startPosition[i];
            spheres[i].transform.position = initialPosition[i];

            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            if (chocolateMaterial != null)
            {
                sphereRenderer.material = chocolateMaterial;
            }
        }
    }

    void Update()
    {
        time += Time.deltaTime;
        lerpFraction = Mathf.Sin(time) * 0.5f + 0.5f;

        // update heart's position based on velocity
        heartPosition += heartVelocity * Time.deltaTime;

        if (heartPosition.x <= frameMin.x || heartPosition.x >= frameMax.x)
            heartVelocity.x *= -1; // this reverses x direction

        if (heartPosition.y <= frameMin.y || heartPosition.y >= frameMax.y)
            heartVelocity.y *= -1; // reverses y direction

        if (heartPosition.z <= frameMin.z || heartPosition.z >= frameMax.z)
            heartVelocity.z *= -1; // reverses z direction

        for (int i = 0; i < numSphere; i++)
        {
            t = i * 2 * Mathf.PI / numSphere;
            spheres[i].transform.position = Vector3.Lerp(startPosition[i], endPosition[i], lerpFraction) + heartPosition;
        }
    }
}
