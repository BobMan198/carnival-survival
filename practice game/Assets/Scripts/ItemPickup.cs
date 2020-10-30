using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemPickup : MonoBehaviourPunCallbacks
{
    public GunInfo weapon;

    private void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody.gameObject.tag.Equals("Player"))
        {
            PlayerController playerController = other.attachedRigidbody.gameObject.GetComponent<PlayerController>();
        }
    }
}
