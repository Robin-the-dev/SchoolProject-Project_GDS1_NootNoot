using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class CheckpointManager : MonoBehaviour
{
    #region Singleton
    private static CheckpointManager _instance;
    public static CheckpointManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }
    #endregion
    
    private Vector3 currentCheckpoint;

    [SerializeField] private Transform player;

    private Vector3 start;
    // Start is called before the first frame update
    void Start()
    {
        currentCheckpoint = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void teleportToCurrCp()
    {
        PlayerMovement.captured = false;
        player.position = currentCheckpoint;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //Debug.Log("tp");
        AnalyticsEvent.LevelFail("L1", 0);
    }

    public void UpdateCurrentCheckpoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }
}
