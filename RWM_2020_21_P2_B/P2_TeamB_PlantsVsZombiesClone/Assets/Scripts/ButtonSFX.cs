using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    public AudioSource mySfx;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    public void HoverSound()
    {
        mySfx.PlayOneShot(hoverFx);
    }
    public void ClickSound()
    {
        mySfx.PlayOneShot(clickFx);
    }
}
