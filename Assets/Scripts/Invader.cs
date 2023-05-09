using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour {
    public System.Action killed;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Laser")))
        {
            killed.Invoke();
            gameObject.SetActive(false);
        }
    }
}