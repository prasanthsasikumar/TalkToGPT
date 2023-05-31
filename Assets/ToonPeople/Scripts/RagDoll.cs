using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPeople
{
    public class RagDoll : MonoBehaviour
    {

        float time;
        float counter;
        bool dead; 

        void Start()
        {
            time = Random.Range(1f, 4f);
        }

        void Update()
        {
            counter += Time.deltaTime;
            if (!dead)
            {
                if (counter > time)
                {
                    counter = -4f;
                    GetComponent<Animator>().SetLayerWeight(6, 1f);
                    dead = true;
                }
                if (Input.GetKeyDown("s")) GetComponent<Animator>().enabled = false;
            }
            else
            {
                if (counter > time)
                {
                    counter = -4f;
                    GetComponent<Animator>().SetLayerWeight(6, 0f);
                    dead = false;
                }
                if (Input.GetKeyDown("w")) GetComponent<Animator>().enabled = true;
            }
        }
        private void LateUpdate()
        {
            if (dead) GetComponent<Animator>().enabled = false;
            else GetComponent<Animator>().enabled = true;
        }
    }
}
