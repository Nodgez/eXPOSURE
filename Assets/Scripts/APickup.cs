using UnityEngine;
using System;
//An abstract class that handles the effects of an object being picked up by an actor
public abstract class APickup : MonoBehaviour
{
    protected abstract void Pickup();
    protected abstract void Drop();

    private void OnTriggerEnter(Collider other)
    {
        print($"Pickup {name} triggered");
        Pickup();
    }
}
