using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform lookAt;
    
    private Vector3 startOffset;
    private Vector3 moveVector;
    private Vector3 animationOffset = new Vector3(0, 5, 4);

    private float transition = 0f;
    private float animationDuration = 2f;

    void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
    }

    void Update()
    {
        moveVector = lookAt.position + startOffset;    
        // X
        moveVector.x = 0;
        // Y
        moveVector.y = Mathf.Clamp(moveVector.y, 2, 4);

        if(transition > 1f)
        {
            transform.position = moveVector;
        } else
        {
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt (lookAt.position + Vector3.up);
        }
    }
}
