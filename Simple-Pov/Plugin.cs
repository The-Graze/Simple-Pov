using BepInEx;
using Photon.Voice.Unity;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;

namespace Simple_Pov
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool set = false;
        GameObject camank;
        GameObject shoulderCamera;
        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }
        void OnGameInitialized(object sender, EventArgs e)
        {
            shoulderCamera = GameObject.Find("Global/Third Person Camera/Shoulder Camera");
            GameObject.Find("Global/Third Person Camera").transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Global/Third Person Camera").transform.GetChild(0).GetComponent<Camera>().fieldOfView = 100;
            StartCoroutine(ExecuteAfterTime(1));
        }
        void Update()
        {
            if (set == true)
            {
               
            }
        }
        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            shoulderCamera.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            shoulderCamera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            shoulderCamera.transform.SetParent(GameObject.Find("Global/Local VRRig/Local Gorilla Player/rig/body/head").transform, false);
            set = true;
        }
    }
}
