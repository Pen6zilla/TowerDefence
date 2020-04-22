using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    [SerializeField]
    private float _touchBoundry = 0.2f;

    private Transform _target;
    private int _wayPointIndex = 0;

    void Start()
    {
        _target = WayPoints.points[0];
    }

    void Update()
    {
        Vector3 dir = _target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, _target.position) <= _touchBoundry)
        {
            GetNextWaypoint();
        }

        void GetNextWaypoint()
        {
            if(_wayPointIndex >= WayPoints.points.Length - 1)
            {
                Destroy(gameObject);
                PlayerStats.Health--;
                return;
            }

            _wayPointIndex++;
            _target = WayPoints.points[_wayPointIndex];
        }
    }
}
