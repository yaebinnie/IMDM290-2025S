// UMD IMDM290 
// Instructor: Myungin Lee
    // [a <-----------> b]
    // Lerp : Linearly interpolates between two points. 
    // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Vector3.Lerp.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    GameObject[] spheres;
    static int numSphere = 500; 
    float time = 0f;
    Vector3[] initPos;
    Vector3[] startPosition, endPosition;
    float lerpFraction; // Lerp point between 0~1
    float t;

    // Start is called before the first frame update
    void Start()
    {
        // Assign proper types and sizes to the variables.
        spheres = new GameObject[numSphere];
        initPos = new Vector3[numSphere]; // Start positions
        startPosition = new Vector3[numSphere]; 
        endPosition = new Vector3[numSphere]; 
        
        // Define target positions. Start = random, End = heart 
        for (int i =0; i < numSphere; i++){
            // Random start positions
            float r = 15f;
            startPosition[i] = new Vector3(r * Random.Range(-1f, 1f), r * Random.Range(-1f, 1f), r * Random.Range(-1f, 1f));        
            // Heart shape end position
            t = i* 2 * Mathf.PI / numSphere;
            endPosition[i] = new Vector3( 
                        5f*Mathf.Sqrt(2f) * Mathf.Sin(t) *  Mathf.Sin(t) *  Mathf.Sin(t),
                        5f* (- Mathf.Cos(t) * Mathf.Cos(t) * Mathf.Cos(t) - Mathf.Cos(t) * Mathf.Cos(t) + 2 *Mathf.Cos(t)) + 3f,
                        10f + Mathf.Sin(time));
        }
        // Let there be spheres..
        for (int i =0; i < numSphere; i++){
            float r = 10f; // radius of the circle
            // Draw primitive elements:
            // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/GameObject.CreatePrimitive.html
            spheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere); 

            // Position
            initPos[i] = startPosition[i];
            spheres[i].transform.position = initPos[i];

            // Color
            // Get the renderer of the spheres and assign colors.
            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            // HSV color space: https://en.wikipedia.org/wiki/HSL_and_HSV
            float hue = (float)i / numSphere; // Hue cycles through 0 to 1
            Color color = Color.HSVToRGB(hue, 1f, 1f); // Full saturation and brightness
            sphereRenderer.material.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Measure Time 
        time += Time.deltaTime; // Time.deltaTime = The interval in seconds from the last frame to the current one
        // what to update over time?
        for (int i =0; i < numSphere; i++){
            // Lerp : Linearly interpolates between two points.
            // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Vector3.Lerp.html
            // Vector3.Lerp(startPosition, endPosition, lerpFraction)
            
            // lerpFraction variable defines the point between startPosition and endPosition (0~1)
            // let it oscillate over time using sin function
            lerpFraction = Mathf.Sin(time) * 0.5f + 0.5f;

            // Lerp logic. Update position       
            t = i* 2 * Mathf.PI / numSphere;
            spheres[i].transform.position = Vector3.Lerp(startPosition[i], endPosition[i], lerpFraction);
            // For now, start positions and end positions are fixed. But what if you change it over time?
            // startPosition[i]; endPosition[i];

            // Color Update over time
            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            float hue = (float)i / numSphere; // Hue cycles through 0 to 1
            Color color = Color.HSVToRGB(Mathf.Abs(hue * Mathf.Sin(time)), Mathf.Cos(time), 2f + Mathf.Cos(time)); // Full saturation and brightness
            sphereRenderer.material.color = color;
        }
    }
}
