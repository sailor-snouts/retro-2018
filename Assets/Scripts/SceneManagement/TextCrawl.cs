using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCrawl : MonoBehaviour {

    [SerializeField]
    List<Text> textToCrawl = null;
    [SerializeField]
    float speed = 5.0f;
    [SerializeField]
    string gotoSceneName = "Title";
    [SerializeField]
    float crawlTime = 30.0f;

    private Navigation nav;

    private void Start()
    {
        nav = FindObjectOfType<Navigation>();
        Invoke("EndCrawl", crawlTime);
    }

    void FixedUpdate () {
        float delta = Time.fixedDeltaTime;

        foreach( Text text in textToCrawl ) {
            Vector3 pos = text.transform.position;
            text.transform.position = new Vector3(pos.x, pos.y + (speed * delta), pos.z);
        }
	}

    void EndCrawl() {
        nav.GoToScene(gotoSceneName);
    }
}
