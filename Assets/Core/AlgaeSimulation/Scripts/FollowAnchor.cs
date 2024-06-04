using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAnchor : MonoBehaviour
{
    private GameObject m_anchor;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.LookAt(GameObject.Find("Main Camera").transform);
        this.transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        StartCoroutine(AccessAnchor());
    }

    // Update is called once per frame
    void Update()
    {
        if(this.m_anchor != null) transform.position = m_anchor.transform.position;
    }

    IEnumerator AccessAnchor() {
        Debug.Log(this.m_anchor);
        if(this.m_anchor == null) {
            m_anchor = GameObject.Find("Anchor");
            yield return new WaitForEndOfFrame();

            yield return AccessAnchor();
        } 

        yield return null;
    }

    public void SetAnchor(string anchorName) {
        Debug.Log(this + " Setting Anchor");
        m_anchor = GameObject.Find(anchorName);
    }
}
