using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Android;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    private double TargetLatitude = 12.978204713521103;
    private double TargetLongitude = 77.63848876908354;

    private double CurrentLatitude;
    private double CurrentLongitude;

    public double EarthEquatorialRadiusInKM = 6378.137;
    public double EarthFirstEccentricitySquared = 0.00669437999014;


    void Start()
    {
        
        TakeLocationPermission();

    }


    private void TakeLocationPermission()
    {
        Debug.Log("TakeLocationPermission.....");
        var cb = new PermissionCallbacks();
        cb.PermissionDenied += (s) =>
        {
            Debug.LogErrorFormat("User denied permission: {0}", s);
        };
        cb.PermissionGranted += (s) =>
        {
            Debug.LogErrorFormat("User granted permission: {0}", s);
            if (Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
            {
                Debug.Log("Microphone permission has been granted.");
            }
            StartCoroutine(nameof(LocationGet));
        };
        cb.PermissionDeniedAndDontAskAgain += (s) =>
        {
            Debug.LogErrorFormat("User Denied permission, dont ask again: {0}", s);
        };

        Permission.RequestUserPermission(Permission.FineLocation, cb);
    }



    public Text lattitude, longitude;
    IEnumerator LocationGet()
    {
        Debug.Log("Location => " + Input.location.isEnabledByUser);
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
            yield break;

        // Starts the location service.
        Input.location.Start();

        // Waits until the location service initializes
        int maxWait = 30;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

            lattitude.text = "" + Input.location.lastData.latitude;
            longitude.text = "" + Input.location.lastData.longitude;


            CurrentLatitude = Input.location.lastData.latitude;
            CurrentLongitude = Input.location.lastData.longitude;

            LocationToEcef();
        }

        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }




    IEnumerator GetCoordinates()
    {
        Debug.Log(Input.location.isEnabledByUser);

        if (!Input.location.isEnabledByUser)
            yield break;

        Input.location.Start(1f, .1f);

        int maxWait = 20;

        Debug.Log(Input.location.status);


        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);

            Debug.Log(maxWait);
            maxWait--;
        }

        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            CurrentLatitude = Input.location.lastData.latitude;
            CurrentLongitude = Input.location.lastData.longitude;

            LocationToEcef();
        }

        Input.location.Stop();

    }

    public void LocationToEcef()
    {
        var rad = Math.PI / 180;

        var latitude = CurrentLatitude * rad - TargetLatitude * rad;
        var longitude = CurrentLongitude * rad - TargetLongitude * rad;

        var a = EarthEquatorialRadiusInKM * 1000;
        var e2 = EarthFirstEccentricitySquared;
        var N = a / Math.Sqrt(1 - e2 * Math.Pow(Math.Sin(latitude), 2));

        var x = N * Math.Cos(latitude) * Math.Cos(longitude);
        var y = N * Math.Cos(latitude) * Math.Sin(longitude);
        var z = (1 - e2) * N * Math.Sin(latitude);

        Vector3 Position = new Vector3((float)x, (float)y, (float)z);
        Debug.Log("XYZ Positions : " + Position);
    }

    private void Update()
    {

    }
}
