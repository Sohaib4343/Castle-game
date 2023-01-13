using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickAndDrop : MonoBehaviour
{
    //Animator
    public Animator animator;

    public GameObject sword;

    public Transform handBone;

    //Reference of the PlayerAttack script
    private PlayerAttack playerAttack;
    private ObjectPickAndDrop objectPickAndDrop;

    //Bool to check if the sword is equipped
    public bool isEquiped;

    //To shoot the ray for grabbing the sword
    private float grabRange = 5f;
    public Transform grabPoint;
    private RaycastHit rayHit;
    public LayerMask weaponLayer;
    private bool weapon;

    private Transform cam;

    private void Awake()
    {
        //Script reference
        playerAttack = GetComponent<PlayerAttack>();
        objectPickAndDrop = GetComponent<ObjectPickAndDrop>();

        cam = Camera.main.transform;
    }

    void Update()
    {
        weapon = Physics.Raycast(grabPoint.position, grabPoint.forward, out rayHit, grabRange, weaponLayer);

        Debug.DrawRay(grabPoint.position, grabPoint.forward, Color.green);

        if (weapon && Input.GetKeyDown(KeyCode.E) && !objectPickAndDrop.isGrabbed)
        {
            animator.SetBool("Equip", true);
            animator.SetBool("IsEquiped", true);
            sword.GetComponent<Rigidbody>().useGravity = false; 
            sword.GetComponent<BoxCollider>().enabled = false;
            isEquiped = true;
            sword.transform.parent = handBone;
            sword.transform.localPosition = new Vector3(0.04899964f, -0.3980003f, 0.1420003f);
            sword.transform.localRotation = Quaternion.Euler(-7.194f, 16.491f, 177.541f);

            //Set the layer of the sword to "WeaponEquipped when the player picks up the sword"
            sword.layer = LayerMask.NameToLayer("WeaponEquipped");
            foreach (Transform child in sword.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("WeaponEquipped");
                Debug.Log("Layer changed to WeaponEquipped");
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && isEquiped)
        {
            animator.SetBool("Equip", false);
            isEquiped = false;
            sword.GetComponent<BoxCollider>().enabled = true;
            sword.GetComponent<Rigidbody>().useGravity = true;
            sword.GetComponent<Rigidbody>().AddForce(cam.forward * 4f, ForceMode.Impulse);
            sword.transform.parent = null;

            //Set the layer of the sword to "Weapon when the player drops the sword"
            sword.layer = LayerMask.NameToLayer("Weapon");
            foreach (Transform child in sword.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Weapon");
                Debug.Log("Layer changed to Weapon");
            }

            Debug.Log("Is not equipped");
        }
    }
}
