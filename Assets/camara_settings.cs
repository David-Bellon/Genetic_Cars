using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara_settings : MonoBehaviour
{
    private float size = 4.0f;

    void Awake()
    {
        Camera.main.orthographicSize = size;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
