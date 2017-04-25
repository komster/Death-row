﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Camera : MonoBehaviour {

    public Camera main;

    private bool zoomOut = false;
    private bool zoomIn = false;



    void Start () {
        CS_Notify.Register(this, "ChangeToArenaCamera");
        CS_Notify.Register(this, "ChangeToTravelCamera");
        CS_Notify.Register(this, "ZoomOut");
        CS_Notify.Register(this, "StopZoomOut");
        CS_Notify.Register(this, "ZoomIn");
        CS_Notify.Register(this, "StopZoomIn");
    }

    private void Update()
    {
        if (zoomOut == true)
        {          
            if (main.orthographicSize < 15)
            {
                main.orthographicSize = main.orthographicSize + 1 * Time.deltaTime;
            }
        }
        if (zoomIn == true )
        {
            if (main.orthographicSize > 7.5)
            {
                main.orthographicSize = main.orthographicSize - 1 * Time.deltaTime;
            }
        }
    }

    public void ChangeToArenaCamera()
    {

    }

    public void ChangeToTravelCamera()
    {

    }
    public void ZoomOut()
    {
        zoomOut = true;
    }
    public void StopZoomOut()
    {
        zoomOut = false;
    }
    public void ZoomIn()
    {
        zoomIn = true;
    }
    public void StopZoomIn()
    {
        zoomIn = false;
    }
}
