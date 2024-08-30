using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonTransformView))]

public class NetworkMethod : MonoBehaviourPun, IPunObservable
{
    //This networks items without needing to use PhotonNetwork.Instantiate, 
    //If this gameobject has a rigidbody, add photon rigidbody view and tick on Synchronize Angular Velocity
    //no creds needed, just don't claim as ur own - kosher
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //credits to kevinboy160 :)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.eulerAngles);
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.eulerAngles = (Vector3)stream.ReceiveNext();
        }
    }
}
