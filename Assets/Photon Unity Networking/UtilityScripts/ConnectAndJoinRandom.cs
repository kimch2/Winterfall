using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

/// <summary>
/// This script automatically connects to Photon (using the settings file),
/// tries to join a random room and creates one if none was found (which is ok).
/// </summary>
public class ConnectAndJoinRandom : Photon.MonoBehaviour
{
    /// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
    public bool AutoConnect = true;

    public byte Version = 1;

    public GameObject[] disableObjects;

    public Slider[] sliders;
    public GameObject[] panels;
    public Text[] texts;
    public TOD_Sky sky;

    public GameObject spawnpoint;

    GameObject player;

    /// <summary>if we don't want to connect in Start(), we have to "remember" if we called ConnectUsingSettings()</summary>
    private bool ConnectInUpdate = true;


    public virtual void Start()
    {
        PhotonNetwork.autoJoinLobby = false;    // we join randomly. always. no need to join a lobby to get the list of rooms.
    }

    public virtual void Update()
    {
        if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected)
        {
            Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");

            ConnectInUpdate = false;
            PhotonNetwork.ConnectUsingSettings(Version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
        }
    }


    // below, we implement some callbacks of PUN
    // you can find PUN's callbacks in the class PunBehaviour or in enum PhotonNetworkingMessage


    public virtual void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public virtual void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
    }

    // the following methods are implemented to give you some context. re-implement them as needed.

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }

    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        player = PhotonNetwork.Instantiate("PlayerWinterfall", spawnpoint.transform.position, Quaternion.identity, 0);
        Debug.Log("SpawnPlayer() called by PUN.");
        FixObjects();
    }

    void FixObjects()
    {
        player.transform.FindChild("Camera").gameObject.SetActive(true);
        player.GetComponent<FirstPersonController>().enabled = true;
        ((MonoBehaviour)player.GetComponent("Crouch")).enabled = true;
        Stats stats = player.GetComponent<Stats>();
        stats.enabled = true;
        stats.healthSlider = sliders[0];
        stats.hungerSlider = sliders[1];
        stats.thirstSlider = sliders[2];
        stats.energySlider = sliders[3];
        stats.moneyText = texts[0];
        stats.coordinatesText = texts[3];
        stats.dateText = texts[1];
        stats.temperatureText = texts[2];
        stats.damagePanel = panels[0];
        stats.deathPanel = panels[1];
        stats.sky = sky;

        foreach (GameObject go in disableObjects)
        {
            go.SetActive(false);
        }
    }
}
