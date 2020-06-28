using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class SkyboxSetter : MonoBehaviour {
    // Fading Stuff
    public float fadeTime = 2.0f;
    public Image blindfold;

    // Image Stuff
    public Material imageMaterial;
    
    // Video Stuff
    public Material videoMaterial;
    public VideoPlayer videoPlayer;

    private VideoClip video;
    private Texture image;

    public void SetSkybox(VideoClip vid)
    {
        video = vid;

        Invoke("fadeToBlack", 0);
        Invoke("switchToVid", fadeTime / 3);
        Invoke("fadeBack", 2*fadeTime / 3);
    }

    public void SetSkybox(Texture img)
    {
        image = img;

        Invoke("fadeToBlack", 0);
        Invoke("switchToImg", fadeTime / 3);
        Invoke("fadeBack", 2*fadeTime / 3);
    }

    private void FadeToBlack()
    {
        blindfold.CrossFadeAlpha(1.0f, fadeTime / 3, false);
    }
    private void SwitchToVid()
    {
        videoPlayer.clip = video;
        RenderSettings.skybox = videoMaterial;
    }
    private void SwitchToImg()
    {
        imageMaterial.SetTexture("_MainTex", image);
        RenderSettings.skybox = imageMaterial;
    }
    private void fadeBack()
    {
        blindfold.CrossFadeAlpha(0.0f, fadeTime / 3, false);
    }

    public void Start()
    {
        if(singleton == null) {
            singleton = this;
        }
    }

    // Singleton Stuff
    private static SkyboxSetter singleton;
    public static SkyboxSetter GetSkyboxSetter(Material imgMat, Material vidMat, VideoPlayer vidPlay, Image blindImg)
    {
        if (singleton == null)
        {
            singleton = new SkyboxSetter(imgMat, vidMat, vidPlay, blindImg);
        }

        return singleton;
    }

    public static SkyboxSetter Get()
    {
        return singleton;
    }
    private SkyboxSetter(Material imgMat, Material vidMat, VideoPlayer vidPlay, Image blindfoldingImage)
    {
        imageMaterial = imgMat;
        videoMaterial = vidMat;
        videoPlayer = vidPlay;
    }
}