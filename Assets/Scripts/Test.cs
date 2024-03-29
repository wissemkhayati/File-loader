﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Test : MonoBehaviour
{

    public RawImage image;
    public VideoPlayer video;

    IEnumerator Start()
    {   
        //Load, save and display image from an external url
        FileLoader loader1 = new FileLoader("downloadedImage", "http://vps587231.ovh.net/static/cocacola.png");
        yield return StartCoroutine(loader1.LoadImage());
        image.texture = loader1.Source_texture;

        //Load, save and play video from an external url
        FileLoader loader2 = new FileLoader("downloadedVideo", "http://vps587231.ovh.net/static/TVPub/Stories/cocacola1.mp4");
        yield return StartCoroutine(loader2.LoadVideo());
        video.url = loader2.Source_video;
    }

}
