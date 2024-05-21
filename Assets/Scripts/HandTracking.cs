using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    [SerializeField] private UDPReceive uDPReceive;
    [SerializeField] private GameObject[] handPoints;


    private void Update()
    {
        string data = uDPReceive.data;
        print(data);

        if (data != null)
        {
            data = data.Remove(0, 1);
            data = data.Remove(data.Length - 1, 1);
            print(data);

            string[] handData = data.Split(',');
            print(handData[0]);

            for (int i = 0; i < 21; i++)
            {
                float x = float.Parse(handData[i * 3]) / 140;
                float y = float.Parse(handData[i * 3 + 1]) / 140;
                float z = float.Parse(handData[i * 3 + 2]) / 180;
                handPoints[i].transform.localPosition = new Vector3(x, y, z);
            }
        }


    }

}
