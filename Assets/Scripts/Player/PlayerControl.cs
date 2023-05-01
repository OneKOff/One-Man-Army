using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PlayerControl : MonoBehaviour, IDamageable
{
    [Header("Movement")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float acceleration = 25f;
    [SerializeField] private float rushForce = 50f;
    [SerializeField] private float jumpForce = 15f;

    [Header("Attacking")] 
    [SerializeField] private PlayerAttack attackPrefab;
    [SerializeField] private float attackCooldown;

    [Header("Damageable")] 
    [SerializeField] private int maxHealth;
    [SerializeField] private Transform healthBarPosition;

    private HealthBar _assignedHealthBar;

    private Vector3 _movement, _targetMovement;
    // private bool _canJump = true;
    private Ray _ray;
    private Camera _cam;

    private float _currentAttackCd = 0f;
    private int _health;

    private void Start()
    {
        _cam = Camera.main;
        _health = maxHealth;
        AssignHealthBar(EnemyManager.Instance.GetNewHealthBar());
    }

    private void Update()
    {
        HandleRay();
        HandleControl();
        HandleAttack();
        HandleRotation();
        HandleHPBarMovement();
        // HandleRush();
        // HandleJump();
    }
    
    private void OnDestroy()
    {
        if (!_assignedHealthBar) { return; }
        Destroy(_assignedHealthBar.gameObject);
    }

    private void HandleRay()
    {
        _ray = _cam.ScreenPointToRay(Input.mousePosition);
    }
    
    private void HandleControl()
    {
        _targetMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 
            Input.GetAxisRaw("Vertical")) * maxSpeed;
        _movement = Vector3.Lerp(_movement, _targetMovement, Time.deltaTime * acceleration);

        _movement = UtilityForVector.ClampVector(_movement, maxSpeed);
        rb.velocity = _movement;
    }

    private void HandleAttack()
    {
        if (_currentAttackCd >= 0f)
        {
            _currentAttackCd -= Time.deltaTime;
            return;
        }
        
        if (Input.GetMouseButtonDown(1) && Physics.Raycast(_ray, out RaycastHit hit))
        {
            Instantiate(attackPrefab, transform);
            Debug.Log("Attack!");
            _currentAttackCd = attackCooldown;
        }
    }

    private void HandleRotation()
    {
        if (Physics.Raycast(_ray, out RaycastHit hit))
        {
            transform.LookAt(hit.point, Vector3.up);
        }
    }

    private void HandleHPBarMovement()
    {
        if (_assignedHealthBar) _assignedHealthBar.transform.position = healthBarPosition.position;
    }

    // private void HandleRush()
    // {
    //     if (!Input.GetMouseButtonDown(0)) { return; }
    //     
    //     rb.AddForce(_movement.normalized * rushForce, ForceMode.Impulse);
    // }
    //
    // private void HandleJump()
    // {
    //     if (!Input.GetKeyDown(KeyCode.Space) || !_canJump) { return; }
    //     
    //     rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //     _canJump = false;
    // }

    // private void OnCollisionEnter(Collision other)
    // {
    //     _canJump = true;
    // }

    public void TakeDamage(int amount)
    {
        _health -= amount;
        if (_assignedHealthBar) _assignedHealthBar.UpdateBar(amount);

        if (_health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void AssignHealthBar(HealthBar healthBar)
    {
        _assignedHealthBar = healthBar;
        healthBar.SetMaxValue(maxHealth);
        healthBar.UpdateBar(maxHealth - _health);
        healthBar.transform.position = healthBarPosition.position;
    }
}
