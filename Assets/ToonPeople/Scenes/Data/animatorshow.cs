using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPeople
{
    public class animatorshow : MonoBehaviour
    {
        public GameObject[] Males;
        public GameObject[] Females;
        public GameObject phone;
        string[] animations;
        GameObject RandomMale;
        GameObject RandomFemale;
        GameObject RandomElder;
        int animN;
        int elderN;
        int set = 0;
        GameObject newphone1;
        GameObject newphone2;
        GameObject newphone3;
        bool phoneON;
        bool rootON;
        float elderOfset;
        public Texture[] texts;
        public GUIStyle newGUIStyle;
        public bool showUI;
        float Cturn;


        void Start()
        {
            if (transform.childCount > 0) Destroy(transform.GetChild(0).gameObject);
            elderOfset = -0.8f;            
            rootON = false;
            phoneON = false;
            animN = 0;
            animations = new string[30] { "walk1","walk2","walkbackwards","strafeR", "strafeL", "stairsUP" , "stairsDOWN", "run", "runR", "runL", "runbackwards", "runstrafeR",
                                                    "runstrafeL", "runINOUT", "sprint", "brake", "runturn180", "jump", "runjumpIN","freefall","turnR45","turnR90","turnL45","turnL90",
                                                    "turn180","hitforward","fallforwardIN","fallbackwardsIN","crouchIN","pushIN" };

            RandomMale = Instantiate(Males[Random.Range(0, 4)]);
            RandomFemale = Instantiate(Females[Random.Range(0, 4)]);
            RandomElder = Instantiate(Males[Random.Range(0, 4)]);
            RandomMale.transform.position += transform.right * 0.8f;
            RandomElder.transform.position += transform.right * -0.8f;

            RandomMale.GetComponent<TPMalePrefabMaker>().Getready();
            RandomMale.GetComponent<TPMalePrefabMaker>().Randomize();
            RandomFemale.GetComponent<TPFemalePrefabMaker>().Getready();
            RandomFemale.GetComponent<TPFemalePrefabMaker>().Randomize();
            RandomElder.GetComponent<TPMalePrefabMaker>().Getready();
            RandomElder.GetComponent<TPMalePrefabMaker>().Randomize();
            RandomElder.GetComponent<TPMalePrefabMaker>().ElderOn();

            RandomMale.GetComponent<Animator>().applyRootMotion = false;
            RandomFemale.GetComponent<Animator>().applyRootMotion = false;
            RandomElder.GetComponent<Animator>().applyRootMotion = false;

            RandomMale.GetComponent<Playanimation>().enabled = false;
            RandomFemale.GetComponent<Playanimation>().enabled = false;
            RandomElder.GetComponent<Playanimation>().enabled = false;

            RandomMale.GetComponent<Animator>().Play("TPM_" + animations[0]);
            RandomFemale.GetComponent<Animator>().Play("TPF_" + animations[0]);
            RandomElder.GetComponent<Animator>().Play("TPE_" + animations[0]);
            
        }

        void Update()
        {
            if (Input.GetKeyDown("w")) changeset();

            if (Input.GetKeyDown("d"))
            {
                changecharacter();
                animN++;
                changeanimation();
            }
            if (Input.GetKeyDown("a"))
            {
                changecharacter();
                animN--; if (animN < 0) animN = animations.Length - 1;
                changeanimation();
            }
            if (Input.GetKeyDown("space")) changecharacter();
            if (Input.GetKeyDown("r")) activeroot();
            if (Input.GetKeyDown("e")) elderONOFF();
            if (Input.GetKeyDown("x")) showUI = !showUI;

            if (Input.GetKeyDown("left")) { Cturn += 90;  turncharacter(); }
            if (Input.GetKeyDown("right")) { Cturn -= 90; turncharacter(); }

        }

        void OnGUI()
        {
            if (showUI)
            {
                GUI.Label(new Rect(1300, 40, 300, 300), texts[0]);
                GUI.Label(new Rect(320, 120, 256, 256), animations[animN], newGUIStyle);

                if (set == 0) GUI.Label(new Rect(300, 40, 256, 128), texts[1]);
                if (set == 1) GUI.Label(new Rect(300, 40, 256, 128), texts[2]);
                if (set == 2) GUI.Label(new Rect(300, 40, 256, 128), texts[3]);
                if (set == 3) GUI.Label(new Rect(300, 40, 256, 128), texts[4]);
                if (set == 4) GUI.Label(new Rect(300, 40, 256, 128), texts[5]);
            }
        }

        void changeanimation()
        {
            if (animN > animations.Length - 1) animN = 0;
            RandomMale.GetComponent<Playanimation>().enabled = false;
            RandomFemale.GetComponent<Playanimation>().enabled = false;
            RandomElder.GetComponent<Playanimation>().enabled = false;
            RandomMale.GetComponent<Animator>().Play("TPM_" + animations[animN]);
            RandomFemale.GetComponent<Animator>().Play("TPF_" + animations[animN]);
            RandomElder.GetComponent<Animator>().Play("TPE_" + animations[animN]);


        }

        void changeset()
        {
            set++; if (set > 4) set = 0;
            if (set == 0) animations = new string[30] { "walk1","walk2","walkbackwards","strafeR", "strafeL", "stairsUP" , "stairsDOWN", "run", "runR", "runL", "runbackwards", "runstrafeR",
                                                    "runstrafeL", "runINOUT", "sprint", "brake", "runturn180", "jump", "runjumpIN","freefall","turnR45","turnR90","turnL45","turnL90",
                                                    "turn180","hitforward","fallforwardIN","fallbackwardsIN","crouchIN","pushIN" };
            if (set == 1) animations = new string[12] { "idle1","idle2","idle3","idle4", "idle5", "idlehappy" , "idlesad", "idleafraid", "idleangry", "idleamazed", "idleembarrassed",
                                                    "idletired" };
            if (set == 2) animations = new string[17] { "talk1","talk2","clap","wave", "salute1", "salute2" , "laugh", "cry", "telloff", "scream", "sneeze", "grabUP", "grabDOWN", "victory1",
                                                    "victory2", "defeat1", "defeat2" };
            if (set == 3) animations = new string[5] { "lookback", "sitdownIN", "sitidle1", "sitidle2", "sitidle3" };
            if (set == 4) animations = new string[9] { "phoneidle", "phonetalk", "phonesurf", "phonehappy", "phoneangry", "phoneamazed", "phonesitidle", "phonesittalk", "phonesitsurf" };
            if (set == 4 && !phoneON) { addphone(); phoneON = true; }
            if (set != 4 && phoneON) { deletephone(); phoneON = false; }
            animN = 0;
            changeanimation();
        }

        void changecharacter()
        {
            Destroy(RandomMale);
            Destroy(RandomFemale);
            Destroy(RandomElder);

            RandomMale = Instantiate(Males[Random.Range(0, 4)]);
            RandomFemale = Instantiate(Females[Random.Range(0, 4)]);

            if (elderN == 0)
            {
                RandomElder = Instantiate(Males[Random.Range(0, 4)]);
                RandomElder.GetComponent<TPMalePrefabMaker>().Getready();
                RandomElder.GetComponent<TPMalePrefabMaker>().Randomize();
                RandomElder.GetComponent<TPMalePrefabMaker>().ElderOn();
            }

            if (elderN == 1)
            {
                RandomElder = Instantiate(Females[Random.Range(0, 4)]);
                RandomElder.GetComponent<TPFemalePrefabMaker>().Getready();
                RandomElder.GetComponent<TPFemalePrefabMaker>().Randomize();
                RandomElder.GetComponent<TPFemalePrefabMaker>().ElderOn();
            }

            RandomMale.transform.position += transform.right * 0.8f;
            RandomElder.transform.position += transform.right * elderOfset;

            RandomMale.GetComponent<TPMalePrefabMaker>().Getready();
            RandomMale.GetComponent<TPMalePrefabMaker>().Randomize();
            RandomMale.GetComponent<TPMalePrefabMaker>().GlassesOff();

            RandomFemale.GetComponent<TPFemalePrefabMaker>().Getready();
            RandomFemale.GetComponent<TPFemalePrefabMaker>().Randomize();
            RandomFemale.GetComponent<TPFemalePrefabMaker>().GlassesOff();


            RandomMale.GetComponent<Animator>().applyRootMotion = rootON;
            RandomFemale.GetComponent<Animator>().applyRootMotion = rootON;
            RandomElder.GetComponent<Animator>().applyRootMotion = rootON;

            RandomMale.GetComponent<Playanimation>().enabled = false;
            RandomFemale.GetComponent<Playanimation>().enabled = false;
            RandomElder.GetComponent<Playanimation>().enabled = false;
            RandomMale.GetComponent<Animator>().Play("TPM_" + animations[animN]);
            RandomFemale.GetComponent<Animator>().Play("TPF_" + animations[animN]);
            RandomElder.GetComponent<Animator>().Play("TPE_" + animations[animN]);
            if (phoneON) addphone();
            if (elderN == 0) elderN++;
            else elderN = 0;
            turncharacter();
        }

        void turncharacter()
        {
            RandomMale.transform.rotation = Quaternion.Euler( new Vector3(0f, Cturn, 0f));
            RandomFemale.transform.rotation = Quaternion.Euler(new Vector3(0f, Cturn, 0f));
            RandomElder.transform.rotation = Quaternion.Euler(new Vector3(0f, Cturn, 0f));
        }

        void addphone()
        {
            newphone1 = Instantiate(phone);
            newphone1.transform.position = RandomMale.transform.Find("ROOT/TP/TP Pelvis/TP Spine/TP Spine1/TP Spine2/TP R Clavicle/TP R UpperArm/TP R Forearm/TP R Hand").position;
            newphone1.transform.rotation = RandomMale.transform.Find("ROOT/TP/TP Pelvis/TP Spine/TP Spine1/TP Spine2/TP R Clavicle/TP R UpperArm/TP R Forearm/TP R Hand").rotation;
            newphone1.transform.parent = RandomMale.transform.Find("ROOT/TP/TP Pelvis/TP Spine/TP Spine1/TP Spine2/TP R Clavicle/TP R UpperArm/TP R Forearm/TP R Hand").transform;

            newphone2 = Instantiate(phone);
            newphone2.transform.position = RandomFemale.transform.Find("ROOT/TP/TP Pelvis/TP Spine/TP Spine1/TP Spine2/TP R Clavicle/TP R UpperArm/TP R Forearm/TP R Hand").position;
            newphone2.transform.rotation = RandomFemale.transform.Find("ROOT/TP/TP Pelvis/TP Spine/TP Spine1/TP Spine2/TP R Clavicle/TP R UpperArm/TP R Forearm/TP R Hand").rotation;
            newphone2.transform.parent = RandomFemale.transform.Find("ROOT/TP/TP Pelvis/TP Spine/TP Spine1/TP Spine2/TP R Clavicle/TP R UpperArm/TP R Forearm/TP R Hand").transform;

            newphone3 = Instantiate(phone);
            newphone3.transform.position = RandomElder.transform.Find("ROOT/TP/TP Pelvis/TP Spine/TP Spine1/TP Spine2/TP R Clavicle/TP R UpperArm/TP R Forearm/TP R Hand").position;
            newphone3.transform.rotation = RandomElder.transform.Find("ROOT/TP/TP Pelvis/TP Spine/TP Spine1/TP Spine2/TP R Clavicle/TP R UpperArm/TP R Forearm/TP R Hand").rotation;
            newphone3.transform.parent = RandomElder.transform.Find("ROOT/TP/TP Pelvis/TP Spine/TP Spine1/TP Spine2/TP R Clavicle/TP R UpperArm/TP R Forearm/TP R Hand").transform;
        }
        void deletephone()
        {
            Destroy(newphone1);
            Destroy(newphone2);
            Destroy(newphone3);
        }

        void activeroot()
        {
            rootON = !rootON;
            RandomMale.GetComponent<Animator>().applyRootMotion = rootON;
            RandomFemale.GetComponent<Animator>().applyRootMotion = rootON;
            RandomElder.GetComponent<Animator>().applyRootMotion = rootON;
        }

        void elderONOFF()
        {
            if (elderOfset == -0.8f) elderOfset = -3f;
            else elderOfset = -0.8f;
            RandomElder.transform.position = transform.position + transform.right * elderOfset;
            RandomElder.transform.Rotate(Vector3.up * Cturn);

        }
    }
}