using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapManager : MonoBehaviour {

    //public string mapJsonPath;
    public TextAsset mapJsonFile;
    public LocationList map;

	// Use this for initialization
	void Start () {
        //string json = File.ReadAllText(mapJsonPath);
        string json = mapJsonFile.ToString();
        LocationList_FromJson jsonMap = JsonUtility.FromJson<LocationList_FromJson>(json);
        map = jsonMap.Initialize(jsonMap);

        map.locations[0].Display();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // From: http://answers.unity.com/answers/802424/view.html
    public static Texture2D LoadImage(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath)) {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}

// TODO: Change to abstract class
public abstract class Initializable<T, Top>
{
    public T result = default(T);
    public T Initialize(Top topLevelObject)
    {
        //Debug.Log(this.ToString());

        if(result == null) {
            result = Generate(topLevelObject);
        }

        return result;
    }
    public abstract T Generate(Top topLevelObject);
}

[System.Serializable]
public class LocationList_FromJson : Initializable<LocationList, LocationList_FromJson>
{
    public List<Location_FromJson> locations;

    public Location_FromJson getRoomByName(string name)
    {
        foreach( Location_FromJson loc in locations) {
            if(loc.name == name) return loc;
        }

        return null;
    }

    public override LocationList Generate(LocationList_FromJson map)
    {
        List<Location> locationsList = new List<Location>();
        foreach ( Location_FromJson location in locations) {
            locationsList.Add( location.Initialize(map) );
        }

        return new LocationList(locationsList);
    }

    public override string ToString()
    {
        return "This is a location list";
    }
}

public class LocationList
{
    public List<Location> locations;

    public LocationList(List<Location> locations)
    {
        this.locations = locations;
    }
}

[System.Serializable]
public class Location_FromJson : Initializable<Location, LocationList_FromJson>
{
    public string name;
    public bool isVideo;
    public string displayPath;
    public List<Direction_FromJson> directions;

    public override Location Generate(LocationList_FromJson map)
    {
        // Initialize all directions needed
        List<Direction> dir = new List<Direction>();
        foreach(Direction_FromJson d in directions) {
            dir.Add(
                d.Initialize(map)
            );
        }

        if (isVideo) {
            // FIXME: This line should load video clip from path
            //return new VideoLocation(name, dir, VideoClip.)
        }
        return new ImageLocation(name, dir, MapManager.LoadImage(displayPath));
    }

    public override string ToString()
    {
        return (isVideo ? "Video Location:" : "Image Location:") + name;
    }
}

[System.Serializable]
public class Direction_FromJson : Initializable<Direction, LocationList_FromJson>
{
    public string targetName;
    public float angle;

    public override Direction Generate(LocationList_FromJson map)
    {
        Location_FromJson targetJson = map.getRoomByName(targetName);

        Location target = targetJson.Initialize(map);
        return new Direction(angle, target);
    }

    public override string ToString()
    {
        return "Direction to \"" + targetName + "\" at " + angle + "degrees";
    }
}