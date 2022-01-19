using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class RotateArround : MonoBehaviour
{
    public GameObject target;
    public float speed = 20;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.isPlaying ) transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);

        transform.LookAt(target.transform.position);
    }
}
