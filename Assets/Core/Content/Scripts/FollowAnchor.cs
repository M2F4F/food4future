/**
* Collaborator:
* - Diro Baloska
**/
using System;
using System.Collections;
using StateMachine;
using UnityEngine;

public class FollowAnchor : MonoBehaviour
{
    private GameObject m_anchor;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Follow Anchor Start");
        this.transform.LookAt(GameObject.Find("Main Camera").transform);
        this.transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.m_anchor != null) transform.position = m_anchor.transform.position;
    }

    public void SetAnchor(string anchorName) {
        m_anchor = GameObject.Find(anchorName);
    }
}
