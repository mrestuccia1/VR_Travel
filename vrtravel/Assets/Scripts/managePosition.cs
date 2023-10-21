using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

// this class contains information about the world
public class environment
{
    /* ************************************* */
    /* Data structures for world definitions */
    // world object type.
    public enum Type
    {
        floor,
        intersection,
        destination,
        oob // out of bounds
    }

    // a world element with necessary information
    public struct Element
    {
        // stored in x0, z0, x1, z1
        public List<float> coords; // store coords of rect representing element
        public Type type; // stores element type
        public Element[] neighbors; // gives neighbors of element
        public byte pos; // unique position
        public GameObject obj; // the object in the game
        public bool x_coord; // True if traveling on x axis, False if on z axis
        public bool direction; // True if traveling in positive x or z direction
    }

    // elements created for each element in unity world
    public Element loc_a; public Element floor_0;
    public Element int_0; public Element floor_1;
    public Element int_1; public Element floor_2;
    public Element loc_b; public Element floor_3;
    public Element loc_c; public List<Element> elements;
    public Element outside;

    /* Create World Environment              */

    // function to build the list. too much toil for this
    public void initList()
    {
      elements = new List<Element>();

      elements.Add(loc_a);
      elements.Add(loc_b);elements.Add(loc_c);

      elements.Add(int_0); elements.Add(int_1);

      elements.Add(floor_0); elements.Add(floor_1);
      elements.Add(floor_2); elements.Add(floor_3);

      elements.Add(outside);
    }

    // the heavy lifting. sets the correct value of each
    // element
    public void initWorld()
    {
      elements = new List<Element>();

    // Locations \\
    // loc_a
    List<float> la_coords = new List<float>() {-0.5f, -0.5f, 0.5f, 0.5f};
    Element[] la_n = new Element[] {floor_0};
    loc_a = new Element();
    loc_a.coords = la_coords; loc_a.neighbors = la_n;
    loc_a.type = Type.destination; loc_a.pos = 0;
    loc_a.x_coord = false; loc_a.direction = false;
    // loc_b
    List<float> lb_coords = new List<float>() {3.5f, -0.5f, 4.5f, 0.5f};
    Element[] lb_n = new Element[] {floor_2};
    loc_b = new Element();
    loc_b.coords = lb_coords; loc_b.neighbors = lb_n;
    loc_b.type = Type.destination; loc_b.pos = 6;
    loc_b.x_coord = false; loc_b.direction = false;

    // loc_c
    List<float> lc_coords = new List<float>() {3.5f, 6.5f, 4.5f, 7.5f};
    Element[] lc_n = new Element[] {floor_3};
    loc_c = new Element();
    loc_c.coords = lc_coords; loc_c.neighbors = lc_n;
    loc_c.type = Type.destination; loc_c.pos = 8;
    loc_c.x_coord = false; loc_c.direction = false;

    // Intersections \\
    // int 0
    List<float> i0_coords = new List<float>() {-0.5f, 4.5f, 0.5f, 5.5f};
    Element[] i0_n = new Element[] {floor_0, floor_1};
    int_0 = new Element();
    int_0.coords = i0_coords; int_0.neighbors = i0_n;
    int_0.type = Type.intersection; int_0.pos = 2;
    int_0.x_coord = false; int_0.direction = false;
    // int 1
    List<float> i1_coords = new List<float>() {3.5f, 4.5f, 4.5f, 5.5f};
    Element[] i1_n = new Element[] {floor_1, floor_2, floor_3};
    int_1 = new Element();
    int_1.coords = i1_coords; int_1.neighbors = i1_n;
    int_1.type = Type.intersection; int_1.pos = 4;
    int_1.x_coord = false; int_1.direction = false;

    // Floors \\
    List<float> f0_coords = new List<float>() {-0.5f, 0.5f, 0.5f, 4.5f};
    Element[] f0_n = new Element[] {loc_a, int_0};
    floor_0 = new Element();
    floor_0.coords = f0_coords; floor_0.neighbors = f0_n;
    floor_0.type = Type.floor; floor_0.pos = 1;
    floor_0.x_coord = false; floor_0.direction = false;

    List<float> f1_coords = new List<float>() {0.5f, 4.5f, 3.5f, 5.5f};
    Element[] f1_n = new Element[] {int_0, int_1};
    floor_1 = new Element();
    floor_1.coords = f1_coords; floor_1.neighbors = f1_n;
    floor_1.type = Type.floor; floor_1.pos = 3;
    floor_1.x_coord = false; floor_1.direction = false;

    List<float> f2_coords = new List<float>() {3.5f, 0.5f, 4.5f, 4.5f};
    Element[] f2_n = new Element[] {loc_b, int_1};
    floor_2 = new Element();
    floor_2.coords = f2_coords; floor_2.neighbors = f0_n;
    floor_2.type = Type.floor; floor_2.pos = 5;
    floor_2.x_coord = false; floor_2.direction = false;

    List<float> f3_coords = new List<float>() {3.5f, 5.5f, 4.5f, 6.5f};
    Element[] f3_n = new Element[] {loc_c, int_1};
    floor_3 = new Element();
    floor_3.coords = f3_coords; floor_3.neighbors = f3_n;
    floor_3.type = Type.floor; floor_3.pos = 7;
    floor_3.x_coord = false; floor_3.direction = false;

    List<float> out_coords = null; Element[] out_n = null;
    outside = new Element();
    outside.coords = out_coords; outside.neighbors = out_n;
    outside.type = Type.oob; outside.pos = 9;
    outside.x_coord = false; outside.direction = false;
    }
}

