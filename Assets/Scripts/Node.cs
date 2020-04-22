using UnityEngine;

public class Node : MonoBehaviour
{
    private Renderer _rend;
    private Color _startColor;

    [Header("Optional")]
    public GameObject Turret;
    public Color NoBuildColor;
    public Color PointColor;
    public Vector3 PositionOffset;

    BuildManager buildManager;

    void Start()
    {
        ColorOfNode();
        _startColor = _rend.material.color;
        buildManager = BuildManager.Instance;
    }

    public Renderer ColorOfNode()
    {
        _rend = GetComponent<Renderer>();
        return _rend;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + PositionOffset;
    }
    void OnMouseDown()
    {
        if(!buildManager.CanBuild)
        {
            Debug.Log("You didnt choose the type of turret");
            _rend.material.color = NoBuildColor;
            return;
        }

        if (Turret != null)
        {
            Debug.Log("No bild here for you");
            _rend.material.color = NoBuildColor;
            return;
        }

        buildManager.BuildTurretOn(this);
    }

    void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
            return;

        _rend.material.color = PointColor;
    }

    void OnMouseExit()
    {
        _rend.material.color = _startColor;
    }
}
