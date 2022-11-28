using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int Health = 100;
    public int Points = 100;
    [SerializeField] private Canvas HPBar;
    [SerializeField] private Slider HPSlider;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject _bullet;

    [SerializeField] private Transform enemyBody;
    [SerializeField] private float walkSpeed = 0.1f;

    private int AverageDamage = 50;
    private bool isReload = true;
    private float reloadTime = 15f;
    private float lastTime = 0;
    private float force = 80f;
    private float rangeMax = 20.0f;
    private const float rangeMin = 5.0f;

    private float smoothTime = 1f;
    private float currentRotationSpeed = 0f;

    private Rigidbody rb;
    private Vector3 from;

    public Transform target;

    private void Start()
    {
        lastTime = Time.time + reloadTime;
        
    }
    private void Update()
    {
        from = target.position - transform.position;

        EnemyRadiusInvestigate();
        HPbar();
        
    }
    public void HPbar()
    {
        HPBar.transform.rotation = Quaternion.LookRotation(from);
        HPSlider.value = Health;
    }
    private void EnemyRotation()
    {
        from.y = 0;
        transform.rotation = Quaternion.LookRotation(from);
    }
    public void EnemyMove()
    {
        enemyBody.parent.position += enemyBody.forward * walkSpeed * Time.deltaTime;
    }
    private void EnemyRadiusInvestigate()
    {
        if (from.magnitude <= rangeMax)
        {
            if (from.magnitude >= rangeMin) EnemyMove();
            EnemyRotation();
            Realoding();
        }
    }
    public void Destroy()
    {
        Destroy(transform.parent.gameObject);
    }
    private void Realoding()
    {
        if (isReload)
        {
            float reload = lastTime - Time.time;
            if (reload <= 0.0f)
            {
                isReload = false;
            }
        }
        if (isReload == false)
        {
            if (Time.time > lastTime)
            {
                isReload = true;
                lastTime = Time.time + reloadTime;
                Fire();
            }
        }
    }
    private void Fire()
    {
        var spawnPosition = Instantiate(_bullet, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
        rb = spawnPosition.GetComponent<Rigidbody>();
        spawnPosition.GetComponent<EnemyBullet>().AverageDamage = AverageDamage;
        if (rb)
        {
            rb.AddForce(_spawnPoint.transform.forward * force, ForceMode.Impulse);
        }
    }
}
