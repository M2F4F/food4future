using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAnchor : MonoBehaviour
{
    private GameObject m_anchor;
    // Start is called before the first frame update
    void Start()
    {
        m_anchor = GameObject.Find("Anchor");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_anchor.transform.position;
    }
}
