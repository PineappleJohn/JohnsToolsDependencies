using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(MeshRenderer))]
public class MonkeButton : MonoBehaviour
{
    private GameObject Object;
    private Material red;
    private Material white;
    private AudioSource sound;
    private string HandTag = "HandTag";

    private void Awake()
    {
        white = gameObject.GetComponent<Renderer>().material;
        Object = gameObject;
        GameObject go = GameObject.FindGameObjectWithTag("redMat");
        red = go.GetComponent<Renderer>().material;
        sound = gameObject.GetComponent<AudioSource>();
        sound.clip = go.GetComponent<AudioSource>().clip;
        sound.volume = go.GetComponent<AudioSource>().volume;
        sound.playOnAwake = false;
        sound.loop = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(HandTag))
        {
            Object.GetComponent<Renderer>().material = red;
            if (sound != null)
            {
                sound.Play();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(HandTag))
        {
            Object.GetComponent<Renderer>().material = white;
            if (sound != null)
            {
                sound.Stop();
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(MonkeButton))]
    public class ButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            MonkeButton script = (MonkeButton)target;
            GUILayout.Space(5);
            GUILayout.Label("Made by john;");
            GUILayout.Space(5);
            if (GUILayout.Button("How to use"))
            {
                Application.OpenURL("https://pastebin.com/raw/bz7XyPf2");
                Debug.Log("Opened pastebin");
            }
        }
    }
#endif
}