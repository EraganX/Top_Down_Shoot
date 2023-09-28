using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator camShake;
    public AudioSource audioSource;
    public AudioSource missFireAuidio;

   public void CameraShake()
    {
        audioSource.Play();
        camShake.SetTrigger("shake");
    }

    public void MissFire()
    {
        missFireAuidio.Play();
        camShake.SetTrigger("missFire");
    }

}
