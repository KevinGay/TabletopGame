using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParentGameObject : MonoBehaviour {

    void destroyObject()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    } 
}
