using UnityEngine;
using System.Collections;


public class ControllerInteractions : MonoBehaviour {
    int left_index, right_index;
    Transform right_linked, left_linked;
    public Transform right_controller, left_controller;

    void Start() {
        left_index = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        right_index = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
        right_linked = null;
        left_linked = null;
    }

    // Update is called once per frame
    void Update() {
        /* TODO: Let go of any released objects */
        if (right_index != -1 && SteamVR_Controller.Input(right_index).GetPressUp(SteamVR_Controller.ButtonMask.Trigger) && right_linked != null)
            releaseObject(right_linked, true);

        if (left_index != -1 && SteamVR_Controller.Input(left_index).GetPressUp(SteamVR_Controller.ButtonMask.Trigger) && left_linked != null)
            releaseObject(left_linked, false);

        /* Link to any new objects */
        if (right_index != -1 && SteamVR_Controller.Input(right_index).GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && right_linked == null)
            linkObject(right_controller, true);

        if (left_index != -1 && SteamVR_Controller.Input(left_index).GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && left_linked == null)
            linkObject(left_controller, false);

        /* TODO: Update position of any previously linked objects */
       // if (right) pullObject(right_index); // TEMPORARY
       // if (left) pullObject(left_index); // TEMPORARY

    }

    void linkObject (Transform controller, bool right)
    {
        Ray controller_direction = new Ray(controller.position, controller.forward);
        RaycastHit hit;
        if (Physics.Raycast(controller_direction, out hit, 30000)) {
            print("HIT " + hit.transform.name);
            if (right)
            {
                right_linked = hit.transform;
                right_linked.parent = right_controller;
            }
            else {
                left_linked = hit.transform;
                left_linked.parent = left_controller;
            }
        }

            // figure out what you're pointing at, get that gameobject's transform
            // highlight object
            // set global
        }

    void pullObject (int device_index) {


    }

    void releaseObject (Transform linked_object, bool right)
    {
        linked_object.parent = null;
        if (right)
            right_linked = null;
        else
            left_linked = null;
    }
    // TODO: make tree come towards me
    // TODO: add haptic feedback


}
