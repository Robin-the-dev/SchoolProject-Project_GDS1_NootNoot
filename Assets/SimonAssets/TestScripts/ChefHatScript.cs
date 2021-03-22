using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefHatScript : MonoBehaviour
{
    [SerializeField]
    private Transform key;
    private Animator door_anim;
    [SerializeField]
    private GuardAI guard;
    [SerializeField]
    private Transform key_hole;
    // Start is called before the first frame update
    void Start()
    {
        door_anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player" && !guard.hasKey)
        {
            key.parent = key_hole;
            key.localPosition = new Vector3(-0.942f, -0.268f, -0.032f);
            key.localRotation = Quaternion.Euler(218.88f, 260.035f, -13.60199f);
            door_anim.SetBool("isOpen", true);
        }
    }
}
