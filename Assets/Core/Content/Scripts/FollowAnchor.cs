/**
* Collaborator:
* - Diro Baloska
**/
using UnityEngine;

public class FollowAnchor : MonoBehaviour
{
    public GameObject anchor;
    [SerializeField] private float m_offset;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.LookAt(GameObject.Find("Main Camera").transform);
        this.transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.anchor == null) return;
        Vector3 position = anchor.transform.position;
        transform.position = new Vector3(position.x, position.y + m_offset, position.z);
    }

    public void SetAnchor(string anchorName) {
        anchor = GameObject.Find(anchorName);
    }
}
