using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

  private Vector3 start_pos;
  public Vector3 next_pos;
  private bool start = true;
  private int id;
  public float speed;
    // Start is called before the first frame update
    void Start()
    {
        start_pos = transform.localPosition;
        id = LeanTween.moveLocal(gameObject, next_pos, speed).id; //accend
    }

    // Update is called once per frame
    void Update()
    {
        if(!LeanTween.isTweening(id) && start){
          id = LeanTween.moveLocal(gameObject, start_pos, speed).id;
          start = false;
        }
        if (!LeanTween.isTweening(id) && !start){
          id = LeanTween.moveLocal(gameObject, next_pos, speed).id;
          start = true;
        }
    }
}
