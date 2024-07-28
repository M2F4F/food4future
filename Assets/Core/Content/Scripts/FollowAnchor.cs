/**
* Collaborator:
* - Diro Baloska
**/
using UnityEngine;

public class FollowAnchor : MonoBehaviour
{
    private GameObject m_anchor;
    [SerializeField] private float m_offset;

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
        if(this.m_anchor == null) return;
        Vector3 position = m_anchor.transform.position;
        transform.position = new Vector3(position.x, position.y + m_offset, position.z);
    }

    public void SetAnchor(string anchorName) {
        m_anchor = GameObject.Find(anchorName);
    }
}
