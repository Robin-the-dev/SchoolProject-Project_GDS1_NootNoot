using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBeakTween : MonoBehaviour
{
  public bool isStretching = false;

  private Vector3 initialSize;

  public float max_stretch = 3.0f;
  private float stretch_distance = 0.0f;
  public float stretch_time = 0.1f;

  [SerializeField]
  private Collider beak_collider;
  [SerializeField]
  private Collider body_collider;
  [SerializeField]
  private TestPoleVault pole_vault_script;

  [SerializeField] private PlayerAudio _playerAudio;


  void Start()
  {
    //beak_collider = GetComponentInChildren<CapsuleCollider>(); // Change Collider type when model changes and collider is a different type
    initialSize = transform.localScale;
    Physics.IgnoreCollision(beak_collider, body_collider, true);
    LeanTween.init(5000);
  }


  void Update()
  {
    //If player left clicks mouse, then start stretching
    if (Input.GetMouseButton(0) && !pole_vault_script.isVaulting)
    {
      //disable and reenable collider so raycast can travel through
      beak_collider.enabled = false;
      //Ray cast used to gage distance between the beak and a wall.
      //if the distance is shorter than the max length that the beak can stretch_time,
      //Change the stretch length to that distance
      RaycastHit hit_object;
      if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit_object))
      {
        //Debug.DrawLine(transform.position, hit_object.point, Color.red, 10.0f);
        //if Raycast hits a wall
        if (hit_object.collider.gameObject.tag == "Wall")
        {
          if ((hit_object.distance*2) <= max_stretch){
            Debug.Log(hit_object.distance);
            stretch_distance = (hit_object.distance*2);
          }
          else{
            stretch_distance = max_stretch;
          }
        }
        // if Raycast doesnt hit a wall
        else
        {
          stretch_distance = max_stretch;
        }
      }
      // if Raycast doesnt hit anything at all
      else
      {
        stretch_distance = max_stretch;
      }

      beak_collider.enabled = true;
      isStretching = true;
    }
    else
      isStretching = false;
    //Stretch beak to the stretch_distance, otherwise return back to normal size
    if (isStretching)
    {
      LeanTween.scale(gameObject,initialSize + new Vector3(0.0f,0.0f,stretch_distance),stretch_time);
    }
    else if (!PauseManager.paused)
    {
      LeanTween.scale(gameObject,initialSize,stretch_time);
    }
  }
}
