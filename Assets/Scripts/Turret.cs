using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private Transform _rotationPart;
    [SerializeField]
    private string _enemyTag = "Enemy";
    private Transform _target;

    public GameObject BulletPrefab;
    public Transform FirePoint;

    private float _fireCountdown = 0f;
    [Header("Characteristics")]
    [SerializeField]
    private float _rateOfSearch = 0.5f;

    public float Range = 15f;
    public float TurnSpeed = 10f;
    public float FireRate = 1f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, _rateOfSearch);
    }

    void UpdateTarget()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(_enemyTag);
        GameObject NearestEnemy = null;

        float ShortestDistance = Mathf.Infinity;

        foreach (GameObject Enemy in Enemies)
        {
            float DistanceToEnemy = Vector3.Distance(transform.position, Enemy.transform.position);

            if(DistanceToEnemy < ShortestDistance)
            {
                ShortestDistance = DistanceToEnemy;
                NearestEnemy = Enemy;
            }
        }

        if(NearestEnemy != null && ShortestDistance <= Range)
        {
            _target = NearestEnemy.transform;
        }
        else
        {
            _target = null;
        }
    }

    void Update()
    {
        if (_target == null)
            return;

        Vector3 Dir = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(Dir);
        Vector3 Rotation = Quaternion.Lerp(_rotationPart.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
        _rotationPart.rotation = Quaternion.Euler(0f, Rotation.y, 0f);

        if (_fireCountdown <= 0)
        {
            Shoot();
            _fireCountdown = 1f / FireRate;
        }

        _fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject BulletGo = (GameObject)Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Bullet bullet = BulletGo.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(_target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
