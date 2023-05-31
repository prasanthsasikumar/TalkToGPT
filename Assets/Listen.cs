using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPeople;

public class Listen : MonoBehaviour
{
    public string talkAnim, listenAnim;

    public GameObject charector;

    // Update is called once per frame
    void Update()
    {
        //On spacebar press, call 
        if (Input.GetKeyDown(KeyCode.T))
        {
            charector.GetComponent<Playanimation>().playtheanimation(talkAnim);
        } 
        else if (Input.GetKeyUp(KeyCode.L))
        {
            charector.GetComponent<Playanimation>().playtheanimation(listenAnim);
        }
        
    }
}
