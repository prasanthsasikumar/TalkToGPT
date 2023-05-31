using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPeople
{
    public class blendshapesshow : MonoBehaviour
    {
        public GameObject[] Heads;
        GameObject[] GOHeads;
        float time;
        public float timelapse;
        public int blendshapesnumber;
        int counter;
        public GUIStyle newGUIStyle;
        public bool showUI;
        string[] names = new string[31] { "","browR_DOWN","browL_DOWN","browR_UP","browL_UP", "browR_SAD", "browL_SAD" ,
                                      "smile", "sad", "disgust", "Open", "M", "E", "U", "F",
                                      "lipUP_UP", "lipDOWN_DOWN", "jawUP",
                                      "blinkR", "blinkL","wideR","wideL", "lidRDOWN_UP","lidLDOWN_UP",
                                      "look_UP", "look_DOWN","look_RIGHT","look_LEFT","look_cross","look_crazy","elder" };

        void Start()
        {
            time = 0f;
            counter = 0;
            GOHeads = new GameObject[Heads.Length];
            GameObject[] GOAUX = new GameObject[Heads.Length];
            for (int forAUX = 0; forAUX < Heads.Length; forAUX++)
            {
                GOAUX[forAUX] = Instantiate(Heads[forAUX], transform.position + transform.right * (forAUX - 4.5f) * -0.25f, transform.rotation, transform);
                foreach (Renderer compAUX in GOAUX[forAUX].transform.GetComponentsInChildren<Renderer>())
                {
                    compAUX.gameObject.SetActive(false);
                }
                GOHeads[forAUX] = GOAUX[forAUX].transform.Find("HEAD").gameObject;
                GOHeads[forAUX].SetActive(true);
            }
            for (int forAUX = 0; forAUX < Heads.Length; forAUX++)
            {
                if (forAUX < (Heads.Length / 2)) GOAUX[forAUX].transform.position = transform.position + (transform.right * (forAUX - 1.5f) * -0.3f);
                if (forAUX > (Heads.Length / 2) - 1) GOAUX[forAUX].transform.position = transform.position + (transform.right * (forAUX - 5.5f) * -0.3f) + (transform.up * -0.5f);
            }
        }

        void Update()
        {
            time += Time.deltaTime;
            if (time > timelapse && counter < blendshapesnumber)
            {
                for (int forAUX = 0; forAUX < Heads.Length; forAUX++)
                {
                    StartCoroutine(ToBS(forAUX, counter));
                }
                counter++;
                time = 0f;
            }
            if (time > timelapse && counter == blendshapesnumber)
            {
                for (int forAUX = 0; forAUX < Heads.Length; forAUX++)
                {
                    GOHeads[forAUX].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(blendshapesnumber - 1, 0f);
                }
                UnityEditor.EditorApplication.isPlaying = false;

                Application.Quit();
            }
        }

        void OnGUI()
        {
            if (showUI)
            {
                GUI.Label(new Rect(800, 300, 300, 300), names[counter], newGUIStyle);

            }
        }

        IEnumerator ToBS(int headN, int BSN)
        {
            float fase = 0;
            while (fase < 100f)
            {
                GOHeads[headN].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(BSN, fase);
                fase += 750f * Time.deltaTime;
                yield return false;
            }
            GOHeads[headN].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(BSN, 100f);
            yield return new WaitForSeconds(0.5f);

            while (fase > 0f)
            {
                GOHeads[headN].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(BSN, fase);

                fase -= 750f * Time.deltaTime;
                yield return false;
            }
            GOHeads[headN].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(BSN, 0f);
            yield return false;

        }
    }
}