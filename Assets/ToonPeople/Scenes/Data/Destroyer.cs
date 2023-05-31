using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPeople
{
    public class Destroyer : MonoBehaviour
    {

        void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}
