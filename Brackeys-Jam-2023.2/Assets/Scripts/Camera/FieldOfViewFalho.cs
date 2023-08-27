using UnityEngine;
using CodeMonkey.Utils;

public class FieldOfViewFalso : MonoBehaviour
{   
    public bool temPeixe = false;
    Mesh area;
    Vector3 origin;
    [SerializeField] private LayerMask layermask;
    public LayerMask pegaPeixe;
    [SerializeField]float fov = 90f;
    float startingAngle;
    [SerializeField]float viewDistance = 50f;

    public GameObject peixeAtingido;
    // Start is called before the first frame update
    void Start()
    {   
        area = new Mesh();
        GetComponent<MeshFilter>().mesh = area;
        origin = Vector3.zero;
    }

    private void Update() 
    {
        int raycount = 50;
        float angle = 0f;
        float angleIncrease = fov/raycount;

        Vector3[] vertices = new Vector3[raycount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[raycount * 3];

        vertices[0] = origin;
        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i<=raycount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, UtilsClass.GetVectorFromAngle(angle), viewDistance, layermask);
            temPeixe = AchouPeixe(pegaPeixe, angle);            
            if (raycastHit2D.collider == null)
            {
                //No Hit
                vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                //Hit something
                Debug.Log("FOV bateu em algo");
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

        area.vertices = vertices;
        area.uv = uv;
        area.triangles = triangles;
        area.RecalculateBounds();
    }

    public void SetOrigin (Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection (Vector3 aimDirection)
    {
        startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) - fov/2f;
    }

    public void SetFOV(float fov)
    {
        this.fov=fov;
    }

    public void SetViewDistance(float ViewDistance)
    {
        this.viewDistance = ViewDistance;
    }

    private bool AchouPeixe(LayerMask pegaPeixe, float angle)
    {   
            RaycastHit2D CaçaPeixe = Physics2D.Raycast(transform.position, UtilsClass.GetVectorFromAngle(angle), viewDistance, pegaPeixe);
            if(CaçaPeixe.collider == null)
            {
                return false;
            }
            else
            {
                Debug.Log(CaçaPeixe.collider.name);
                return true;
            }
    }

}
