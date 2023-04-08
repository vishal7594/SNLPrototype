using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        StartCoroutine("GetCoordinates");
    }

    IEnumerator GetCoordinates()
    {
      
        while (true)
        {
            if (!Input.location.isEnabledByUser)
                yield break;

            Input.location.Start(1f, .1f);

            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
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

        Vector3 Position = new Vector3((float)x,(float)y, (float)z);
        Debug.Log("XYZ Positions : " + Position);
    }

    private void Update()
    {
        
    }
}
