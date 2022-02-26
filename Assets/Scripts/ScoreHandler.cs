using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;//Servono a comunicare con la rete
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private Text _scoreEntryTemplate;

    [SerializeField]public string _username;
    [SerializeField]public int _score;

    // Start is called before the first frame update
    void Start()
    {

        ScoreEntries entries;
        try
        {
            HttpWebRequest request = WebRequest.CreateHttp("http://127.0.0.1:3000/score");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                entries = JsonUtility.FromJson<ScoreEntries>(reader.ReadToEnd());
            }
            foreach(var entry in entries.entries)
            {
                Text newText = Instantiate(_scoreEntryTemplate, _scoreEntryTemplate.transform.parent);
                newText.text = $"{entry.name}: {entry.score}";
                newText.gameObject.SetActive(true);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            /*  HttpWebRequest request = WebRequest.CreateHttp("http://127.0.0.1:3000/set-score");
              request.Method = "POST";

              HttpWebResponse response = (HttpWebResponse)request.GetResponse();

              Debug.Log("Richiesta Inviata");*/
            StartCoroutine(SendDataToServer());
        }
    }

    IEnumerator SendDataToServer()
    {
        Dictionary<string, string> requestParent = new Dictionary<string, string>();
        requestParent.Add("text", "prova");

        UnityWebRequest request = UnityWebRequest.Post("http://127.0.0.1:3000/set-score",requestParent);
        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);
    }
}
