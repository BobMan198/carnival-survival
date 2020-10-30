using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;

    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    
    public GunInfo currentGun;
    public GunInfo gunSlot1;
    public GunInfo gunSlot2;
    public GameObject gun1;
    public GameObject gun2;
    public GunInfo startPistol;
    public Camera fpsCam;

    public float playerHealth;
    public float playerMaxHealth;
    public float playerRegen;

    public GameObject gunHolder;

    float verticalLookRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;

    Rigidbody rb;

    PhotonView PV;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();

        SetupGunInfo();

       // gameObject.AddComponent<perks>[Random.Range(0, perks.Length)]().Initialize();
    }

    public void SetupGunInfo()
    {
        currentGun = ScriptableObject.Instantiate(startPistol);
        gun1 = Instantiate(currentGun.gunModel, gunHolder.transform);
        gunSlot1 = currentGun;
    }

    public void Flaws()
    {
        var f = new HashSet<Perk.FlawType>();
        f.Add(Perk.flawType[Random.Range(0, Perk.flawType.Length)]);

        foreach (Perk.FlawType type in f)
        {
        
        switch(type)
        {
            case Perk.FlawType.HealthFlaw:
            gameObject.AddComponent<HealthFlaw>().Initialize();
            break;

            case Perk.FlawType.RegenFlaw:
            gameObject.AddComponent<RegenFlaw>().Initialize();
            break;

            case Perk.FlawType.RangeFlaw:
            gameObject.AddComponent<RangeFlaw>().Initialize();
            break;

            case Perk.FlawType.MagFlaw:
            gameObject.AddComponent<MagFlaw>().Initialize();
            break;

            case Perk.FlawType.RateFlaw:
            gameObject.AddComponent<RateFlaw>().Initialize();
            break;
        }
    }

    }

    public void Perks()
    {
        var h = new HashSet<Perk.PerkType>();
        while(h.Count < 2)
        {
            h.Add(Perk.perkType[Random.Range(0, Perk.perkType.Length)]);
        }

        foreach (Perk.PerkType type in h)
        {
        
        switch(type)
        {
            case Perk.PerkType.HealthPerk:
            gameObject.AddComponent<HealthPerk>().Initialize();
            break;

            case Perk.PerkType.RegenPerk:
            gameObject.AddComponent<RegenPerk>().Initialize();
            break;

            case Perk.PerkType.RangePerk:
            gameObject.AddComponent<RangePerk>().Initialize();
            break;

            case Perk.PerkType.MagPerk:
            gameObject.AddComponent<MagPerk>().Initialize();
            break;

            case Perk.PerkType.RatePerk:
            gameObject.AddComponent<RatePerk>().Initialize();
            break;
        }
    }
        
    }

    void Start()
    {

        Perks();
        Flaws();

        if(PV.IsMine)
        {
            SwitchWeapon();
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }

        //healthDisplay = RoomManager.healthDisplay;
    }

    void Update()
    {
        //Shooting if this is local players photon view
        if(!PV.IsMine)
        {
            return;
        }   
        if(Input.GetButtonDown("Fire1"))
        {
            PV.RPC("RPC_Shooting", RpcTarget.All);
        }
      //  healthDisplay.text = playerHealth.ToString();

        if(!PV.IsMine)
            return;

        Look();
        Move();      
        Jump(); 
        Reload();
        Scope();

        SwitchWeapon();
        PickupItem();
        
    }

    //Walking
    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }

    [PunRPC]
    void RPC_Shooting()
    {
        currentGun.muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, currentGun.itemRange))
        {
            Debug.Log(hit.transform.name);

            if(hit.transform.tag == "Player")
            {
                Debug.Log("Hit player");
                hit.transform.gameObject.GetComponent<PlayerController>().playerHealth -= currentGun.itemDamage;
            }

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                //items[itemIndex].itemGameObject.GetComponentInChildren<Item>();
                target.TakeDamage(currentGun.itemDamage);
            }
            
            GameObject impactGO = Instantiate(currentGun.impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }


    void Reload()
    {

    }

    void Scope()
    {

    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    //Item Switching

    void SwitchWeapon()
    {
       if(Input.GetKeyDown(KeyCode.Alpha1))
       {
           if(gunSlot1 != null)
        {
            gun1.SetActive(true);
            gun2.SetActive(false);
           currentGun = gunSlot1;
        }
      }

        if(Input.GetKeyDown(KeyCode.Alpha2))
       {
           if(gunSlot2 != null)
        {
            gun1.SetActive(false);
            gun2.SetActive(true);
           currentGun = gunSlot2;
        }
       }

       gunHolder.transform.localPosition = currentGun.itemHolderOffset;
    }

    [PunRPC]
    void PickupItem()
    {
        RaycastHit hit;

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 5))
            {
                if(hit.transform.tag == "Gun")
                {
                    Debug.Log("Picked up gun " + hit.collider.gameObject.name);
                    GunInfo gunInfo = hit.transform.gameObject.GetComponent<GunInfoHolder>().gunInfo;
                    gunSlot2 = ScriptableObject.Instantiate(gunInfo);
                    gun2 = Instantiate(gunSlot2.gunModel, gunHolder.transform);
                    gunHolder.transform.position = currentGun.itemHolderOffset;
                    currentGun = gunSlot2;
                    gun1.SetActive(false);
                    gun2.SetActive(true);
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }

    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    void FixedUpdate()
    {
        if(!PV.IsMine)
            return;

        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}
