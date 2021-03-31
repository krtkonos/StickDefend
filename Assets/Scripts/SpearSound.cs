using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSound : MonoBehaviour
{
    public AudioClip stab;
    public AudioSource _stab;
    public AudioClip footStep;
    public AudioSource _footStep;

    private void Stab()
    {
        _stab.PlayOneShot(stab);
    }
    private void FootStep()
    {
        _footStep.PlayOneShot(footStep);
    }
}
