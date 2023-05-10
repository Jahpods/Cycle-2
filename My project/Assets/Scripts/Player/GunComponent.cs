using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunComponent : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField]
    private LayerMask playerMask;
    [SerializeField]
    private LayerMask boxMask;
    private RaycastHit target;

    [Header("Visuals")]
    [SerializeField]
    private LineRenderer lr;
    [SerializeField]
    private Color[] colours;

    [SerializeField]
    private Transform gun;
    [SerializeField]
    private Transform shoot;
    [SerializeField]
    private Image crosshair;

    [SerializeField]
    private MeshRenderer[] mr;

    [Header("Pickup Settings")]
    [SerializeField]
    private Transform heldArea;
    [SerializeField]
    private Vector3 heldPosition;
    private GameObject heldObj;
    private Rigidbody heldObjRb;

    [Header("Physics Settings")]
    [SerializeField]
    private float pickUpRange = 5.0f;
    [SerializeField]
    private float pickUpSizeMax = 1.0f;
    [SerializeField]
    private float pickUpStrength = 150.0f;
    [SerializeField]
    private PhysicMaterial[] pm;

    private AudioManager am;

    void Start(){
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //lr.SetPosition(1,shoot.position);
        lr.SetPosition(1,gun.position);
        target = Intersect();

        if(Input.GetMouseButton(0) || Input.GetMouseButton(1) || heldObj != null){
            am.Play("laser");
        }else{
            am.Pause("laser");
        }

        //Handle Visuals
        if(heldObj != null){
            if(Input.GetMouseButton(0)){
                ChangeColours(0);
            }else if(Input.GetMouseButton(1)){
                ChangeColours(1);
            }else{
                ChangeColours(2);
            }
            lr.SetPosition(1, heldObj.transform.position);
            gun.forward = Vector3.Lerp(gun.forward, heldObj.transform.position - gun.position, Time.deltaTime * 200f);
            //Handle Crosshair
            crosshair.color = colours[3];
        }else{
            bool isShooting = false;
            if(Input.GetMouseButton(0)){
                ChangeColours(0);;
                isShooting = true;
            }else if(Input.GetMouseButton(1)){
                ChangeColours(1);
                isShooting = true;
            }

            if(isShooting){
                if(target.collider != null){
                    lr.SetPosition(1, target.point);
                    gun.forward = target.point - gun.position;
                }else{
                    Vector3 dir = Camera.main.transform.position + Camera.main.transform.forward * 100f;
                    lr.SetPosition(1, dir);
                    gun.forward = dir - gun.position;
                }

            }else{
                gun.forward = Vector3.Lerp(gun.forward, Camera.main.transform.forward, Time.deltaTime * 1f);
            }

            if(canPickUp()){
                crosshair.color = colours[4];
            }else{
                crosshair.color = colours[5];
            }
        }

        lr.SetPosition(0, gun.position);

        //Handle Gun Shooting
        if((target.collider != null && (target.collider.gameObject.CompareTag("GrowingObject") || target.collider.gameObject.CompareTag("Enemy"))) || heldObj != null){
            //Handle Growing and Shrinking of Objects
            GameObject obj = heldObj == null ? target.transform.gameObject : heldObj;
            if(Input.GetMouseButton(0)){
                obj.transform.GetComponent<IGrowable>().Grow();
            }else if(Input.GetMouseButton(1)){
                obj.transform.GetComponent<IGrowable>().Shrink();
            }
        } 


        //Handle Picking Up objects
        // Input.GetMouseButtonDown(2) OLD CONTROLS MIDDLE CLICK TO PICK UP
        if(Input.GetMouseButtonDown(2)){
            if(heldObj == null && canPickUp()){
                PickUpObject(target.transform.gameObject);
            }else if(heldObj != null){
                DropObject();
            }

        }

        //Handle heldArea movement
        RaycastHit walls = IntersectSansBoxes();
        if(heldObj != null){
            if(Vector3.Distance(Camera.main.transform.position, walls.point) >= heldPosition.z){
                heldArea.localPosition = heldPosition;
            }else if(Vector3.Distance(Camera.main.transform.position, walls.point) < heldObj.transform.localScale.x + 1f){
                heldArea.localPosition = new Vector3(0,0, heldObj.transform.localScale.x + 1f);
            }else{
                heldArea.position = walls.point;
            }
            
        }else{
            heldArea.localPosition = heldPosition;
        }

        //Moving Picked Up Objects
        if(heldObj != null){
            // 
            if((Vector3.Distance(heldObj.transform.position, heldArea.position) > 2.1f && heldObjRb.velocity.magnitude < 0.5f)|| 
                averageVector(heldObj.GetComponent<IGrowable>().scaleFactor) >= pickUpSizeMax){
                DropObject();
            }  
        }
    }

    void FixedUpdate(){
        if(heldObj != null){
            // 
            if((Vector3.Distance(heldObj.transform.position, heldArea.position) > 2.1f && heldObjRb.velocity.magnitude < 0.5f)|| 
                averageVector(heldObj.GetComponent<IGrowable>().scaleFactor) >= pickUpSizeMax){
                DropObject();
            }else{
                MoveObject();
            }   
        }
    }

    void ChangeColours(int i){
        lr.material.SetColor("_Color", colours[i]);

        foreach(MeshRenderer rend in mr){
            rend.material.SetColor("_Color", colours[i]);
        }
    }

    float averageVector(Vector3 vec){
        return (vec.x + vec.y + vec.z) / 3;
    }

    bool canPickUp(){
        if(target.collider !=  null && target.transform.GetComponent<IPickUp>() != null){
            return (Vector3.Distance(target.point, transform.position) < pickUpRange && 
                averageVector(target.transform.GetComponent<IGrowable>().scaleFactor) < pickUpSizeMax);
        }else{
            return false;
        }

    }

    void PickUpObject(GameObject pickObj){
        if(heldObj == null){
            pickObj.GetComponent<IPickUp>().IsHeld = true;
            pickObj.GetComponent<Collider>().material = pm[0];
            Physics.IgnoreCollision(pickObj.GetComponent<Collider>(), GetComponent<Collider>(), true);
            heldObjRb = pickObj.GetComponent<Rigidbody>();
            heldObjRb.useGravity = false;
            heldObjRb.drag = 10;
            heldObjRb.constraints = RigidbodyConstraints.FreezeRotation;

            heldObj = pickObj;
        }
    }

    void DropObject(){
        heldObj.GetComponent<IPickUp>().IsHeld = false;
        heldObj.GetComponent<Collider>().material = pm[1];
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), GetComponent<Collider>(), false);
        heldObjRb.useGravity = true;
        heldObjRb.drag = 0;
        heldObjRb.constraints = RigidbodyConstraints.None;

        heldObj = null;
    }

    void MoveObject(){
        if(Vector3.Distance(heldObj.transform.position, heldArea.position) > 0.1f){
            Vector3 moveDir = heldArea.position - heldObj.transform.position;
            heldObjRb.AddForce(moveDir * pickUpStrength);
        }
    }

    RaycastHit Intersect(){
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, playerMask);
        return hit;
    }

    RaycastHit IntersectSansBoxes(){
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, boxMask);
        return hit;
    }
}
