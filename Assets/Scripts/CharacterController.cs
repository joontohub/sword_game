using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().fireEvents = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
