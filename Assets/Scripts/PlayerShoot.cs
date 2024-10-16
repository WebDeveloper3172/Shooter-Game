using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot instance;

    [SerializeField] private int damage = 10;
    [SerializeField] private int range = 100;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource shootSound;

    [SerializeField] private GameObject playerprojectilePrefab;
    [SerializeField] private float playerprojectileForce = 80f;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        shootSound.Play();

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.TryGetComponent(out EnemyLife target))
            {
                target.TakeDamage(damage);
            }
        }
        InstantiatePlayerProjectile();
    }
    private void InstantiatePlayerProjectile()
    {
        GameObject playerprojectile = Instantiate(playerprojectilePrefab , muzzleFlash.transform.position , Quaternion.identity);
        Rigidbody rb = playerprojectile.GetComponent<Rigidbody>();
        rb.AddForce(muzzleFlash.transform.forward * playerprojectileForce , ForceMode.Impulse);

        Destroy(playerprojectile, 3f);
    }
}
