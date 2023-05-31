using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TPFemalePrefabMaker))]


public class EditorTPFemalePrefabMaker : Editor
{
    public override void OnInspectorGUI()
    {
        TPFemalePrefabMaker myPrefabMaker = (TPFemalePrefabMaker)target;
        if (!myPrefabMaker.allOptions)
        {
            if (GUILayout.Button("LET'S GET DRESS", GUILayout.Width(250), GUILayout.Height(75)))
            {
                myPrefabMaker.Menu();
                myPrefabMaker.Getready();
            }
        }

        else
        {
            if (GUILayout.Button("RANDOMIZE", GUILayout.Width(250), GUILayout.Height(75)))
            {
                myPrefabMaker.Randomize();
            }

            EditorGUILayout.Space();

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Prevhat(); }
            EditorGUILayout.LabelField("   HAT", GUILayout.Width(65), GUILayout.Height(20));
            if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nexthat(); }
            if (myPrefabMaker.hatactive)
            {
                if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nexthatcolor(1); }
                EditorGUILayout.LabelField("  material", GUILayout.Width(65), GUILayout.Height(20));
                if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nexthatcolor(0); }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Prevhair(); }
            EditorGUILayout.LabelField("  HAIR", GUILayout.Width(65), GUILayout.Height(20));
            if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nexthair(); }
            if (myPrefabMaker.haircoloractive)
            {
                if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nexthaircolor(1); }
                EditorGUILayout.LabelField("  material", GUILayout.Width(65), GUILayout.Height(20));
                if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nexthaircolor(0); }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextskincolor(1); }
            EditorGUILayout.LabelField("  SKIN", GUILayout.Width(65), GUILayout.Height(20));
            if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextskincolor(0); }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nexteyescolor(1); }
            EditorGUILayout.LabelField("  EYES", GUILayout.Width(65), GUILayout.Height(20));
            if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nexteyescolor(0); }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("GLASSES", GUILayout.Width(115), GUILayout.Height(20))) { myPrefabMaker.GlassesOn(); }
            if (myPrefabMaker.glassesactive)
            {
                if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextglasses(1); }
                EditorGUILayout.LabelField("  material", GUILayout.Width(65), GUILayout.Height(20));
                if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextglasses(0); }
            }
            GUILayout.EndHorizontal();

            

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Prevjacket(); }
            EditorGUILayout.LabelField(" JACKET", GUILayout.Width(65), GUILayout.Height(20));
            if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextjacket(); }
            if (myPrefabMaker.jacketactive)
            {
                if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextjacketcolor(1); }
                EditorGUILayout.LabelField("  material", GUILayout.Width(65), GUILayout.Height(20));
                if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextjacketcolor(0); }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Prevchest(); }
            EditorGUILayout.LabelField("   CHEST", GUILayout.Width(65), GUILayout.Height(20));
            if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextchest(); }
            if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextchestcolor(1); }
            EditorGUILayout.LabelField("  material", GUILayout.Width(65), GUILayout.Height(20));
            if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextchestcolor(0); }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            if (myPrefabMaker.tieactive)
            {
                if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Prevtie(); }
                EditorGUILayout.LabelField("     TIE", GUILayout.Width(65), GUILayout.Height(20));
                if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nexttie(); }
                if (myPrefabMaker.tieactivecolor)
                {
                    if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nexttiecolor(1); }
                    EditorGUILayout.LabelField("  material", GUILayout.Width(65), GUILayout.Height(20));
                    if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nexttiecolor(0); }
                }
            }
            else EditorGUILayout.LabelField("        ", GUILayout.Width(115), GUILayout.Height(20));
            GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box");
            if (myPrefabMaker.legsactive)
            {
                if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Prevlegs(); }
                EditorGUILayout.LabelField("    LEGS", GUILayout.Width(65), GUILayout.Height(20));
                if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextlegs(); }
                if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextlegscolor(1); }
                EditorGUILayout.LabelField("  material", GUILayout.Width(65), GUILayout.Height(20));
                if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextlegscolor(0); }
            }
            else EditorGUILayout.LabelField("        ", GUILayout.Width(115), GUILayout.Height(20));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Prevfeet(); }
            EditorGUILayout.LabelField("    FEET", GUILayout.Width(65), GUILayout.Height(20));
            if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextfeet(); }
            if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextfeetcolor(1); }
            EditorGUILayout.LabelField("  material", GUILayout.Width(65), GUILayout.Height(20));
            if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(20))) { myPrefabMaker.Nextfeetcolor(0); }
            GUILayout.EndHorizontal();

            if (GUILayout.Button("NUDE", GUILayout.Width(100), GUILayout.Height(25))) myPrefabMaker.Nude();

            if (myPrefabMaker.elder)
            {
                if (GUILayout.Button("ELDER OFF", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    myPrefabMaker.ElderOff();
                }
            }
            else if (GUILayout.Button("ELDER ON", GUILayout.Width(100), GUILayout.Height(25)))
            {
                myPrefabMaker.ElderOn();
            }


            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("CREATE COPY", GUILayout.Width(100), GUILayout.Height(50)))
            {
                myPrefabMaker.CreateCopy();
            }
            if (GUILayout.Button("DONE", GUILayout.Width(100), GUILayout.Height(50)))
            {
                myPrefabMaker.FIX();
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button("RESET", GUILayout.Width(100), GUILayout.Height(50)))
            {
                myPrefabMaker.ResetModel();
            }
        }
    }
}
