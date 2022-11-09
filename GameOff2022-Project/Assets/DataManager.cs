using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; set; }
    public OreInBoxItemDB OreInBoxItemDB;

    public ItemsInWorldDB ItemsInWorldDB;

    public GameObject OrePrefab;
    public Transform collectionBoxTransformRef;

    public GameObject[] itemsInWorld;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        LoadOresInBox();

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Shop"){
            LoadItemsInWorld();
        }
        else{
            return;
        }

        DebugOreCount();
    }

    public void AddOreToBox(GameObject obj, string oreType, float oreSize, float oreWeight, float oreQuality, float orePrice){
        OreInBoxItem item = new OreInBoxItem();
        item.Position = obj.transform.localPosition;
        item.oreRotationQ = obj.transform.rotation;
        item.oreType = oreType;
        item.oreSize = oreSize;
        item.oreWeight = oreWeight;
        item.oreQuality = oreQuality;
        item.orePrice = orePrice;
        OreInBoxItemDB.items.Add(item);
    }

    public void SaveOresInBox(){
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(OreInBoxItemDB));
        //FileStream stream = new FileStream(Application.persistentDataPath + "/GameFiles/orebox.xml", FileMode.Create);
        string path = Application.persistentDataPath + "orebox.xml";
        FileStream stream = new FileStream(path, FileMode.Create);
        xmlSerializer.Serialize(stream, OreInBoxItemDB);
        stream.Close();
    }

    public void LoadOresInBox(){
        //Debug.Log(Application.persistentDataPath);
        if (!File.Exists(Application.persistentDataPath + "orebox.xml")){
            return;
        }
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(OreInBoxItemDB));
        FileStream stream = new FileStream(Application.persistentDataPath + "orebox.xml", FileMode.Open);
        OreInBoxItemDB = xmlSerializer.Deserialize(stream) as OreInBoxItemDB;
        stream.Close();

        foreach(OreInBoxItem item in OreInBoxItemDB.items){
            GameObject ore = Instantiate(OrePrefab, new Vector3(0f,0f,0f), Quaternion.identity);
            //ore.transform.parent = collectionBoxTransformRef;
            //ore.transform.localPosition = item.Position;
            //ore.transform.parent = null;
            ore.GetComponent<Ore>().oreType = item.oreType;
            ore.GetComponent<Ore>().size = item.oreSize;
            //ore.GetComponent<Ore>().SetSize(item.oreSize);
            ore.GetComponent<Ore>().weight = item.oreWeight;
            ore.GetComponent<Ore>().quality = item.oreQuality;
            ore.GetComponent<Ore>().price = item.orePrice;
            ore.transform.parent = collectionBoxTransformRef;
            ore.transform.localPosition = item.Position;
            ore.transform.rotation = item.oreRotationQ;
        }

        OreInBoxItemDB.items.Clear();
    }

    public void SaveItemsInWorld(){

        ItemsInWorldDB.items.Clear();

        itemsInWorld = null;
        if (itemsInWorld == null){
            itemsInWorld = GameObject.FindGameObjectsWithTag("Pickup");
        }

        foreach (GameObject iObj in itemsInWorld){
            if (iObj.GetComponent<Ore>().inCollectionZone == false){
                ItemsInWorld item = new ItemsInWorld();
                item.Position = iObj.transform.position;
                item.oreRotationQ = iObj.transform.rotation;
                item.oreType = iObj.GetComponent<Ore>().oreType;
                item.oreSize = iObj.GetComponent<Ore>().size;
                item.oreWeight = iObj.GetComponent<Ore>().weight;
                item.oreQuality = iObj.GetComponent<Ore>().quality;
                item.orePrice = iObj.GetComponent<Ore>().price;
                ItemsInWorldDB.items.Add(item);
            }
        }

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ItemsInWorldDB));
        string path = Application.persistentDataPath + "inworld.xml";
        FileStream stream = new FileStream(path, FileMode.Create);
        xmlSerializer.Serialize(stream, ItemsInWorldDB);
        stream.Close();
    }

    public void LoadItemsInWorld(){
        //Debug.Log(Application.persistentDataPath);

        if (!File.Exists(Application.persistentDataPath + "inworld.xml")){
            return;
        }

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ItemsInWorldDB));
        FileStream stream = new FileStream(Application.persistentDataPath + "inworld.xml", FileMode.Open);
        ItemsInWorldDB = xmlSerializer.Deserialize(stream) as ItemsInWorldDB;
        stream.Close();

        foreach(ItemsInWorld item in ItemsInWorldDB.items){
            GameObject ore = Instantiate(OrePrefab, new Vector3(0f,0f,0f), Quaternion.identity);
            //ore.transform.parent = collectionBoxTransformRef;
            //ore.transform.localPosition = item.Position;
            //ore.transform.parent = null;
            ore.GetComponent<Ore>().oreType = item.oreType;
            ore.GetComponent<Ore>().size = item.oreSize;
            ore.GetComponent<Ore>().weight = item.oreWeight;
            ore.GetComponent<Ore>().quality = item.oreQuality;
            ore.GetComponent<Ore>().price = item.orePrice;
            ore.transform.position = item.Position;
            ore.transform.rotation = item.oreRotationQ;
        }

        ItemsInWorldDB.items.Clear();
    }

    public void ClearAllOreFromScene(){
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Pickup");
        foreach(GameObject obj in allObjects) {
            if(obj.tag == "Pickup"){
                Destroy(obj);
            }
        }
    }

    public void DebugOreCount(){
        int oreCount = 0;
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Pickup");
        
        foreach(GameObject obj in allObjects) {
            if(obj.tag == "Pickup"){
                oreCount = oreCount + 1;
            }
        }

        Debug.Log("Ore Count: " + oreCount.ToString());
    }
}

[System.Serializable]
public class OreInBoxItemDB{
    public List<OreInBoxItem> items = new List<OreInBoxItem>();
}

[System.Serializable]
public class ItemsInWorldDB{
    public List<ItemsInWorld> items = new List<ItemsInWorld>();
}

[System.Serializable]
public class OreInBoxItem{
    public Vector3 Position;
    public string oreType;
    public float oreSize;
    public float oreWeight;
    public float oreQuality;
    public float orePrice;
    public Quaternion oreRotationQ;
}

[System.Serializable]
public class ItemsInWorld{
    public Vector3 Position;
    public string oreType;
    public float oreSize;
    public float oreWeight;
    public float oreQuality;
    public float orePrice;
    public Quaternion oreRotationQ;
}