using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{



    [Range(0, 10)]
    public float magnitude = 1.67f;
    [Range(0, 10)]
    public float duration = 0.22f;

    bool shaking = true;

    Quaternion startPos;


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(!shaking)
            transform.localRotation = Quaternion.Lerp(transform.localRotation, startPos, Time.deltaTime);
        
    }


    IEnumerator Shake(float magnitude, float duration)
    {
        // shaking = true;

        startPos = transform.localRotation;
        float time = 0f;
        Debug.Log(name);
        while (time <= duration)
        {
            float x = Random.Range(-magnitude, magnitude);
            float y = Random.Range(-magnitude, magnitude);
            float z = Random.Range(-magnitude, magnitude);
            //transform.localPosition = startPos + new Vector3(x,y,0f);
            transform.Rotate(new Vector3(x,y,z));
            time += Time.deltaTime;
            yield return null;
        }
        shaking = false;
        // transform.localRotation = startPos;
    }

    public void Shake()
    {
        magnitude += 0.03f;
        StartCoroutine(Shake(magnitude, duration));
    }
}
