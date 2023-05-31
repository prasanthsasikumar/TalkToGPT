using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]

public class TPFemalePrefabMaker : MonoBehaviour

    {
    public bool allOptions;
    int hair;
    int chest;
    int legs;
    int feet;
    int tie;
    int jacket;
    public bool tieactive;
    public bool tieactivecolor;
    public bool glassesactive;
    public bool jacketactive;
    public bool hatactive;
    public bool legsactive;
    public bool haircoloractive;
    GameObject GOhead;
    GameObject GOheadsimple;
    GameObject[] GOfeet;
    GameObject[] GOhair;
    GameObject[] GOchest;
    GameObject[] GOlegs;
    GameObject GOglasses;
    GameObject[] GOjackets;
    GameObject[] GOties;
    public Object[] MATSkins;
    public Object[] MATElderSkins;
    public Object[] MAThairA;
    public Object[] MAThairB;
    public Object[] MAThairC;
    public Object[] MAThairD;
    public Object[] MAThairE;
    public Object[] MAThairF;
    public Object[] MAThairG;
    public Object[] MATGlasses;
    public Object[] MATDress;
    public Object[] MATTshirt;
    public Object[] MATShirtA;
    public Object[] MATShirtB;
    public Object[] MATEyes;
    public Object[] MATJacket;
    public Object[] MATSweater;
    public Object[] MATLegs;
    public Object[] MATFeetA;
    public Object[] MATFeetB;
    public Object[] MATFeetC;
    public Object[] MATHatA;
    public Object[] MATHatB;
    public Object[] MATHatC;
    public Object[] MATBowtie;
    public Object[] MATTie;
    public Object[] MATteeth;    
    public bool elder;
    Material headskin;

    void Start()
    {
        allOptions = false;
    }
    void LateUpdate()
    {
        if (elder)
        {
            SkinnedMeshRenderer rendhead;
            GOhead = transform.Find("HEAD").gameObject as GameObject;
            rendhead = GOhead.GetComponent<SkinnedMeshRenderer>();
            rendhead.SetBlendShapeWeight(29, 100);            
        }
    }
    public void Getready()

    {
        //load models
        GOhead = transform.Find("HEAD").gameObject as GameObject;
        GOheadsimple = transform.Find("HEADsimple").gameObject as GameObject;
        if (GOheadsimple == null) GOheadsimple = transform.Find("HEADelder").gameObject as GameObject;
        GOheadsimple.SetActive(false);

        GOhair = new GameObject[10];
        GOchest = new GameObject[10];
        GOlegs = new GameObject[5];
        GOfeet = new GameObject[4];
        GOjackets = new GameObject[2];
        GOties = new GameObject[3];

        string[] hairnames = new string[10] { "TPFHairA", "TPFHairB", "TPFHairC", "TPFHairD", "TPFHairE", "TPFHairF", "TPFHairG", "TPFHatA", "TPFHatB", "TPFHatC" };
        string[] chestnames = new string[10] { "CHEST","TPFDressL","TPFDressS", "TPFShirtAL", "TPFShirtAS", "TPFShirtBL", "TPFShirtBS", "TPFTshirtB", "TPFTshirtL", "TPFTshirtS" };
        string[] legnames = new string[5] { "LEGS","TPFLeggins", "TPFLegsL", "TPFLegsS","TPFSkirt" };
        string[] feetnames = new string[4] { "FEET", "TPFFeetA", "TPFFeetB", "TPFFeetC" };
        string[] jacketnames = new string[2] { "TPFSuit", "TPFSweater" };
        string[] tiesnames = new string[3] { "TPFBowtie", "TPFTieL", "TPFTieS" };

        for (int forAUX = 0; forAUX < 10; forAUX++) GOhair[forAUX] = transform.Find(hairnames[forAUX]).gameObject as GameObject;
        for (int forAUX = 0; forAUX < 10; forAUX++) GOchest[forAUX] = transform.Find(chestnames[forAUX]).gameObject as GameObject;
        for (int forAUX = 0; forAUX < 5; forAUX++)  GOlegs[forAUX] = transform.Find(legnames[forAUX]).gameObject as GameObject;
        for (int forAUX = 0; forAUX < 4; forAUX++)  GOfeet[forAUX] = transform.Find(feetnames[forAUX]).gameObject as GameObject;
        for (int forAUX = 0; forAUX < 2; forAUX++)  GOjackets[forAUX] = transform.Find(jacketnames[forAUX]).gameObject as GameObject;
        for (int forAUX = 0; forAUX < 3; forAUX++)  GOties[forAUX] = transform.Find(tiesnames[forAUX]).gameObject as GameObject;

        GOglasses = transform.Find("ROOT/TP/TP Pelvis/TP Spine/TP Spine1/TP Spine2/TP Neck/TP Head/Glasses").gameObject as GameObject;
                
        if (GOfeet[0].activeSelf && GOfeet[1].activeSelf && GOfeet[2].activeSelf)
        {
            Randomize();
            elder = false;
            haircoloractive = true;
        }
        else
        {
            while (!GOhair[hair].activeSelf) hair++;if (hair > 6) hatactive = true;
            else hatactive = false;
            while (!GOchest[chest].activeSelf) chest++;
            if (chest != 1) while (!GOlegs[legs].activeSelf) legs++;
            while (!GOfeet[feet].activeSelf) feet++;
            if (GOjackets[0].activeSelf) jacket = 0; if (GOjackets[1].activeSelf) jacket = 1;
            if (!GOjackets[0].activeSelf && !GOjackets[1].activeSelf) jacket = 2;
            tie = 3;
            for (int forAUX = 0; forAUX < 3; forAUX++)
            {
                if (GOties[forAUX].activeSelf) tie = forAUX;
            }
            if (!GOties[0].activeSelf && !GOties[1].activeSelf && !GOties[2].activeSelf) { tieactive = false; tieactivecolor = false; }
            if (GOglasses.activeSelf) glassesactive = true;
            Checkties();
            Checklegs();
            Checkelder();
        }
    }
    void ResetSkin()
    {
        string[] allskins = new string[8] { "TPFemaleA", "TPFemaleB", "TPFemaleC", "TPFemaleD", "TP_E_FemaleA", "TP_E_FemaleB", "TP_E_FemaleC", "TP_E_FemaleD" };
        Material[] AUXmaterials;
        int materialcount;
        AUXmaterials = GOhead.GetComponent<Renderer>().sharedMaterials;
        materialcount = GOhead.GetComponent<Renderer>().sharedMaterials.Length;
        for (int forAUX2 = 0; forAUX2 < materialcount; forAUX2++)
            for (int forAUX3 = 0; forAUX3 < allskins.Length; forAUX3++)
                for (int forAUX4 = 1; forAUX4 < MATSkins.Length + 1; forAUX4++)
                {
                    if (AUXmaterials[forAUX2].name == allskins[forAUX3] + "0" + forAUX4 || AUXmaterials[forAUX2].name == allskins[forAUX3] + forAUX4)
                    {
                        headskin = AUXmaterials[forAUX2];
                    }
                }
        //legs
        for (int forAUX = 0; forAUX < GOlegs.Length; forAUX++)
        {
            AUXmaterials = GOlegs[forAUX].GetComponent<Renderer>().sharedMaterials;
            materialcount = GOlegs[forAUX].GetComponent<Renderer>().sharedMaterials.Length;
            for (int forAUX2 = 0; forAUX2 < materialcount; forAUX2++)
                for (int forAUX3 = 0; forAUX3 < 4; forAUX3++)
                    for (int forAUX4 = 1; forAUX4 < MATSkins.Length + 1; forAUX4++)
                    {
                        if (AUXmaterials[forAUX2].name == allskins[forAUX3] + "0" + forAUX4 || AUXmaterials[forAUX2].name == allskins[forAUX3] + forAUX4)
                        {
                            AUXmaterials[forAUX2] = headskin;
                            GOlegs[forAUX].GetComponent<Renderer>().sharedMaterials = AUXmaterials;
                        }
                    }
        }
        //chest
        for (int forAUX = 0; forAUX < GOchest.Length; forAUX++)
        {
            AUXmaterials = GOchest[forAUX].GetComponent<Renderer>().sharedMaterials;
            materialcount = GOchest[forAUX].GetComponent<Renderer>().sharedMaterials.Length;
            for (int forAUX2 = 0; forAUX2 < materialcount; forAUX2++)
                for (int forAUX3 = 0; forAUX3 < 4; forAUX3++)
                    for (int forAUX4 = 1; forAUX4 < MATSkins.Length + 1; forAUX4++)
                    {
                        if (AUXmaterials[forAUX2].name == allskins[forAUX3] + "0" + forAUX4 || AUXmaterials[forAUX2].name == allskins[forAUX3] + forAUX4)
                        {
                            AUXmaterials[forAUX2] = headskin;
                            GOchest[forAUX].GetComponent<Renderer>().sharedMaterials = AUXmaterials;
                        }
                    }
        }
        //feet
        for (int forAUX = 0; forAUX < GOfeet.Length; forAUX++)
        {
            AUXmaterials = GOfeet[forAUX].GetComponent<Renderer>().sharedMaterials;
            materialcount = GOfeet[forAUX].GetComponent<Renderer>().sharedMaterials.Length;
            for (int forAUX2 = 0; forAUX2 < materialcount; forAUX2++)
                for (int forAUX3 = 0; forAUX3 < 4; forAUX3++)
                    for (int forAUX4 = 1; forAUX4 < MATSkins.Length + 1; forAUX4++)
                    {
                        if (AUXmaterials[forAUX2].name == allskins[forAUX3] + "0" + forAUX4 || AUXmaterials[forAUX2].name == allskins[forAUX3] + forAUX4)
                        {
                            AUXmaterials[forAUX2] = headskin;
                            GOfeet[forAUX].GetComponent<Renderer>().sharedMaterials = AUXmaterials;
                        }
                    }
        }
    }
    public void Deactivateall()
    {
        for (int forAUX = 0; forAUX < GOhair.Length; forAUX++) GOhair[forAUX].SetActive(false);
        for (int forAUX = 0; forAUX < GOchest.Length; forAUX++) GOchest[forAUX].SetActive(false);
        for (int forAUX = 0; forAUX < GOlegs.Length; forAUX++) GOlegs[forAUX].SetActive(false);
        for (int forAUX = 0; forAUX < GOfeet.Length; forAUX++) GOfeet[forAUX].SetActive(false);
        for (int forAUX = 0; forAUX < GOjackets.Length; forAUX++) GOjackets[forAUX].SetActive(false);
        for (int forAUX = 0; forAUX < GOties.Length; forAUX++) GOties[forAUX].SetActive(false);
        GOglasses.SetActive(false);
        glassesactive = false;
        jacketactive = false;
        tieactivecolor = false;
        tieactive = false;
        tieactivecolor = false;
        hatactive = false;
    }
    public void Activateall()
    {
        for (int forAUX = 0; forAUX < GOhair.Length; forAUX++) GOhair[forAUX].SetActive(true);
        for (int forAUX = 0; forAUX < GOchest.Length; forAUX++) GOchest[forAUX].SetActive(true);
        for (int forAUX = 0; forAUX < GOlegs.Length; forAUX++) GOlegs[forAUX].SetActive(true);
        for (int forAUX = 0; forAUX < GOfeet.Length; forAUX++) GOfeet[forAUX].SetActive(true);
        for (int forAUX = 0; forAUX < GOjackets.Length; forAUX++) GOjackets[forAUX].SetActive(true);
        for (int forAUX = 0; forAUX < GOties.Length; forAUX++) GOties[forAUX].SetActive(true);
        GOglasses.SetActive(true);
    }
    public void Menu()
    {
        allOptions = !allOptions;
    }
    public void Checklegs()
    {
        if (chest ==1)
        {
            legsactive = false;
            GOlegs[legs].SetActive(false);
        }
        else
        {
            legsactive = true;
            GOlegs[legs].SetActive(true);
        }
    }
    public void Checkties()
    {
        if (chest ==3 || chest ==4)
        {
            tieactive = true;
            if (tie != 3)
            {
                GOties[tie].SetActive(true);
                tieactivecolor = true;
            }
            else tieactivecolor = false;
        }
        else
        {
            if (tie != 3) GOties[tie].SetActive(false);
            tieactive = false;
            tieactivecolor = false;
        }
    }
    void Checkelder()
    {
        Material[] AUXmaterials;
        elder = false;
        haircoloractive = true;
        AUXmaterials = GOhead.GetComponent<Renderer>().sharedMaterials;
        int materialcount = GOhead.GetComponent<Renderer>().sharedMaterials.Length;
        for (int forAUX = 0; forAUX < materialcount; forAUX++)
        {
            if (AUXmaterials[forAUX].name == MATteeth[1].name)
            {
                elder = true;
                haircoloractive = false;
            }            
        }
    }

    //models
    public void Nexthat()
    {
        hatactive = true;
        if (hair < 7)
        {
            GOhair[hair].SetActive(false);
            hair = 7;
            GOhair[hair].SetActive(true);
        }
        else
        {
            GOhair[hair].SetActive(false);
            hair++;
            if (hair > GOhair.Length-1) hair = 7;          
            GOhair[hair].SetActive(true);
        }
    }
    public void Prevhat()
    {
        hatactive = true;
        if (hair < 7)
        {
            GOhair[hair].SetActive(false);
            hair = 9;
            GOhair[hair].SetActive(true);
        }
        else
        {
            GOhair[hair].SetActive(false);
            hair--;
            if (hair < 7) hair = 9;
            GOhair[hair].SetActive(true);
        }
    }
    public void Nexthair()
    {
        hatactive = false;
        GOhair[hair].SetActive(false);
        if (hair < GOhair.Length - 4) hair++;
        else hair = 0;
        GOhair[hair].SetActive(true);
    }
    public void GlassesOn()
    {
        glassesactive = !glassesactive;
        GOglasses.SetActive(glassesactive);
    }
    public void GlassesOff()
    {
        glassesactive = false;
        GOglasses.SetActive(glassesactive);
    }
    public void Nextchest()
    {
        GOchest[chest].SetActive(false);
        if (chest < GOchest.Length - 1) chest++;
        else chest = 0;
        GOchest[chest].SetActive(true);
        Checkties();
        Checklegs();
    }
    public void Nextlegs()
    {
        GOlegs[legs].SetActive(false);
        if (legs < GOlegs.Length - 1) legs++;
        else legs = 0;
        GOlegs[legs].SetActive(true);
    }
    public void Nextfeet()
    {
        GOfeet[feet].SetActive(false);
        if (feet < GOfeet.Length - 1) feet++;
        else feet = 0;
        GOfeet[feet].SetActive(true);
    }
    public void Nexttie()
    {
        if (tie != 3) GOties[tie].SetActive(false);
        if (tie < GOties.Length) tie++;
        else tie = 0;
        if (tie != 3) GOties[tie].SetActive(true);
        if (tie == 3) tieactivecolor = false;
        else tieactivecolor = true;
    }
    public void Nextjacket()
    {
        if (jacket == 2)
        {
            jacket = 0;
            GOjackets[jacket].SetActive(true);
            jacketactive = true;
        }
        else
        {
            if (jacket == 1)
            {
                GOjackets[jacket].SetActive(false);
                jacket = 2;
                jacketactive = false;
            }
            if (jacket == 0)
            {
                GOjackets[jacket].SetActive(false);
                jacket = 1;
                GOjackets[jacket].SetActive(true);
            }
        }
    }
    public void Prevhair()
    {
        hatactive = false;
        GOhair[hair].SetActive(false);
        if (hair > 0) hair--;
        else hair = 6;
        GOhair[hair].SetActive(true);
    }
    public void Prevchest()
    {
        GOchest[chest].SetActive(false);
        chest--;
        if (chest < 0) chest = GOchest.Length - 1;
        GOchest[chest].SetActive(true);
        Checkties();
        Checklegs();
    }
    public void Prevlegs()
    {
        GOlegs[legs].SetActive(false);
        if (legs > 0) legs--;
        else legs = GOlegs.Length - 1;
        GOlegs[legs].SetActive(true);
    }
    public void Prevfeet()
    {
        GOfeet[feet].SetActive(false);
        if (feet > 0) feet--;
        else feet = GOfeet.Length - 1;
        GOfeet[feet].SetActive(true);
    }
    public void Prevtie()
    {
        if (tie != 3) GOties[tie].SetActive(false);
        tie--;
        if (tie < 0) tie = 3;
        if (tie != 3) GOties[tie].SetActive(true);
        if (tie == 3) tieactivecolor = false;
        else tieactivecolor = true;
    }
    public void Prevjacket()
    {
        if (jacket == 0)
        {
            GOjackets[jacket].SetActive(false);
            jacket = 2;
            jacketactive = false;
        }
        else
        {
            if (jacket == 1)
            {
                GOjackets[jacket].SetActive(false);
                jacket = 0;
                GOjackets[jacket].SetActive(true);
            }
            if (jacket == 2)
            {
                jacket = 1;
                jacketactive = true;
                GOjackets[jacket].SetActive(true);
            }
        }
    }
    
    //materials    
    public void Nexthatcolor(int todo)
    {
        if (hatactive)
        {
            if (hair == 7) ChangeMaterials(MATHatA, todo);
            if (hair == 8) ChangeMaterials(MATHatB, todo);
            if (hair == 9) ChangeMaterials(MATHatC, todo);
        }
    }
    public void Nextskincolor(int todo)
    {
        ChangeMaterials(MATSkins, todo);
        ChangeMaterials(MATElderSkins, todo);
    }
    public void Nexthaircolor(int todo)
    {
        if (!elder)
        {
            int intindex = 0;
            Material AUXmaterial;
            AUXmaterial = GOhair[0].GetComponent<Renderer>().sharedMaterial;
            while (AUXmaterial.name != MAThairA[intindex].name) intindex++;
            if (intindex == 2 && todo == 0) todo = 3;
            if (intindex == 0 && todo == 1) todo = 4;
            ChangeMaterials(MAThairA, todo);
            ChangeMaterials(MAThairB, todo);
            ChangeMaterials(MAThairC, todo);
            ChangeMaterials(MAThairD, todo);
            ChangeMaterials(MAThairE, todo);
            ChangeMaterials(MAThairF, todo);
            ChangeMaterials(MAThairG, todo);
        }
    }
    public void Nextglasses(int todo)
    {
        ChangeMaterials(MATGlasses, todo);
    }
    public void Nexteyescolor(int todo)
    {
        ChangeMaterials(MATEyes, todo);
    }
    public void Nextchestcolor(int todo)
    {
        if (chest < 3) ChangeMaterial(GOchest[1], MATDress, todo);
        if (chest < 3) ChangeMaterial(GOchest[2], MATDress, todo);
        if (chest ==3 || chest == 4) ChangeMaterials(MATShirtA, todo);
        if (chest == 5 || chest == 6) ChangeMaterials(MATShirtB, todo); 
        if (chest > 6) ChangeMaterials(MATTshirt, todo); 
    }
    public void Nextjacketcolor(int todo)
    {
        if (jacket == 0) ChangeMaterials(MATJacket, todo);
        if (jacket == 1) ChangeMaterials(MATSweater, todo);
    }
    public void Nextlegscolor(int todo)
    {
        ChangeMaterials(MATLegs, todo);
        ChangeMaterial(GOlegs[4], MATDress, todo);
    }
    public void Nextfeetcolor(int todo)
    {
        if (feet == 1) ChangeMaterials(MATFeetA, todo);
        if (feet == 2) ChangeMaterials(MATFeetB, todo);
        if (feet == 3) ChangeMaterials(MATFeetC, todo);
    }
    public void Nexttiecolor(int todo)
    {
        if (tie == 0) ChangeMaterials(MATBowtie, todo);
        if (tie > 0) ChangeMaterials(MATTie, todo);
    }
         

    public void ResetModel()
    {
            ElderOff();
            Activateall();
            ChangeMaterials(MATHatA, 3);
            ChangeMaterials(MATHatB, 3);
            ChangeMaterials(MATHatC, 3);
            ChangeMaterials(MATSkins, 3);
            ChangeMaterials(MAThairA, 3);
            ChangeMaterials(MAThairB, 3);
            ChangeMaterials(MAThairC, 3);
            ChangeMaterials(MAThairD, 3);
            ChangeMaterials(MAThairE, 3);
            ChangeMaterials(MAThairF, 3);
            ChangeMaterials(MAThairG, 3);
            ChangeMaterials(MATGlasses, 3);
            ChangeMaterials(MATEyes, 3);
            ChangeMaterials(MATShirtA, 3);
            ChangeMaterials(MATShirtB, 3);
            ChangeMaterials(MATTshirt, 3);
            ChangeMaterials(MATDress, 3);
            ChangeMaterials(MATJacket, 3);
            ChangeMaterials(MATSweater, 3);
            ChangeMaterials(MATLegs, 3);
            ChangeMaterials(MATFeetA, 3);
            ChangeMaterials(MATFeetB, 3);
            ChangeMaterials(MATBowtie, 3);
            ChangeMaterials(MATTie, 3);
            ChangeMaterials(MATteeth, 3);
            Menu();
    }
    public void Randomize()
    {
        Deactivateall();
        ResetSkin();
        Checkelder();
        hair = Random.Range(0, 15);
        if (hair > 9) hair = Random.Range(0, 5);
        GOhair[hair].SetActive(true);
        if (hair > 5) hatactive = true;
        chest = Random.Range(1, GOchest.Length); GOchest[chest].SetActive(true);
        tie = Random.Range(0, 4);
        Checkties();
        legs = Random.Range(1, GOlegs.Length); GOlegs[legs].SetActive(true);
        Checklegs();
        feet = Random.Range(1, GOfeet.Length); GOfeet[feet].SetActive(true);
        jacket = Random.Range(0, 6);
        if (jacket < 2)
        {
            jacketactive = true;
            GOjackets[jacket].SetActive(true);
        }
        else jacketactive = false;
        if (Random.Range(0, 4) > 2)
        {
            glassesactive = true;
            GOglasses.SetActive(true);
            ChangeMaterial(GOglasses, MATGlasses, 2);
        }
        else glassesactive = false;

        //materials
        ChangeMaterial(GOhead, MATEyes, 2);
        if (tieactivecolor) for (int forAUX2 = 0; forAUX2 < (Random.Range(0, 10)); forAUX2++) Nexttiecolor(0);
        for (int forAUX2 = 0; forAUX2 < (Random.Range(0, 8)); forAUX2++) Nexthaircolor(0);
        for (int forAUX2 = 0; forAUX2 < (Random.Range(0, 32)); forAUX2++) Nextskincolor(0);
        for (int forAUX2 = 0; forAUX2 < (Random.Range(0, 26)); forAUX2++) Nextfeetcolor(0);
        for (int forAUX2 = 0; forAUX2 < (Random.Range(0, 26)); forAUX2++) Nextjacketcolor(0);
        for (int forAUX2 = 0; forAUX2 < (Random.Range(0, 24)); forAUX2++) Nexthatcolor(0);
        if (legsactive) for (int forAUX2 = 0; forAUX2 < (Random.Range(0, 32)); forAUX2++) Nextlegscolor(0);
        
        for (int forAUX2 = 0; forAUX2 < (Random.Range(0, 34)); forAUX2++) Nextchestcolor(0);
    }
    public void CreateCopy()
    {
        GameObject newcharacter = Instantiate(gameObject, transform.position, transform.rotation);
        for (int forAUX = transform.childCount - 1; forAUX > 0; forAUX--)
        {
            if (!newcharacter.transform.GetChild(forAUX).gameObject.activeSelf) DestroyImmediate(newcharacter.transform.GetChild(forAUX).gameObject);
        }
        if (!GOglasses.activeSelf) DestroyImmediate(newcharacter.transform.Find("ROOT/TP/TP Pelvis/TP Spine/TP Spine1/TP Spine2/TP Neck/TP Head/Glasses").gameObject as GameObject);
        DestroyImmediate(newcharacter.GetComponent<TPFemalePrefabMaker>());
    }
    public void FIX()
    {
        GameObject newcharacter = Instantiate(gameObject, transform.position, transform.rotation);
        for (int forAUX = transform.childCount - 1; forAUX >= 0; forAUX--)
        {
            if (!newcharacter.transform.GetChild(forAUX).gameObject.activeSelf) DestroyImmediate(newcharacter.transform.GetChild(forAUX).gameObject);
        }
        if (!GOglasses.activeSelf) DestroyImmediate(newcharacter.transform.Find("ROOT/TP/TP Pelvis/TP Spine/TP Spine1/TP Spine2/TP Neck/TP Head/Glasses").gameObject as GameObject);
        DestroyImmediate(newcharacter.GetComponent<TPFemalePrefabMaker>());
        DestroyImmediate(gameObject);
    }

    public void ElderOn()
    {
        elder = true;
        haircoloractive = false;
        //blendshapes
        SkinnedMeshRenderer rendhead;
        rendhead = GOhead.GetComponent<SkinnedMeshRenderer>();
        rendhead.SetBlendShapeWeight(29, 100);
        

        //skin        
        SwitchMaterial(GOhead, MATSkins, MATElderSkins);
        for (int forAUX = 0; forAUX < GOchest.Length; forAUX++) SwitchMaterial(GOchest[forAUX], MATSkins, MATElderSkins);
        for (int forAUX = 0; forAUX < GOlegs.Length; forAUX++) SwitchMaterial(GOlegs[forAUX], MATSkins, MATElderSkins);
        for (int forAUX = 0; forAUX < GOfeet.Length; forAUX++) SwitchMaterial(GOfeet[forAUX], MATSkins, MATElderSkins);
        
        //teeth
        ChangeMaterials(MATteeth, 1);

        //hair
        ChangeMaterials(MAThairA, 5);
        ChangeMaterials(MAThairB, 5);
        ChangeMaterials(MAThairC, 5);
        ChangeMaterials(MAThairD, 5);
        ChangeMaterials(MAThairE, 5);
        ChangeMaterials(MAThairF, 5);
        ChangeMaterials(MAThairG, 5);

    }
    public void ElderOff()

    {
        elder = false;
        haircoloractive = true;
        //blendshapes
        SkinnedMeshRenderer rendhead;
        rendhead = GOhead.GetComponent<SkinnedMeshRenderer>();
        rendhead.SetBlendShapeWeight(29, 0);
        

        //skin
        SwitchMaterial(GOhead, MATElderSkins, MATSkins);
        for (int forAUX = 0; forAUX < GOchest.Length; forAUX++) SwitchMaterial(GOchest[forAUX], MATElderSkins, MATSkins);
        for (int forAUX = 0; forAUX < GOlegs.Length; forAUX++) SwitchMaterial(GOlegs[forAUX], MATElderSkins, MATSkins);
        for (int forAUX = 0; forAUX < GOfeet.Length; forAUX++) SwitchMaterial(GOfeet[forAUX], MATElderSkins, MATSkins);

        //teeth
        ChangeMaterials(MATteeth, 1);

        //hair 
        ChangeMaterials(MAThairA, 3);
        ChangeMaterials(MAThairB, 3);
        ChangeMaterials(MAThairC, 3);
        ChangeMaterials(MAThairD, 3);
        ChangeMaterials(MAThairE, 3);
        ChangeMaterials(MAThairF, 3);
        ChangeMaterials(MAThairG, 3);
    }
    public void Nude()
    {
        GOchest[chest].SetActive(false);
        GOlegs[legs].SetActive(false);
        GOfeet[feet].SetActive(false);
        chest = 0; legs = 0; feet = 0;
        GOchest[0].SetActive(true);
        GOlegs[0].SetActive(true);
        GOfeet[0].SetActive(true);
        if (jacket != 2) GOjackets[jacket].SetActive(false);
        jacketactive = false;
        jacket = 2;
        if (tie != 3) GOties[tie].SetActive(false);
        tie = 3;
    }
    void ChangeMaterial(GameObject GO, Object[] MAT, int todo)
    {
        bool found = false;
        int MATindex = 0;
        int subMAT = 0;
        Material[] AUXmaterials;
        AUXmaterials = GO.GetComponent<Renderer>().sharedMaterials;
        int materialcount = GO.GetComponent<Renderer>().sharedMaterials.Length;

        for (int forAUX = 0; forAUX < materialcount; forAUX++)
            for (int forAUX2 = 0; forAUX2 < MAT.Length; forAUX2++)
            {
                if (AUXmaterials[forAUX].name == MAT[forAUX2].name)
                {
                    subMAT = forAUX;
                    MATindex = forAUX2;
                    found = true;
                }
            }
        if (found)
        {
            if (todo == 0) //increase
            {
                MATindex++;
                if (MATindex > MAT.Length - 1) MATindex = 0;
            }
            if (todo == 1) //decrease
            {
                MATindex--;
                if (MATindex < 0) MATindex = MAT.Length - 1;
            }
            if (todo == 2) //random value
            {
                MATindex = Random.Range(0, MAT.Length);
            }
            if (todo == 3) //reset value
            {
                MATindex = 0;
            }
            if (todo == 4) //penultimate
            {
                MATindex = MAT.Length - 2;
            }
            if (todo == 5) //last one
            {
                MATindex = MAT.Length - 1;
            }
            AUXmaterials[subMAT] = MAT[MATindex] as Material;
            GO.GetComponent<Renderer>().sharedMaterials = AUXmaterials;
        }
    }
    void ChangeMaterials(Object[] MAT, int todo)
    {
        for (int forAUX = 0; forAUX < GOhair.Length; forAUX++) ChangeMaterial(GOhair[forAUX], MAT, todo);
        ChangeMaterial(GOhead, MAT, todo);
        ChangeMaterial(GOglasses, MAT, todo);

        ChangeMaterial(GOheadsimple, MAT, todo);
        
        ChangeMaterial(GOjackets[0], MAT, todo);
        ChangeMaterial(GOjackets[1], MAT, todo);
        for (int forAUX = 0; forAUX < GOties.Length; forAUX++) ChangeMaterial(GOties[forAUX], MAT, todo);

        for (int forAUX = 0; forAUX < GOchest.Length; forAUX++) ChangeMaterial(GOchest[forAUX], MAT, todo);
        for (int forAUX = 0; forAUX < GOlegs.Length; forAUX++) ChangeMaterial(GOlegs[forAUX], MAT, todo);
        for (int forAUX = 0; forAUX < GOfeet.Length; forAUX++) ChangeMaterial(GOfeet[forAUX], MAT, todo);

    }
    void SwitchMaterial(GameObject GO, Object[] MAT1, Object[] MAT2)
    {
        Material[] AUXmaterials;
        AUXmaterials = GO.GetComponent<Renderer>().sharedMaterials;
        int materialcount = GO.GetComponent<Renderer>().sharedMaterials.Length;
        int index = 0;
        for (int forAUX = 0; forAUX < materialcount; forAUX++)
            for (int forAUX2 = 0; forAUX2 < MAT1.Length; forAUX2++)
            {
                if (AUXmaterials[forAUX] == MAT1[forAUX2])
                {
                    index = forAUX2;
                    if (forAUX2 > MAT2.Length-1) index -= (int)Mathf.Floor(index / 4)*4;
                    AUXmaterials[forAUX] = MAT2[index] as Material;
                    GO.GetComponent<Renderer>().sharedMaterials = AUXmaterials;
                }
            }
    }
}


