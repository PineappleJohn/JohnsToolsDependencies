using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HitSoundsv2 : MonoBehaviour
{
    //public AudioClip[] water, stone, tree, grass, metal, glass, snow, dirt, carpet, wood, sand, concrete, Body, oht, untaggedHitsounds;
    public List<AudioData> audioData = new List<AudioData>();
    public AudioClip slipSound;
    public AudioSource audioSource;
    public bool LeftController;
    private float hapticWaitSeconds = 0.05f;
    //Dictionary<string, AudioClip[]> audio;
    private bool isTouchingSlip = false;

    [System.Serializable]
    public class AudioData
    {
        public AudioClip[] audio = new AudioClip[1];
        public string tagForSound;
        private AudioClip sound;
        private string soundTag;

        public AudioData(AudioClip audio, string tagForSound)
        {
            this.audio[0] = audio;
            this.tagForSound = tagForSound;
        }
    }

    void Start()
    {
        /*audio = new Dictionary<string, AudioClip[]> {
            { "Water", water },
            { "Stone", stone },
            { "Tree", tree },
            { "Grass", grass },
            { "Metal", metal },
            { "Glass", glass },
            { "Snow", snow },
            { "Dirt", dirt },
            { "Carpet", carpet },
            { "Wood", wood },
            { "Sand", sand },
            { "Concrete", concrete},
            { "Body", Body},
            { "OtherHandTag", oht}
        };*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Slip"))
        {
            foreach (var t in audioData)
            {
                if (other.CompareTag(t.tagForSound))
                {
                    audioSource.PlayOneShot(t.audio[Random.Range(0, t.audio.Length)]);
                    StartVibration(LeftController, 0.15f, 0.15f);
                }
            }
        }
        else if (other.gameObject.CompareTag("Slip"))
        {
            PlayLoopingSlipSound();
            isTouchingSlip = true;
        }

        /*else if (other.gameObject.CompareTag("Untagged"))
        {
            PlayRandomSound(untaggedHitsounds, audioSource);
            StartVibration(LeftController, 0.15f, 0.15f);
            StopLoopingSlipSound();
        }
        else if (!other.gameObject.CompareTag("HandTag") && !other.gameObject.CompareTag("Player"))
        {
            PlayRandomSound(audio[other.gameObject.tag], audioSource);
            StartVibration(LeftController, 0.15f, 0.15f);
            StopLoopingSlipSound();
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Slip"))
        {
            StopLoopingSlipSound();
            isTouchingSlip = false;
        }
    }

    void PlayRandomSound(AudioClip[] audioClips, AudioSource audioSource)
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }

    void PlayLoopingSlipSound()
    {
        if (!isTouchingSlip)
        {
            audioSource.clip = slipSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void StopLoopingSlipSound()
    {
        if (isTouchingSlip)
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
    }

    public void StartVibration(bool forLeftController, float amplitude, float duration)
    {
        StartCoroutine(HapticPulses(forLeftController, amplitude, duration));
    }

    private IEnumerator HapticPulses(bool forLeftController, float amplitude, float duration)
    {
        float startTime = Time.time;
        uint channel = 0u;
        InputDevice device = ((!forLeftController) ? InputDevices.GetDeviceAtXRNode(XRNode.RightHand) : InputDevices.GetDeviceAtXRNode(XRNode.LeftHand));
        while (Time.time < startTime + duration)
        {
            device.SendHapticImpulse(channel, amplitude, hapticWaitSeconds);
            yield return new WaitForSeconds(hapticWaitSeconds * 0.9f);
        }
    }
}
