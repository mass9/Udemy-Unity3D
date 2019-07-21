using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f ,10f);

    [SerializeField] private float periods = 2f;
    
    // Start is called before the first frame update
    [Range(0,1)] [SerializeField] float movementfactor;

    private Vector3 startingPos;
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(periods <= Mathf.Epsilon){ return; }
        float cycles = Time.time / periods; //grows continuelyy from 0 to 1

        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(cycles*tau); // -1 to +1
        
        print(rawSinWave);

        movementfactor = (float) (rawSinWave/2f + 0.5); // 0 to 1
        Vector3 offset = movementVector * movementfactor;
        transform.position = startingPos + offset;
    }
}
