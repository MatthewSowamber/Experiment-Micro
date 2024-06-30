using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlank : MonoBehaviour
{
        public Rigidbody platform; 
        private bool hasPlayerExited; 
        float timer = 0f;
    [SerializeField] float falltime;
    public AudioManager am;

    void Start() 
        { 
            platform = GetComponent<Rigidbody>(); 

            platform.isKinematic = true; 

            platform.useGravity = false; 
        }

        private void OnTriggerEnter(Collider other)
        {
        if (other.transform.tag == "Player")
        {

            hasPlayerExited = true;
            am.AudioTrigger(AudioManager.SoundFXCat.Woodbreaking, transform.position, 1f);
        }

        }

        void OnCollisionEnter(Collision collision) 
        {
            hasPlayerExited = false;
        }

        void OnCollisionExit() 
        {
            hasPlayerExited = true; 
        }

        void Update() 
        { if (hasPlayerExited == true)

        {
            timer += Time.deltaTime; 
                if (timer > falltime) 
                {
                platform.isKinematic = false; 
                    
                platform.useGravity = true; 
                }
            } 
        }
    
}
