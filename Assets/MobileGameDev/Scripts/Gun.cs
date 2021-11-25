using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform playerCamera;

    public float maxFiringDistance = 100f;
    public int maxAmmo;
    public int currentAmmo;
    public int gunPower = 20;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FireGun();
        }
    }

    private bool FireRay(out RaycastHit hit)
    {
        Debug.DrawRay(playerCamera.transform.position,playerCamera.transform.forward*maxFiringDistance,Color.red,1f);
        if(Physics.Raycast(playerCamera.transform.position,playerCamera.transform.forward,out hit, maxFiringDistance))
        {
	        return true;
        }
        return false;

    }

    protected void FireGun()
    {
        RaycastHit hit;
        if(!FireRay(out hit))
        {
            return;
        }
        Debug.Log(hit.transform.name);
        if(hit.transform.CompareTag("Enemy"))
        {
            hit.transform.GetComponent<Enemy>().TakeDamage(gunPower);
        }
    }

    public void OnFireButtonPress()
    {
        FireGun();
    }
}
