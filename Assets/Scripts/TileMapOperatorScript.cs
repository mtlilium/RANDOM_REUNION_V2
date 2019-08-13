using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapOperatorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //LoadStructure("TilemapTemplate", "Tilemap");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadStructure(string mapName,string structureName)
    {

        GameObject LoadStructure_object = Resources.Load("Map/"+mapName+"/"+structureName) as GameObject;
        GameObject LoadStructure_instance = (GameObject)Instantiate(LoadStructure_object, Vector3.zero,Quaternion.identity,this.transform);
       
    }
}
