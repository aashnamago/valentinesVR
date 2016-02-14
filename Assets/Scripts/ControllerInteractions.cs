using UnityEngine;
using System.Collections;


public class ControllerInteractions : MonoBehaviour {
    public int left_index, right_index;
    public Transform right_linked, left_linked; // old_right_parent, old_left_parent;
    public Transform right_controller, left_controller;
    LineRenderer left_line_renderer, right_line_renderer;
    Vector3 left_offset, right_offset; // old_right_local, old_left_local;
    

    void Start() {
        left_index = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        right_index = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);

        right_linked = null;
        left_linked = null;
      //  old_left_parent = null;
       // old_right_parent = null;
        left_offset = Vector3.zero; // useless
        right_offset = Vector3.zero; // useless
        //old_left_local = Vector3.zero;
       // old_right_local = Vector3.zero; 

        left_line_renderer = left_controller.gameObject.AddComponent<LineRenderer>();
        left_line_renderer.material = new Material(Shader.Find("Particles/Additive"));
        left_line_renderer.SetColors(Color.red, Color.red);
        left_line_renderer.SetWidth(0.02F, 0.02F);
        left_line_renderer.SetVertexCount(2);
        left_line_renderer.enabled = false;

        right_line_renderer = right_controller.gameObject.AddComponent<LineRenderer>();
        right_line_renderer.material = new Material(Shader.Find("Particles/Additive"));
        right_line_renderer.SetColors(Color.red, Color.red);
        right_line_renderer.SetWidth(0.02F, 0.02F);
        right_line_renderer.SetVertexCount(2);
        right_line_renderer.enabled = false;
        
        // TODO: haptic feedback
        // TODO: if it's pressed and it's not null, still grab the object
        // set local position under the parent to 0/0/0
    }

    // Update is called once per frame
    void Update() {
        if (right_index != -1 && SteamVR_Controller.Input(right_index).GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && right_linked == null) /* Link to any new objects */
            linkObject(right_controller, true);
        else if (right_index != -1 && SteamVR_Controller.Input(right_index).GetPress(SteamVR_Controller.ButtonMask.Trigger) && right_linked != null)  /* Update any existing linked objects */
            updateTarget(right_controller, true);
        else if (right_index != -1 && SteamVR_Controller.Input(right_index).GetPressUp(SteamVR_Controller.ButtonMask.Trigger) && right_linked != null)   /* Let go of any released objects */
            releaseObject(right_linked, true);


        if (left_index != -1 && SteamVR_Controller.Input(left_index).GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && left_linked == null) /* Link to any new objects */
            linkObject(left_controller, false);
        else if (left_index != -1 && SteamVR_Controller.Input(left_index).GetPress(SteamVR_Controller.ButtonMask.Trigger) && left_linked != null)   /* Update any existing linked objects */
           updateTarget(left_controller, false);
        else if (left_index != -1 && SteamVR_Controller.Input(left_index).GetPressUp(SteamVR_Controller.ButtonMask.Trigger) && left_linked != null)   /* Let go of any released objects */
            releaseObject(left_linked, false);
    }

    void linkObject (Transform controller, bool right)
    {
        Ray controller_direction = new Ray(controller.position, controller.forward);
        RaycastHit hit;
        if (Physics.Raycast(controller_direction, out hit, Mathf.Infinity)) {
            print("hit");
            if (right)
            {
                right_line_renderer.enabled = true;
                right_linked = hit.transform;
                //  old_right_parent = right_linked.parent;
                //     old_right_local = right_linked.localPosition;
                //      right_linked.parent = right_controller; // TODO: start lerping the object towards the controller
                right_linked.position = Vector3.Lerp(right_linked.position, right_controller.position, Time.deltaTime * 1.0F);
                right_line_renderer.SetPosition(0, right_controller.position);
                right_line_renderer.SetPosition(1, hit.point);
                right_offset = hit.point - right_linked.position;
                return;
            }
            left_line_renderer.enabled = true;
            left_linked = hit.transform;
            // old_left_parent = left_linked.parent;
            //old_left_local = left_linked.localPosition;
            // left_linked.parent = left_controller;   // TODO: start lerping the object towards the controller
            left_linked.position = Vector3.Lerp(left_linked.position, left_controller.position, Time.deltaTime * 1.0F);
            left_line_renderer.SetPosition(0, left_controller.position);
            left_line_renderer.SetPosition(1, hit.point);
            left_offset = hit.point - left_linked.position;
        } else
        {
            print("didn't hit");
        }
    }

    void updateTarget(Transform controller, bool right)
    {
        /* TODO: Update position of any previously linked objects */
        if (right)
        {
            right_line_renderer.SetPosition(0, right_controller.position);
            right_line_renderer.SetPosition(1, right_linked.position + right_offset);
            right_linked.position = Vector3.Lerp(right_linked.position, right_controller.position, Time.deltaTime * 1.0F);
            return;
        }
        left_line_renderer.SetPosition(0, left_controller.position);
        left_line_renderer.SetPosition(1, left_linked.position + left_offset);
        left_linked.position = Vector3.Lerp(left_linked.position, left_controller.position, Time.deltaTime * 1.0F);
    }

    void releaseObject (Transform linked_object, bool right)
    {
        if (right)
        {
           // right_linked.localPosition = old_right_local;
         //   right_linked.parent = old_right_parent;
         
            right_linked = null;
            right_line_renderer.enabled = false;
            return;
        }
      //  left_linked.localPosition = old_left_local;
      //  left_linked.parent = old_left_parent;
  
        left_linked = null;
        left_line_renderer.enabled = false;
    }
}
