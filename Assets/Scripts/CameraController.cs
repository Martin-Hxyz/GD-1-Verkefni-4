using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject following;
    private Transform _transform;
    private Vector3 _offset = new Vector3(0, 0, -3);

    void Start()
    {
        _transform = transform;
    }

    void Update()
    {
        _transform.position = following.transform.position + _offset;
    }
}