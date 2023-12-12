using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : APickup
{
    [SerializeField] private Player player;

    protected override void Drop()
    {
        this.transform.SetParent(null);
    }

    protected override void Pickup()
    {
        this.transform.SetParent(player.transform);
        this.transform.localPosition = Vector3.up;
    }
}
