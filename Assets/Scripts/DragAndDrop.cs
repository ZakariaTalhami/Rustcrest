using UnityEngine;

public class DragAndDrop : MonoBehaviour {

    private GameObject target;    
    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
           MouseClickEvent clickEvent = MouseUtil.GetMousePositionInWorld();
           if(clickEvent.hitGameObject.tag == "Intractable") {
               target = clickEvent.hitGameObject;
               target.GetComponent<Collider>().enabled = false;
               Debug.Log("Picked Up " + target.name);
           } 
        }
        if(Input.GetMouseButtonUp(0)) {
            if(target) {
                MouseClickEvent clickEvent = MouseUtil.GetMousePositionInWorld();
                target.transform.position = clickEvent.point;
                target.GetComponent<Collider>().enabled = true;
                Debug.Log("Dropped " + target.name);
                target = null;
            }
        }

        if(target != null) {
            MouseClickEvent clickEvent = MouseUtil.GetMousePositionInWorld();
            target.transform.position = clickEvent.point + new Vector3(0, 0.2f, 0);
        }
    }
}