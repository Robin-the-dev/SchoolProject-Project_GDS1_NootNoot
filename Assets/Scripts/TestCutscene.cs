using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TestCutscene : MonoBehaviour
{
    private PlayableDirector pd;
    public PlayerMovement playerMovement;

    private void Start() {
        pd = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            pd.Play();
            StartCoroutine(enumerator(other));
            //Debug.Log(pd.time);
        }
    }

    private IEnumerator enumerator(Collider collider) {
        playerMovement.enabled = false;
        yield return new WaitForSeconds(12.5f);
        playerMovement.enabled = true;
    }
}
