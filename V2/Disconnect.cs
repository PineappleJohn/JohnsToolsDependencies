using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Disconnect : MonoBehaviour
{
    bool useCollider;

    private void Awake()
    {
        if (gameObject.GetComponent<Collider>().isTrigger)
            useCollider = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HandTag") && useCollider)
        {
            LeaveRoom();
        }
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.Disconnect();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Disconnect))]
    public class DisconnectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Disconnect script = (Disconnect)target;
            GUILayout.Label("Made by john;");
            if (GUILayout.Button("Disconnect"))
                script.LeaveRoom();
        }
    }
#endif
}