using UnityEngine;
using System.Collections;

public class Modify : MonoBehaviour
{
    Vector2 rot;

    void Update()
    {
        Plane playerPlane = new Plane(Vector3.back, transform.position);
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        float d;
        if(playerPlane.Raycast(mouseRay, out d))
        {
            Vector3 hitPoint = mouseRay.GetPoint(d);

            if (Input.GetKeyDown(KeyCode.Space))
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
        

        //rot = new Vector2(rot.x + Input.GetAxis("Mouse X") * 3, rot.y + Input.GetAxis("Mouse Y") * 3);
        //
        //transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
        //transform.localRotation *= Quaternion.AngleAxis(rot.y, Vector3.left);

        //transform.position += transform.forward * 3 * Input.GetAxis("Vertical");
        transform.position += transform.right * Input.GetAxis("Horizontal");
    }
}