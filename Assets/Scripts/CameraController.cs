using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private bool _movement = true;

    public float PanSpeed = 30f;
    public float PanBorderThickness = 10f;

    public float ScrollSpeed = 5000f;
    public float MinY = 10f;
    public float MaxY = 90f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _movement = !_movement;

        if (!_movement)
            return;

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - PanBorderThickness)
        {
            transform.Translate(Vector3.forward * PanSpeed * Time.deltaTime, Space.World);
        }
        
        if (Input.GetKey("a") || Input.mousePosition.x <= PanBorderThickness)
        {
            transform.Translate(Vector3.back * PanSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= PanBorderThickness)
        {
            transform.Translate(Vector3.right * PanSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - PanBorderThickness)
        {
            transform.Translate(Vector3.left * PanSpeed * Time.deltaTime, Space.World);
        }

        float Scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 Pos = transform.position;

        Pos.y -= Scroll * ScrollSpeed * Time.deltaTime;
        Pos.y = Mathf.Clamp(Pos.y, MinY, MaxY);
        transform.position = Pos;
    }
}
