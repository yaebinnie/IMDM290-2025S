using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateHeart : MonoBehaviour
{
    GameObject[] spheres;
    static int numSphere = 200;
    float time = 0f;
    Vector3[] startPosition, endPosition;
    float lerpFraction;
    public Material chocolateMaterial;


    void Start()
    {
        if (chocolateMaterial == null)
        {
            chocolateMaterial = Resources.Load<Material>("ChocolateMaterial");
        }

        spheres = new GameObject[numSphere];
        startPosition = new Vector3[numSphere];
        endPosition = new Vector3[numSphere];

        for (int i = 0; i < numSphere; i++)
        {
            float r = 10f;
            startPosition[i] = new Vector3(r * Random.Range(-1f, 1f), r * Random.Range(-1f, 1f), r * Random.Range(-1f, 1f));

            float scaleFactor = 0.5f;

            float t = (float)i / numSphere * Mathf.PI * 2;
            float x = scaleFactor * (16 * Mathf.Pow(Mathf.Sin(t), 3));
            float y = scaleFactor * (13 * Mathf.Cos(t) - 5 * Mathf.Cos(2 * t) - 2 * Mathf.Cos(3 * t) - Mathf.Cos(4 * t));

            endPosition[i] = new Vector3(x, y, 0);

        }

        for (int i = 0; i < numSphere; i++)
        {
            spheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            spheres[i].transform.position = startPosition[i];

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

        for (int i = 0; i < numSphere; i++)
        {
            // Lerp movement
            lerpFraction = Mathf.Sin(time) * 0.5f + 0.5f;
            spheres[i].transform.position = Vector3.Lerp(startPosition[i], endPosition[i], lerpFraction);
        }
    }
}