// this class is pretty much copied from given code - allows us
// to interact with the devices
public class devices{
    public UnityEngine.XR.InputDevice HeadsetDevice;
    public UnityEngine.XR.InputDevice LeftControllerDevice;
    public UnityEngine.XR.InputDevice RightControllerDevice;
    public List<UnityEngine.XR.InputDevice> trackedDevices;

    public void initDevices()
    {
      trackedDevices = new List<UnityEngine.XR.InputDevice>();
      var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.TrackedDevice;
      UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, trackedDevices);

      List<UnityEngine.XR.InputDevice> headsetDevices = new List<UnityEngine.XR.InputDevice>();
      UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeadMounted, headsetDevices);
      if (headsetDevices.Count > 0)
      {
          HeadsetDevice = headsetDevices[0];
      }

      List<UnityEngine.XR.InputDevice> leftControllerDevices = new List<UnityEngine.XR.InputDevice>();
      UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.Left, leftControllerDevices);
      if (leftControllerDevices.Count > 0)
      {
          LeftControllerDevice = leftControllerDevices[0];
      }

      List<UnityEngine.XR.InputDevice> rightControllerDevices = new List<UnityEngine.XR.InputDevice>();
      UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.Right, rightControllerDevices);
      if (rightControllerDevices.Count > 0)
      {
          RightControllerDevice = rightControllerDevices[0];
      }
    }
}

// this class does the heavy lifting. makes (most of) the 
// process run
public class managePosition : MonoBehaviour
{
    // initialize environment
    environment environ = new environment();
    devices devs;
    public AudioSource turnLeftAudio;
    public AudioSource turnRightAudio;
    public AudioSource stopAudio;
    public AudioSource arrivedAudio;
    public AudioSource forwardAudio;
    public AudioSource backInPathAudio;
    public string prevInstruction;


    // assign game objects to each element
    public GameObject loca; public GameObject floor0;
    public GameObject int0; public GameObject floor1;
    public GameObject int1; public GameObject floor2;
    public GameObject locb; public GameObject floor3;
    public GameObject locc;

    // text mesh variable to change text on Instructions HUD
    public TextMeshProUGUI instruction;

    // definition of variables to be changed
    public GameObject player; // the camera
    public GameObject path_0; public GameObject path_1;
    public GameObject path_2; public GameObject path_3;

    // define trail struct
    public List<environment.Element> trail;


    // adjusts opacity of game object, just used to make paths
    // visible / invisible
    void toggleVis(GameObject path, float opacity)
    {
      Color color = path.GetComponent<Renderer>().material.color;
      Color newColor = new Color(color.r, color.g, color.b, opacity);
      path.GetComponent<Renderer>().material.SetColor("_Color", newColor);
    }

    // function to make stuff invisible
    public void makeInvis() {
      toggleVis(path_0, 0f); toggleVis(path_1, 0f);
      toggleVis(path_2, 0f); toggleVis(path_3, 0f);
    }

