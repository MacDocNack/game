using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player movement")]
    [SerializeField] private float _walkSpeed = 3f;
    private float bodyRotationSpeed = 0.5f;
    private float rotate;

    [Header("Player camera")]
    [SerializeField] private float Sensitivity = 2f;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform cameraZoom;
    [SerializeField] private Transform cameraDefault;
    private float cameraMovementY;
    private float cameraMovementX;
    private bool isZoom = false;

    [Header("Player tank")]
    [SerializeField] private Player _player;
    [SerializeField] private Transform tower;
    [SerializeField] private Transform cannon;
    [SerializeField] private Transform body;

    [Header("Player health")]
    [SerializeField] private Slider HPSlider;
    public int PlayerHP;
    public int PlayerMaxHP;

    [Header("Shooting settings")]
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Text _reloading;
    [SerializeField] private Text bulletCountText;
    private int AverageDamage = 300;
    private bool isReload;
    private float _reloadTime = 1f;
    private float _lastTime = 0;
    private float _force = 60f;
    public float bulletMax = 16;
    public float bulletCount = 0;

    [Header("Another settings")]
    [SerializeField] private Score score;
    private Rigidbody rb;


    private void Start()
    {
        if (_player == null)
            Debug.LogError("Player is not set!!");
        else
            _player = GetComponent<Player>();

        PlayerHP = PlayerMaxHP;
        HPSlider.maxValue = PlayerMaxHP;
        Cursor.lockState = CursorLockMode.Locked;
        cameraDefault.position = _camera.transform.position;
        bulletCount = bulletMax;
    }

    private void Update()
    {
        HPSlider.value = PlayerHP;
        CameraMovement();
        Movement();
        Zoom();
        SpawnBullet();
    }

    private void CameraMovement()
    {
        if (_camera != null)
        {
            cameraMovementX += Input.GetAxis("Mouse X") * Sensitivity;
            cameraMovementY += Input.GetAxis("Mouse Y") * Sensitivity;

            cameraMovementY = Mathf.Clamp(cameraMovementY, -5, 10);
            tower.transform.rotation = Quaternion.Euler(0, cameraMovementX, 0);
            cannon.transform.rotation = Quaternion.Euler(-cameraMovementY, cameraMovementX, 0);
        }
    }
    private void Movement()
    {

        if (Input.GetKey(KeyCode.W))
        {
            body.parent.position += body.forward * _walkSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.parent.position -= body.forward * _walkSpeed * Time.deltaTime;

            if (Input.GetKey(KeyCode.A))
            {
                rotate += bodyRotationSpeed;
                gameObject.transform.rotation = Quaternion.Euler(0, rotate, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rotate -= bodyRotationSpeed;
                gameObject.transform.rotation = Quaternion.Euler(0, rotate, 0);
            }
        }
        if (!Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A))
            {
                rotate -= bodyRotationSpeed;
                gameObject.transform.rotation = Quaternion.Euler(0, rotate, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rotate += bodyRotationSpeed;
                gameObject.transform.rotation = Quaternion.Euler(0, rotate, 0);
            }
        }

    }
    private void Zoom()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            switch (isZoom)
            {
                case true:
                    _camera.transform.parent = cameraDefault;
                    _camera.transform.position = cameraDefault.position;
                    isZoom = false;
                    break;
                case false:
                    _camera.transform.parent = cameraZoom;
                    _camera.transform.position = cameraZoom.position;
                    isZoom = true;
                    break;
            }
        }
    }
    private void SpawnBullet()
    {
        if (bulletCount > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time > _lastTime)
                {
                    isReload = true;
                    _lastTime = Time.time + _reloadTime;
                    Fire();
                }
            }
            if (isReload)
            {
                float reload = _lastTime - Time.time;
                if (reload <= 0.0f)
                {
                    isReload = false;
                    _reloading.text = "Ready";
                }
                else
                {
                    _reloading.text = string.Format("{0:0.00}", reload);
                }
            }
        }
        else
        {
            _reloading.text = "No ammo!";
        }
        bulletCountText.text = string.Format("Ammo: {0}", bulletCount);
    }
    private void Fire()
    {
        var spawnPosition = Instantiate(_bullet, _spawnPoint.transform.position, new Quaternion(0, _spawnPoint.transform.rotation.y, 0, _spawnPoint.transform.rotation.w));
        spawnPosition.GetComponent<Bullet>().score = score;
        spawnPosition.GetComponent<Bullet>().AverageDamage = AverageDamage;
        rb = spawnPosition.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.AddForce(_spawnPoint.transform.forward * _force, ForceMode.Impulse);
        }
        bulletCount--;
    }
}
