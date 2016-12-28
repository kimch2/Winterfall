﻿using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Screenshot : MonoBehaviour
{
    private int count = 0;
    public AudioSource sound;

    void Start ()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/Screenshots"))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(Application.persistentDataPath + "/Screenshots");
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Screenshot"))
            StartCoroutine(ScreenshotEncode());
    }

    IEnumerator ScreenshotEncode()
    {
        // wait for graphics to render
        yield return new WaitForEndOfFrame();

        // create a texture to pass to encoding
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        // put buffer into texture
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        // split the process up--ReadPixels() and the GetPixels() call inside of the encoder are both pretty heavy
        yield return 0;

        byte[] bytes = texture.EncodeToPNG();
        float time = Time.deltaTime;

        // save our test image (could also upload to WWW)
        File.WriteAllBytes(Application.persistentDataPath + "/Screenshots/screenshot-" + count + "-" + time + ".png", bytes);
        count++;

        // Added by Karl. - Tell unity to delete the texture, by default it seems to keep hold of it and memory crashes will occur after too many screenshots.
        DestroyObject(texture);

        //Debug.Log("Screenshot saved to: " + Application.dataPath + "/Screenshots/screenshot-" + count + ".png");
        //StartCoroutine(Notifications.Call("Screenshot saved to: " + "/Screenshots/screenshot-" + count + "-" + time + ".png"));
        print("Screenshot saved to: " + Application.persistentDataPath + "/Screenshots/screenshot-" + count + "-" + time + ".png");
        sound.Play();
    }
}