    // this function takes a trail and makes the path visible
    public void visTrail(List<environment.Element> trail) {
      for (int i = 0; i < trail.Count; i++) {
        if (trail[i].type == environment.Type.floor) {
          GameObject path;
          switch (trail[i].pos) {
            case 1:
              path = path_0;
              break;
            case 3:
              path = path_1;
              break;
            case 5:
              path = path_2;
              break;
            default:
              path = path_3;
              break;
          }
          toggleVis(path, 1f);
        }
      }
    }

    // Start is called before the first frame update
    void Start()
    {
        turnLeftAudio.Play();
        devs = new devices();
        devs.initDevices();
        // initialize world
        environ.initWorld();

        environ.loc_a.obj = loca; environ.loc_b.obj = locb;
        environ.loc_c.obj = locc; environ.int_0.obj = int0;
        environ.int_1.obj = int1; environ.floor_0.obj = floor0;
        environ.floor_1.obj = floor1; environ.floor_2.obj = floor2;
        environ.floor_3.obj = floor3;

        environ.initList();

        makeInvis();
    }

    // clamp function. if the given value (B) is outside
    // the bounds of the clamp, set it equal to the closest
    // extrema. otherwise return B
    float CLAMP(float A, float B, float C)
    {
        if (B > C) return C;
        if (A > B) return A;
        return B;
    }
    
    // linear interpolation function. a = (val - imin)/(imax - imin);
    // result = (1 - a)*omin + a * omax;
    float LERP(float omin, float omax, float imin, float val, float imax)
    {
      float alpha = (val - imin) / (imax - imin);
      return omax * alpha + (1-alpha) * omin;
    }

    // function working great. returns position (hard coded pos # in struct)
    // no inputs required
    byte findPos(List<environment.Element> trail)
    {
      float player_posx = player.transform.position.x;
      float player_posz = player.transform.position.z;

      for (int i=0; i<trail.Count; i++)
      {
        environment.Element el = trail[i];

        // el.coords are stored as [z0, x0, z1, x1]
        bool within_x = ((el.coords[1] <= player_posx) && (player_posx <= el.coords[3]));
        bool within_z = ((el.coords[0] <= player_posz) && (player_posz <= el.coords[2]));

        if (within_x && within_z) {
          return (byte)i;
        }

      }
      // take player location, return element (from environ) user is in
      return 9;
    }

    // this function sends an impulse due to certain instructions
    public void SendImpulse(float amplitude, float duration, string instruction)
    {
      foreach (var device in devs.trackedDevices)
      {
        if (instruction == "left") {
          devs.LeftControllerDevice.SendHapticImpulse(0u, amplitude, duration);
        }else if (instruction == "right") {
          devs.RightControllerDevice.SendHapticImpulse(0u, amplitude, duration);
        }else{
          if (device.TryGetHapticCapabilities(out var capabilities) &&
              capabilities.supportsImpulse)
          {
              device.SendHapticImpulse(0u, amplitude, duration);
          }
        }
      }

    }

