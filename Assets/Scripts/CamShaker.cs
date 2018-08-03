using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShaker : MonoBehaviour
{

    Camera cam;

    [Range(0, 10)]
    public float magnitude;
    [Range(0, 10)]
    public float duration;

    bool shaking = false;

    Quaternion startPos;


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(cam && !shaking)
            cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, startPos, Time.deltaTime);
        
    }


    IEnumerator Shake(float magnitude, float duration)
    {
        shaking = true;

        cam = Camera.main;
        startPos = cam.transform.localRotation;
        float time = 0f;
        Debug.Log(cam.name);
        while (time <= duration)
        {
            float x = Random.Range(-magnitude, magnitude);
            float y = Random.Range(-magnitude, magnitude);
            float z = Random.Range(-magnitude, magnitude);
            //cam.transform.localPosition = startPos + new Vector3(x,y,0f);
            cam.transform.Rotate(new Vector3(x,y,z));
            time += Time.deltaTime;
            yield return null;
        }
        shaking = false;
        // cam.transform.localRotation = startPos;
    }

    public void Shake()
    {
        magnitude += 0.03f;
        StartCoroutine(Shake(magnitude, duration));
    }
}
