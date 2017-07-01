using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    public float JumpStrength = 1.0f;
    public float MoveSpeed = 1.0f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Plane playerPlane = new Plane(Vector3.back, transform.position);
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        float d;
        if(playerPlane.Raycast(mouseRay, out d))
        {
            Vector3 hitPoint = mouseRay.GetPoint(d);

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, (hitPoint - transform.position).normalized, out hit, 100))
                {
                    TerrainGen.SetBlock(hit, new BlockAir());
                }
            }
            Debug.DrawRay(transform.position, (hitPoint - transform.position).normalized, Color.red);
        }

        Debug.DrawRay(Camera.main.transform.position, mouseRay.direction, Color.yellow);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * JumpStrength, ForceMode.Impulse);
        }


        //rot = new Vector2(rot.x + Input.GetAxis("Mouse X") * 3, rot.y + Input.GetAxis("Mouse Y") * 3);
        //
        //transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
        //transform.localRotation *= Quaternion.AngleAxis(rot.y, Vector3.left);

        //transform.position += transform.forward * 3 * Input.GetAxis("Vertical");
        //transform.position += transform.right * Input.GetAxis("Horizontal");
        _rb.AddForce((transform.right * MoveSpeed) * Input.GetAxis("Horizontal"));
    }
}