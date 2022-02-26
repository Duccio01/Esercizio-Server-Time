using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;//Servono a comunicare con la rete
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BallsGenerator : MonoBehaviour
{
    [SerializeField] int maxBalls = 10;
    [SerializeField] GameObject prefabBalls;
    public bool midnight = false;
    public string time;
    // Start is called before the first frame update
    void Start()
    {
        GeneraBalls();
        
    }

    private void GeneraBalls()
    {
        for(int i=0;i<maxBalls;i++)
        {
            Instantiate(prefabBalls, new Vector3(i * 2.0f, 0.2f, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ChangeDay());
        if(midnight)
        {
            GeneraBalls();
            midnight = false;
            
        }
    }

     public IEnumerator ChangeDay()
    {
        UnityWebRequest webReq=UnityWebRequest.Get("http://127.0.0.1:3000/date");
        webReq.timeout = 3;
        yield return webReq.SendWebRequest();

        string timeAsString = webReq.downloadHandler.text;
        if(!time.Equals(timeAsString,StringComparison.OrdinalIgnoreCase))
        {
            midnight = true;
            time = timeAsString;
        }
    }
}
