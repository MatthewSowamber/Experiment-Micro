using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Shrinking : MonoBehaviour
{
    [SerializeField] private float _maxSize = 1f;
    [SerializeField] private SizeBar _sizeBar;
    // Time it takes in seconds to shrink from starting scale to target scale.
    public float ShrinkDuration = 5f;
    private float _currentSize;

    // The target scale
    public Vector3 TargetScale = Vector3.one * .5f;

    // The starting scale
    Vector3 startScale;

    Vector3 newScale;

    // T is our interpolant for our linear interpolation.
    float t = 0;

    private void Start()
    {
        _currentSize = _maxSize;

        _sizeBar.UpdateSizeBar(_maxSize, _currentSize);
    }

    void OnEnable()
    {
        // initialize stuff in OnEnable
        startScale = transform.localScale;
        t = 0;
    }

    void Update()
    {

        if (!Manager.gamePaused)
        {
            _currentSize = transform.localScale.y;

            // Divide deltaTime by the duration to stretch out the time it takes for t to go from 0 to 1.
            t += Time.deltaTime / ShrinkDuration;
            _sizeBar.UpdateSizeBar(_maxSize, _currentSize);

            // Lerp wants the third parameter to go from 0 to 1 over time. 't' will do that for us.
            newScale = Vector3.Lerp(startScale, TargetScale, t);
            _sizeBar.UpdateSizeBar(_maxSize, _currentSize);

            // Debug.Log(t);

            transform.localScale = newScale;
            _sizeBar.UpdateSizeBar(_maxSize, _currentSize);

            if (t >= 1)
            {
                Manager.Lives(-3);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Shrink Beaker")
        {
            //newScale -= new Vector3(0.75f, 0.75f, 0.75f);
            t += 0.50f;
            _sizeBar.UpdateSizeBar(_maxSize, _currentSize);
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.Drinkingsound, transform.position, 1f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Growth Beaker")
        {
            //newScale -= new Vector3(0.75f, 0.75f, 0.75f);
            t -= 0.55f;
            _sizeBar.UpdateSizeBar(_maxSize, _currentSize);
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.Drinkingsound, transform.position, 1f);
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.tag == "Danger")
        {
            //transform.localScale = Vector3.one;
            t = 0f;
            _sizeBar.UpdateSizeBar(_maxSize, _currentSize);
        }
    }

}
