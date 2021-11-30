using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    public Transform playerCamera;

    public float maxFiringDistance = 100f;
    public int maxAmmo;
    public int currentAmmo;
    public int gunPower = 20;
    
    // UI 
    [SerializeField] public Text ammoText;
    
    

    // Start is called before the first frame update
    void Start()
    {
        maxAmmo = 10;
        currentAmmo = maxAmmo;
        ammoText.text = currentAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(currentAmmo > 0)
            {
                FireGun();
            }
            
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadTime());
        }

        if(currentAmmo < 0)
        {
            currentAmmo = 0;
            ammoText.text = currentAmmo.ToString();
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
        UseAmmo();
    }

    public void OnFireButtonPress()
    {
        FireGun();
    }
    
    public void OnReloadButtonPress()
    {
        StartCoroutine(ReloadTime());
    }

    void UseAmmo()
    {
        currentAmmo--;
        ammoText.text = currentAmmo.ToString();
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator ReloadTime()
    {
        ammoText.text = "R";
        yield return new WaitForSeconds(1f);
        Reload();
    }
}
