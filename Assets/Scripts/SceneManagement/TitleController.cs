using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {

    [SerializeField]
    float crawlTimer = 30.0f;

    Navigation nav;

    private void Start()
    {
        nav = FindObjectOfType<Navigation>();
        Invoke("StartCrawl", crawlTimer);
    }

    void StartCrawl() {
        nav.GoToScene("OpeningCrawl");
    }

}
