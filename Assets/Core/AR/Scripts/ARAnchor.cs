using UnityEngine;

public class ARAnchor : MonoBehaviour
{
    private GameObject _camera;

    void Awake() {
        _camera = GameObject.Find("Main Camera");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Anchor spawned: " + this.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(_camera.transform);
        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
    }
}
