using CodeMonkey.Utils;
using UnityEngine;

public class AimHandler : MonoBehaviour
{   
    private Transform aimTransform;
    // Start is called before the first frame update
    private void Awake() 
    {    
        aimTransform = transform.Find("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition-transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles= new Vector3 (0,0,angle);
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shoot");
        }
    }
}
