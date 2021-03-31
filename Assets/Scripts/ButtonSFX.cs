using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    [SerializeField] private AudioSource playerChop;
  
    public void PlayPlayerChop()
    {
        playerChop.Play();
    }
}
