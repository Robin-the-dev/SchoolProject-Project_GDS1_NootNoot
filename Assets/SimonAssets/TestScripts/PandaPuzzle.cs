using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaPuzzle : MonoBehaviour
{
    public Material red_mat;
    public Animator bars_anim;
    public Animator panda_anim;
    public Animator panda_roll;
    public Animator comrade_hat;
    private Renderer pad_renderer;
    // Start is called before the first frame update
    void Start()
    {
        pad_renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            pad_renderer.material = red_mat;
            transform.localPosition = new Vector3(0.0f,0.36f,0.0f);
            bars_anim.SetBool("isOpen", true);
            StartCoroutine(PandaDelay());
            Destroy(GetComponent<Collider>());
        }
    }
    IEnumerator PandaDelay()
    {
        yield return new WaitForSeconds(1.5f);
        panda_anim.SetBool("isMove", true);
        panda_roll.SetBool("isRoll", true);
        yield return new WaitForSeconds(23.0f);
        panda_roll.SetBool("isRoll", false);
        yield return new WaitForSeconds(1.5f);
        comrade_hat.SetBool("isDone", true);
    }
}
