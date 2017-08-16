using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private Rigidbody _rb;
    private bool _isJumping;
    private bool _isGrounded;
    private float _fallingAcceleration;
    private float _jumpTimer;
    private Vector3 _playerRayOrigin;

    public float MoveSpeed = 0.1f;
    public float JumpTime = 0.11f;
    public float JumpSpeed = 0.2f;
    public float FallSpeed = 0.1f;
    public float MaxFallSpeed = 0.4f;
    public float _fallingAccelerationRate = 0.01f;

    private void Start()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Block Interaction
        Plane playerPlane = new Plane(Vector3.back, transform.position);
        _playerRayOrigin = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit;

        float d;
        if(playerPlane.Raycast(mouseRay, out d))
        {
            Vector3 hitPoint = mouseRay.GetPoint(d);

            if (Input.GetMouseButtonDown(0))
            {
                hit = Physics2D.Raycast(_playerRayOrigin, (hitPoint - _playerRayOrigin).normalized, 100, LayerMask.NameToLayer("Platforms"));
                if (hit.collider != null)
                {
                    TerrainGen.SetBlock(hit, new BlockAir());
                }
            }

            if(Input.GetMouseButtonDown(1))
            {
                hit = Physics2D.Raycast(_playerRayOrigin, (hitPoint - _playerRayOrigin).normalized, 100, LayerMask.NameToLayer("Platforms"));
                if (hit.collider != null)
                {
                    TerrainGen.SetBlock(hit, new Block(), true);
                }
            }
            Debug.DrawRay(_playerRayOrigin, (hitPoint - _playerRayOrigin).normalized, Color.red);
        }

        Debug.DrawRay(Camera.main.transform.position, mouseRay.direction, Color.yellow);
    }

    //private void FixedUpdate()
    //{
    //    // Movement
    //    // Jump
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        //_rb.AddForce(Vector3.up * JumpStrength, ForceMode.Impulse);
    //        if (_isGrounded)
    //        {
    //            _isGrounded = false;
    //            _isJumping = true;
    //            _jumpTimer = JumpTime;
    //        }
    //    }

    //    RaycastHit hit;
    //    if (_isJumping)
    //    {
    //        if (_jumpTimer < 0)
    //        {
    //            // Stop Upward Movement
    //            _isJumping = false;
    //        }
    //        else
    //        {
    //            // Upward Movement
    //            Vector3 jumpDirection = Vector3.up * JumpSpeed;
    //            if (!_rb.SweepTest(jumpDirection, out hit, jumpDirection.magnitude))
    //            {
    //                transform.Translate(jumpDirection);
    //            }
    //            _jumpTimer -= Time.deltaTime;
    //        }
    //    }
    //    else
    //    {
    //        // Falling Movement
    //        Vector3 fallDirection = Vector3.down * _fallingAcceleration;
    //        if (!_rb.SweepTest(fallDirection, out hit, fallDirection.magnitude))
    //        {
    //            transform.Translate(fallDirection);
    //            _fallingAcceleration = Mathf.Min(_fallingAcceleration + _fallingAccelerationRate, MaxFallSpeed);
    //        }
    //        else
    //        {
    //            _isGrounded = true;
    //            _fallingAcceleration = FallSpeed;
    //        }
    //    }

    //    Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal") * MoveSpeed, 0, 0);
    //    if (!_rb.SweepTest(moveDirection, out hit, moveDirection.magnitude))
    //    {
    //        transform.Translate(moveDirection);
    //    }


    //    //rot = new Vector2(rot.x + Input.GetAxis("Mouse X") * 3, rot.y + Input.GetAxis("Mouse Y") * 3);
    //    //
    //    //transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
    //    //transform.localRotation *= Quaternion.AngleAxis(rot.y, Vector3.left);

    //    //transform.position += transform.forward * 3 * Input.GetAxis("Vertical");
    //    //transform.position += transform.right * Input.GetAxis("Horizontal");

    //    //_rb.AddForce((transform.right * MoveSpeed) * Input.GetAxis("Horizontal"));
    //}

    public List<WorldPos> OccupiedBlocks()
    {
        List<WorldPos> pos = new List<WorldPos>();
        pos.Add(TerrainGen.GetBlockPos(_playerRayOrigin));
        pos.Add(new WorldPos(pos[0].x, pos[0].y - 1));
        return pos;
    }
}