using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundFXCat
    {
        Footsteps, Beakersound, Drinkingsound, Jumping, Woodbreaking
    }
    public GameObject audioObject;
    public AudioClip[] Footsteps;
    public AudioClip[] Beakersound;
    public AudioClip[] Drinkingsound;
    public AudioClip[] Jumping;
    public AudioClip[] Woodbreaking;

    public void AudioTrigger(SoundFXCat audioType, Vector3 audioPosition, float volume)
    {
        GameObject newAudio = GameObject.Instantiate(audioObject, audioPosition, Quaternion.identity);
        ControlAudio ca = newAudio.GetComponent<ControlAudio>();
        switch (audioType)
        {
            case (SoundFXCat.Footsteps):
                ca.myClip = Footsteps[Random.Range(0, Footsteps.Length)];
                break;
            case (SoundFXCat.Beakersound):
                ca.myClip = Beakersound[Random.Range(0, Beakersound.Length)];
                break;
            case (SoundFXCat.Drinkingsound):
                ca.myClip = Drinkingsound[Random.Range(0, Drinkingsound.Length)];
                break;
            case (SoundFXCat.Jumping):
                ca.myClip = Jumping[Random.Range(0, Jumping.Length)];
                break;
            case (SoundFXCat.Woodbreaking):
                ca.myClip = Woodbreaking[Random.Range(0, Woodbreaking.Length)];
                break;
        }

        ca.volume = volume;
        ca.StartAudio();
    }
}