    void playSound(string prevInstr) {
      string curInstr = instruction.text;
      if (prevInstr != curInstr) {
        if (curInstr == "Turn Right.") {
          stopAudio.Play();
          turnRightAudio.Play();
        }else if (curInstr == "Turn Left.") {
          stopAudio.Play();
          turnLeftAudio.Play();
        }else if (curInstr == "Walk forward.") {
          forwardAudio.Play();
        }else if (curInstr == "Arrived.") {
          arrivedAudio.Play();
        }else if (curInstr == "Get back in the path!") {
          backInPathAudio.Play();
        }
      }
    }
    // this function takes the element index from the path and gives instructions
    // [as text on the HUD]. takes element id and returns nothing
    void giveInstructions( List<environment.Element> trail, byte el_idx)
    {
        environment.Element curr_pos = trail[el_idx];

        // need to add these in when git pulled.
        bool x_coord = curr_pos.x_coord;

        // if current position is an intersection find the angle at which to turn
        if (curr_pos.type == environment.Type.intersection) {
          // check if in center of intersection
          float relpos_x = LERP(0, 1, curr_pos.coords[1], player.transform.position.x, curr_pos.coords[3]);
          float relpos_z = LERP(0, 1, curr_pos.coords[0], player.transform.position.z, curr_pos.coords[2]);

          bool valid_x = relpos_x >= .2 && relpos_x <= .8;
          bool valid_z = relpos_z >= .2 && relpos_z <= .8;

          // handle cases when user needs to be closer to center
          // need separation as only one of these matters
          if (x_coord) {
            if (!valid_x) {
              instruction.text = "Go to the center of the intersection.";
              return;
            }
          } else { // z_coord
            if (!valid_z) {
              instruction.text = "Go to the center of the intersection.";
              return;
            }
          }

          // gets player rotation
          float player_rot = player.transform.eulerAngles.y;

          // object rotation should be in line with the NEXT object's direction if
          // in an intersection!
          float obj_rot = trail[el_idx + 1].obj.transform.eulerAngles.y;
          obj_rot += 90; obj_rot %= 360;

          // then finds difference of angles. adjusts to the range of
          // [0, 360].
          player_rot -= obj_rot;
          player_rot += 360; player_rot %= 360;

          if (player_rot < 15 || player_rot > 345){
            instruction.text = "Walk forward.";
            return;
          } else {
            if (player_rot > 180) {
              instruction.text = "Turn Right.";
              SendImpulse(0.1f, 0.05f, "right");
            } else {
              instruction.text = "Turn Left.";
              SendImpulse(0.1f, 0.05f, "left");
            }
            return;
          }
        // if no turning is needed display walk forward
        }

        // either arrived, or really lost
        if (curr_pos.type == environment.Type.destination) {
          if (curr_pos.pos == trail[trail.Count - 1].pos) { // last element
            instruction.text = "Arrived."; // in Correct area
            return;
          }
          else { // not in correct area!
            instruction.text = "Walk Forward.";
            return;
          }
        }

        //
        if (curr_pos.type == environment.Type.floor) { // on floor

          // gets player rotation
          float player_rot = player.transform.eulerAngles.y;

          // get current floor rotation
          float obj_rot = curr_pos.obj.transform.eulerAngles.y;
          obj_rot += 90; obj_rot %= 360;

          // then finds difference of angles. adjusts to the range of
          // [0, 360].
          player_rot -= obj_rot;
          player_rot += 360; player_rot %= 360;


          if (player_rot < 15 || player_rot > 345){
            instruction.text = "Walk forward.";
            return;
          } else {
            if (player_rot > 180) {
              instruction.text = "Turn Right.";
              SendImpulse(0.1f, 0.05f, "right");
            } else {
              instruction.text = "Turn Left.";
              SendImpulse(0.1f, 0.05f, "left");
            }
            return;
          }

        } else {
          instruction.text = "Get back in bounds!";
          return;
        }
    }

        // [z0, x0, z1, x1]
    List<environment.Element> setTrailBools(List<environment.Element> trail) {
      // start at i = 1 b/c can ignore destinations
      // which are always started at
      for (int i = 1; i < trail.Count - 1; i++) {
        environment.Element cur = trail[i];
        environment.Element prv = trail[i-1];
        if (cur.type == environment.Type.floor) {
          float diff_x = cur.coords[1] - prv.coords[1];
          float diff_z = cur.coords[0] - prv.coords[0];
          cur.x_coord = false;
          if (diff_x != 0){
            cur.x_coord = true;
          }
          cur.direction = false;
          if ((diff_x > 0 && diff_z == 0) || diff_z > 0){
            cur.direction = true;
          }
        } else if (cur.type == environment.Type.intersection) {
          environment.Element nxt = trail[i+1];
          cur.x_coord = prv.x_coord;
          float diff_x = nxt.coords[1] - cur.coords[1];
          float diff_z = nxt.coords[0] - cur.coords[0];
          cur.direction = false;
          if (diff_x > 0 | diff_z > 0){
            cur.direction = true;
          }
        }
        trail[i] = cur;
      }
      return trail;
    }

