using BepInEx;
using Photon.Voice.Unity;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR;
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
        Camera cam;
        GameObject shoulderCamera;
        GameObject cm;
        GameObject head;
        bool joyLC;
        bool joyRC;
        Vector3 ogvec;
        
        private readonly XRNode lNode = XRNode.LeftHand;
        private readonly XRNode rNode = XRNode.RightHand;
        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }
        void OnGameInitialized(object sender, EventArgs e)
        {
            shoulderCamera = GameObject.Find("Global/Third Person Camera/Shoulder Camera");
            ogvec = shoulderCamera.transform.localPosition;
            cm = shoulderCamera.transform.GetChild(0).gameObject;
            cam = shoulderCamera.GetComponent<Camera>();
            head = GameObject.Find("Global/Local VRRig/Local Gorilla Player/rig/body/head");
            shoulderCamera.transform.SetParent(head.transform, false);
        }
        void FixedUpdate()
        {
            InputDevices.GetDeviceAtXRNode(lNode).TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out joyLC);
            InputDevices.GetDeviceAtXRNode(rNode).TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out joyRC);

            if (joyLC)
            {
               cm.SetActive(false);
               cam.fieldOfView = 100;
               shoulderCamera.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
               shoulderCamera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
               shoulderCamera.transform.SetParent(head.transform);

            }
            if (joyRC) 
            {
                shoulderCamera.transform.localPosition = ogvec;
                cm.SetActive(true);
                cam.fieldOfView = 120;

            }
        }
    }
}
