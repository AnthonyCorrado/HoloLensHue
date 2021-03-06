﻿using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class SmartLightUI : MonoBehaviour {

    private GameObject holoLightContainer;
    private GameObject lightUI;
    private int tagId;

    private bool showLightUI = false;

    private GestureManager gestureManager;

    public void Start()
    {
        gestureManager = GestureManager.Instance;

        tagId = int.Parse(gameObject.tag);

        foreach (Transform child in transform)
        {
            if (child.name == "HoloLightContainer(Clone)")
            {
                holoLightContainer = child.gameObject;
            }
        }
        
        if (holoLightContainer)
        {
            holoLightContainer.SetActive(false);
        }
    }

    void OnEnable()
    {
        LightUIManager.toggleUIChanged += updateToggleShow;
    }

    void OnDisable()
    {
        LightUIManager.toggleUIChanged -= updateToggleShow;
    }

    private void UpdateSmartLightUI(SmartLight sl)
    {
        if (sl.Name == gameObject.name)
        {
            Renderer rend = gameObject.GetComponent<Renderer>();
            Vector4 ledColor = ColorService.GetColorByHue(sl.State.Hue);
            rend.material.color = ledColor;
        }
    }

    private void updateToggleShow()
    {
        holoLightContainer.SetActive(LightUIManager.lightUIs[tagId].Show);     
    }

    public void OnSelect()
    {
        SendMessageUpwards("ToggleUI", tagId, SendMessageOptions.DontRequireReceiver);
    }
}
