using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    #region Singleton
    private static ZoneManager _instance;
    public static ZoneManager Instance { get { return _instance; } }

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
    
    private Enclosure.EnclosureAnimal CurrentEnclosure;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCurrentEnclosure(Enclosure.EnclosureAnimal animal)
    {
        CurrentEnclosure = animal;
        
        
    }
}
