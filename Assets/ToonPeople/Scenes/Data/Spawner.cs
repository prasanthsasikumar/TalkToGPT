using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPeople
{
    public class Spawner : MonoBehaviour
    {

        public GameObject[] characters;
        float randomTime;
        float timeCounter;
        public float deviation;

        void Update()
        {
            if (timeCounter > randomTime)
            {
                GameObject newchar = Instantiate(characters[Random.Range(0, 8)], transform.position + (transform.right * Random.Range(-1f, 1f)), transform.rotation * Quaternion.Euler(Vector3.up * Random.Range(-deviation, deviation)));

                if (newchar.GetComponent<TPMalePrefabMaker>() != null)
                {
                    newchar.GetComponent<TPMalePrefabMaker>().Getready();
                    newchar.GetComponent<TPMalePrefabMaker>().Randomize();
                    newchar.GetComponent<Playanimation>().anim = ("TPM_walk1");
                    float cscale = Random.Range(1f, 1.1f);
                    newchar.transform.localScale = new Vector3(cscale, cscale, cscale);
                }
                if (newchar.GetComponent<TPFemalePrefabMaker>() != null)
                {
                    newchar.GetComponent<TPFemalePrefabMaker>().Getready();
                    newchar.GetComponent<TPFemalePrefabMaker>().Randomize();
                    newchar.GetComponent<Playanimation>().anim = ("TPF_walk1");
                    float cscale = Random.Range(0.95f, 1f);
                    newchar.transform.localScale = new Vector3(cscale, cscale, cscale);
                }
                newchar.GetComponent<Animator>().applyRootMotion = true;

                randomTime = Random.Range(1, 4);
                timeCounter = 0f;
            }
            timeCounter += Time.deltaTime;
        }
    }
}