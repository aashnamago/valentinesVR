using UnityEngine;
using System.Collections;


public class ControllerInteractions : MonoBehaviour {
    int leftIndex, rightIndex;
    Transform rightLinked; // TODO
    Transform leftLinked; // TODO
    bool right; // TEMPORARY
    bool left; // TEMPORARY
  

    void Start() {
        leftIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        rightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
        right = false;
        left = false;
    }

    // Update is called once per frame
    void Update() {
        /* TODO: Let go of any released objects */


        /* Link to any new objects */
        if (rightIndex != -1 && SteamVR_Controller.Input(rightIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            linkObject(rightIndex);
            
        if (leftIndex != -1 && SteamVR_Controller.Input(leftIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            linkObject(leftIndex);

        /* TODO: Update position of any previously linked objects */
        if (right) pullObject(rightIndex);
        if (left) pullObject(leftIndex);

    }

    void linkObject(int device_index)
    {
        if (device_index == rightIndex) right = true;
        if (device_index == leftIndex) left = true;


        // figure out what you're pointing at, get that gameobject's transform
        // highlight object
        // set global... then in update, 
        Vector3 position = SteamVR_Controller.Input(device_index).transform.pos;
        Vector3 direction = SteamVR_Controller.Input(device_index).transform.rot * position;

      //var fwd = transform.TransformDirection(Vector3.forward);
     // if (Physics.Raycast(transform.position, fwd, 50))
      //{
      //    print("There is something in front of the object!");
      //}
       
    }

    void pullObject (int device_index) {
        Vector3 position = SteamVR_Controller.Input(device_index).transform.pos;
        Vector3 direction = SteamVR_Controller.Input(device_index).transform.rot * position;

        Debug.DrawRay(transform.position, transform.forward, Color.red);
      //  Instantiate(prefab, transform.position, Quaternion.identity);
        string toPrint = "hi";
        if (device_index == rightIndex) toPrint = "right";
        if (device_index == leftIndex) toPrint = "left";
        print("Something is happening with " + toPrint);
    }


    // TODO: make tree come towards me
    // TODO: add haptic feedback


}
