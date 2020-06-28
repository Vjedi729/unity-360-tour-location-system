using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public abstract class Location {

    public static GameObject arrow;

    public string name;
    public List<Direction> directions;

    public abstract void Display();

    private List<GameObject> drawnArrows;
    public void DrawArrows()
    {
        foreach( Direction d in directions ) {
            GameObject g = Object.Instantiate(arrow) as GameObject;
            g.transform.rotation *= Quaternion.Euler(0, d.angle, 0);
            g.transform.position = new Vector3(0, 0, 0);

            Button b = g.GetComponentInChildren<Button>();
            b.onClick.AddListener(this.Leave);
            b.onClick.AddListener(d.GoToTarget);
            drawnArrows.Add(g);
        }
    }

    public void Leave()
    {
        foreach( GameObject g in drawnArrows) {
            GameObject.Destroy(g);
        }
    }
}

public class ImageLocation : Location
{
    private Texture image;

    public ImageLocation(string name, List<Direction> directions, Texture image)
    {
        this.name = name;
        this.directions = directions;
        this.image = image;
    }

    public override void Display()
    {
        SkyboxSetter.Get().SetSkybox(image);
        DrawArrows();
    }
}

public class VideoLocation : Location
{
    private VideoClip video;

    public VideoLocation(string name, List<Direction> directions, VideoClip video)
    {
        this.name = name;
        this.directions = directions;
        this.video = video;
    }

    public override void Display()
    {
        SkyboxSetter.Get().SetSkybox(video);
        DrawArrows();
    }

}