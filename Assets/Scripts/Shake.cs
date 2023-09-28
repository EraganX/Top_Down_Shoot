using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator camShake;

   public void CameraShake()
    {
        camShake.SetTrigger("shake");
    }

    public void MissFire()
    {
        camShake.SetTrigger("missFire");
    }

}
