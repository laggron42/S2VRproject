using UnityEngine;
using UnityEditor;
using System.Collections;
// CopyComponents - by Michael L. Croswell for Colorado Game Coders, LLC
// March 2010
 
//Modified by Kristian Helle Jespersen
//June 2011
 
public class ReplaceGameObjects : ScriptableWizard
{
    public bool copyValues = true;
    public GameObject NewType;
    public GameObject[] OldObjects;
 
    [MenuItem("Custom/Replace GameObjects")]
 
 
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Replace GameObjects", typeof(ReplaceGameObjects), "Replace");
    }
 
    void OnWizardCreate()
    {
        //Transform[] Replaces;
        //Replaces = Replace.GetComponentsInChildren<Transform>();
        int i = 1;
        foreach (GameObject go in OldObjects)
        {
            GameObject newObject;
            newObject = (GameObject)PrefabUtility.InstantiatePrefab(NewType);
            if (i > 0)
                newObject.name = newObject.name + " (" + i + ")";
            newObject.transform.position = go.transform.position;
            newObject.transform.rotation = go.transform.rotation;
            newObject.transform.parent = go.transform.parent;
 
            DestroyImmediate(go);
            i++;
        }
 
    }
}