    public List<environment.Element> getTrail(byte start_loc, byte end_loc)
    {
      // Initialize Trail A to B
      List<environment.Element> trail_AB = new List<environment.Element>();
      trail_AB.Add(environ.loc_a);
      trail_AB.Add(environ.floor_0);
      trail_AB.Add(environ.int_0);
      trail_AB.Add(environ.floor_1);
      trail_AB.Add(environ.int_1);
      trail_AB.Add(environ.floor_2);
      trail_AB.Add(environ.loc_b);

      // Initialize Trail A to C
      List<environment.Element> trail_AC = new List<environment.Element>();
      trail_AC.Add(environ.loc_a);
      trail_AC.Add(environ.floor_0);
      trail_AC.Add(environ.int_0);
      trail_AC.Add(environ.floor_1);
      trail_AC.Add(environ.int_1);
      trail_AC.Add(environ.floor_3);
      trail_AC.Add(environ.loc_c);

      // Initialize Trail B to C
      List<environment.Element> trail_BC = new List<environment.Element>();
      trail_BC.Add(environ.loc_b);
      trail_BC.Add(environ.floor_2);
      trail_BC.Add(environ.int_1);
      trail_BC.Add(environ.floor_3);
      trail_BC.Add(environ.loc_c);

      switch(start_loc) {

        case 0: // Location A
          if (end_loc == 6) { // If location B
            setTrailBools(trail_AB);
            return trail_AB;
          }else{ // If location C
            setTrailBools(trail_AC);
            return trail_AC;
          }

        case 6: // Location B
          if (end_loc == 0) { // If location A
            trail_AB.Reverse();
            setTrailBools(trail_AB);
            return trail_AB;
          }else{ // If location C
            setTrailBools(trail_BC);
            return trail_BC;
          }

        default: // Location C
          if (end_loc == 0) { // If location A
            trail_AC.Reverse();
            setTrailBools(trail_AC);
            return trail_AC;
          }else{ // If Location B
            trail_BC.Reverse();
            setTrailBools(trail_BC);
            return trail_BC;
          }
      }
    }

    // direction - true means vector points to positive z side of x-z plane. if perpendicualr to
    // z plane, true means it points towards the positive x direction
    void updatePath(List<environment.Element> trail, byte el_idx, int pathid)
    {

        bool x_coord = trail[el_idx].x_coord;
        bool direction = trail[el_idx].direction;

        Vector3 location = player.transform.position;
        environment.Element element = trail[el_idx];


        // initialize path start value, path end, and player position
        // used for adjusting later.
        float k0, k1, pos;

        if (x_coord) {
          k0 = element.coords[1]; k1 = element.coords[3];
          pos = location.x;
        } else {
          k0 = element.coords[0]; k1 = element.coords[2];
          pos = location.z;
        }

        // just in case, clamp the value
        float clamp_val = CLAMP(k0, pos, k1);

        // if direction is 0, going negative way. set values appropriately
        if (!direction) {
          float temp = k1;
          k1 = k0;
          k0 = temp;
        }

        // take appropriate center, scale to adjust path value
        float center = LERP(0f, 0.5f, k0, clamp_val, k1);
        float scale = LERP(1, 0, k0, clamp_val, k1);


        GameObject path;
        switch(pathid) {
          case 0:
            path = path_0;
            break;
          case 1:
            path = path_1;
            break;
          case 2:
            path = path_2;
            break;
          default:
            path = path_3;
            break;
        }

        path.transform.localPosition = new Vector3(center, 70f, 0f);
        path.transform.localScale = new Vector3(scale, .01f, .3f);
    }

    // Update is called once per frame
    void Update()
    {
        bool aButtonPressed = OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch);
        bool frontTriggerPressed = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);

        // find pos (finds position in world, returns square youre in)
        byte el_idx = findPos(trail);
        prevInstruction = instruction.text;
        if (el_idx == 9) {
          instruction.text = "Get back in the path!";
          playSound(prevInstruction);
          SendImpulse(0.1f, 0.05f, "out");
          return;
        }

        // if youre on a floor, update the path on the floor
        if (trail[el_idx].type == environment.Type.floor)
        {
          int pathid;

          switch (trail[el_idx].pos) {
            case 1:
              pathid = 0;
              break;
            case 3:
              pathid = 1;
              break;
            case 5:
              pathid = 2;
              break;
            case 7:
              pathid = 3;
              break;
            default:
              pathid = 0;
              break;
          }

          // need to add trail in!
          updatePath(trail, el_idx, pathid);
        }
        prevInstruction = instruction.text;
        giveInstructions(trail, el_idx);
        playSound(prevInstruction);
        

        // maybe have another function to check for new instructions
    }
}
