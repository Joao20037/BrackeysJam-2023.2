using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.VisualScripting;

public class FieldOfView : MonoBehaviour
{
    Mesh mesh = new Mesh();
    Vector3 origin;
    [SerializeField] private LayerMask layermask;
    float fov;
    float startingAngle;
    // Start is called before the first frame update
    void Start()
    {   
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        fov = 90f;
    }

    private void Update() {
        int raycount = 50;
        float angle = 0f;
        float angleIncrease = fov/raycount;
        float viewDistance = 50f;

        Vector3[] vertices = new Vector3[raycount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[raycount * 3];

        vertices[0] = origin;
        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i<=raycount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), viewDistance, layermask);
            if (raycastHit2D.collider == null)
            {
                //No Hit
                vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                //Hit something
                vertex = raycastHit2D.point;
            }
            vertices[vertexIndex] = vertex;
            if(i>0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            
            vertexIndex++;
            angle-=angleIncrease;
        }

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetOrigin (Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection (Vector3 aimDirection)
    {
        startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) - fov/2f;
    }
}
