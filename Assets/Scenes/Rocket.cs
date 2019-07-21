using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    //Components of GameObjects
    Rigidbody rigidBody; //to access rigid body components
    AudioSource thurstSFX;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem deathParticle;
    
    enum State
    {
        Alive,
        Dying,
        Transceding
    };

    private State state = State.Alive;

   

    [SerializeField] float rcsThrust = 100f;

    [SerializeField]  float mainthrust = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        thurstSFX = GetComponent<AudioSource>();
        mainEngineParticle.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {   
        if (state != State.Alive )
        {
            return;
        }
        
        switch (other.gameObject.tag)
        {
             
                
            case "Launch Pad":
                //do nothing
                 break;
            
            case "Finish":
                successParticle.Play();
                state = State.Transceding;
                print("You've won");
                
                Invoke("LoadNextScene",1f); //Load after 1 second
                break;
            
            case  "Dead":
                deathParticle.Play();
                state = State.Dying;
                Invoke("LoadSameScene",1f); 
                
                print("You are dead now");
                break;
        }

        
    }

    private  void LoadSameScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene( 1);
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float straight = mainthrust * Time.deltaTime;
            mainEngineParticle.Play();

            print("Thrusting");
            rigidBody.AddRelativeForce(Vector3.up * straight);
           
            if (!thurstSFX.isPlaying) //
            {
                thurstSFX.Play();
             
            }
        }
        else
        {
            mainEngineParticle.Stop();
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
