using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootZone : MonoBehaviour
{

    Affect affect;
    // Start is called before the first frame update
    void Start()
    {
        affect = new Affect();
        affect.type = Affect.Type.SPEED;
        affect.value = 1.2f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            if(!character.HasAffect(affect))
                character.AddAffect(affect);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            character.RemoveAffect(affect);
        }
    }
}
