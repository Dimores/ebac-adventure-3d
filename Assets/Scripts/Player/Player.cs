using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    public CharacterController characterController;
    public Transform characterTransform;
    public Animator animator;

    [Header("Movement Settings")]
    public float speed = 1f; 
    public float turnSpeed = 1f; 
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;
    public float runSpeed = 1.5f;

    [Header("Health")]
    public HealthBase healthBase;
    public float timeToRespawn = 2f;
    public string respawnText = "Reviveu no Checkpoint";

    [Header("Colliders")]
    [SerializeField] private List<Collider> colliders;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    private float vSpeed = 0f; 
    private string run = "Run";
    private string jump = "Jump";

    private bool _isDead = false;

    #region PROPERTIES
    public bool IsDead {  get { return _isDead; } }
    #endregion

    #region UNITY_METHODS

    private void OnValidate()
    {
        if(healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += Kill;

        _isDead = false;
    }
    void Update() {

        if (_isDead) return;

        characterTransform.Rotate(0, Input.GetAxisRaw("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxisRaw("Vertical"); 
        var speedVector = characterTransform.forward * inputAxisVertical * speed;

        if(characterController.isGrounded) {
            vSpeed = 0f;
            if (Input.GetButtonDown(jump)) {
                vSpeed = jumpSpeed;
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        characterController.Move(Input.GetButton(run) ? speedVector * runSpeed * Time.deltaTime : speedVector * Time.deltaTime); 

        animator.SetBool(run, inputAxisVertical != 0);
        animator.speed = Input.GetButton(run) ? runSpeed : 1f;

        if (inputAxisVertical != 0)
            animator.SetFloat("AnimSpeed", inputAxisVertical < 0 ? -1f : 1f);
    }
    #endregion

    #region LIFE
    public void Damage(HealthBase h)
    {
        flashColors.ForEach(flashColor => flashColor.Flash());
        EffectsManager.Instance.ChangeVignette();
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }

    private void Kill(HealthBase h)
    {
        if(_isDead == false)
        {
            _isDead = true;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            if (CheckpointManager.Instance.HasCheckpoint())
                Invoke("Revive", timeToRespawn);
            else
                Debug.Log("Morreu sem checkpoint, fim de jogo");
        } 
    }
    #endregion

    #region RESPAWN
    public void Respawn()
    {
        characterController.enabled = false;
        transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        characterController.enabled = true;

        animator.SetTrigger("Revive");

        CheckpointManager.Instance.ShowCheckpointOnUI(respawnText);
    }

    private void Revive()
    {
        healthBase.ResetLife();

        _isDead = false;
        colliders.ForEach(i => i.enabled = true);

        Respawn();
    }
    #endregion
}
