using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;
    [SerializeField]
    private float _effectLifeTime = 2f;

    public float Speed = 80f;
    public float ExplosionRadious = 0f;
    public GameObject HitEffect;
    public int KillEarning = 100;

    public void Seek(Transform Target)
    {
        _target = Target;
    }

    private void Update()
    {
        if(_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = _target.position - transform.position;
        float DistanceThisFrame = Speed * Time.deltaTime;

        if(dir.magnitude <= DistanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * DistanceThisFrame, Space.World);
        transform.LookAt(_target);
    }

    void HitTarget()
    {
        GameObject Effect = (GameObject)Instantiate(HitEffect, transform.position, transform.rotation);
        Destroy(Effect, _effectLifeTime);

        if(ExplosionRadious > 0f)
        {
            Explode(); 
        }
        else
        {
            Damage(_target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadious);
        foreach (Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Destroy(enemy.gameObject);
        PlayerStats.Money += KillEarning;
    }
}
