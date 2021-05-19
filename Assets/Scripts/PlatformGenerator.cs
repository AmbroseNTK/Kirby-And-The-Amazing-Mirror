using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> ObjectDictionary = new List<GameObject>();
    public float TileSize = 0.15f;
    public string MapText = "0";
    public Camera MainCam;

    // Start is called before the first frame update
    void Start()
    {
        GeneratePlatform();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnValidate()
    {
        try
        {
            GeneratePlatform();
        }
        catch (Exception ex) { return; }
    }

    private void GeneratePlatform()
    {

        // Remove children
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);
            // if (child.gameObject.tag == "Generated")
            // {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall += () =>
            {
                if (child != null)
                {
                    DestroyImmediate(child.gameObject);
                }
            };
#elif !UNITY_EDITOR
    //DestroyImmediate(child.gameObject);
#endif
            // }
        }
        string[] lines = MapText.Split('~');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] textTiles = lines[i].Split('|');
            for (int j = 0; j < textTiles.Length; j++)
            {
                if (textTiles[j] == "")
                {
                    continue;
                }
                GameObject selected = ObjectDictionary[int.Parse(textTiles[j].Trim())];
                Console.WriteLine(this.transform.position);

                GameObject child = Instantiate(selected, this.transform.position + new Vector3(TileSize * j, -TileSize * i, 0), Quaternion.identity);
                //child.tag = "Generated";
                child.transform.parent = this.transform;


            }
        }
    }
}
