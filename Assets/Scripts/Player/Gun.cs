using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum GunFireType
    {
        SingleShot,
        FullAuto
    }
    public LayerMask GunLayerMask;
    public float WeaponRange;
    public Transform GunBarrel;
    public GunFireType thisGunFireType;
    public float FireRate;
    public float BulletSize;

    private float fireTimer;

    private void Start()
    {
        fireTimer = FireRate;

    }
    private void Update()
    {
        switch (thisGunFireType)
        {
            case (GunFireType.SingleShot) :
                //pressed left moust button
                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();

                }
                break;

            case (GunFireType.FullAuto):

                if (Input.GetMouseButton(0))
                {
                    if (fireTimer <= 0)
                    {
                        Shoot();
                        fireTimer = FireRate;
                    }
                    else
                    {
                        fireTimer-= Time.deltaTime;
                    }

                }
                break;
        }
     

       
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(GunBarrel.position, GunBarrel.forward, out hit, WeaponRange, GunLayerMask))
        {
            //Debug.DrawRay(GunBarrel.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow, 2.0f);
            //Debug.Log("Did Hit");
            Collider[] colliders = Physics.OverlapSphere(hit.point, BulletSize,GunLayerMask);
            foreach (Collider col in colliders)
            {

                Destroy(col.transform.gameObject);
            }

            
         //   Destroy(hit.transform.gameObject);
        }
        else
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white,2.0f);
            //Debug.Log("Did not Hit");
        }
    }
}
