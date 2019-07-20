using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Rocket : MonoBehaviour
{
    //Components of GameObjects
    Rigidbody rigidBody; //to access rigid body components

    AudioSource thurstSFX;
    
    [SerializeField] float rcsThrust = 100f;

    [SerializeField]  float mainthrust = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        thurstSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Launch Pad":
                //do nothing
                 break;
            
            case  "Dead":
                Destroy(gameObject);
                
                print("You are dead now");
                break;
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float straight = mainthrust * Time.deltaTime;
                
            print("Thrusting");
            rigidBody.AddRelativeForce(Vector3.up * straight);
            if (!thurstSFX.isPlaying) //
            {
                thurstSFX.Play();
            }
        }
        else
        {
            thurstSFX.Stop();
        }

     
    }
    
    
    private  void Rotate()
    {

        rigidBody.freezeRotation = true;
        
        float rotationFrame = rcsThrust * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.A))
        {
            
            print("Turn Left");
            transform.Rotate(Vector3.forward * rotationFrame) ;
        }

        if (Input.GetKey(KeyCode.D))
        {
            print("Turn Right");
            transform.Rotate(- Vector3.forward* rotationFrame);
        }

        rigidBody.freezeRotation = false;
    }

    
}
