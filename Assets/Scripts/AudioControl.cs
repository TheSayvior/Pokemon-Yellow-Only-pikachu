using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour {

    public AudioSource StartMusic;
    public AudioSource ScaryMusic;
    public AudioSource Flickering;
    public AudioSource MetalDoor;
    public AudioSource Dragging;
    public AudioSource ScareSound;
    public AudioSource PianoTune;

    public void StartPiano()
    {
        PianoTune.Play();
    }

    public void StartScare()
    {
        ScareSound.Play();
    }

    public void StopDragging()
    {
        Dragging.Stop();
    }

    public void StartDragging()
    {
        Dragging.Play();
    }

    public void StopStartMusic()
    {
        StartMusic.Stop();
    }

    public void StartScaryMusic()
    {
        ScaryMusic.Play();
    }

    public void PlayFlickering()
    {
        Flickering.Play();
    }

    public void StopFlickering()
    {
        Flickering.Stop();
    }

    public void StartMetalDoor()
    {
        MetalDoor.Play();
    }
}
