using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{   
    [SerializeField] private GameObject target;
    private Vector3 currentPos;
    [SerializeField] private float duration = 2.0f;
    private float elapsedTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            Vector3 interpolatePos = Interpolation.Lerp(currentPos, target.transform.position, t, Interpolation.EasingType.EaseIn);
            transform.position = interpolatePos;
        }
    }
}
