using UnityEngine;
using System.Collections.Generic;
public class PathDot : MonoBehaviour {
    PathArea area;
    public List<PathDot> nears;
    //public PathDot door_step;
//    public string location;
    public Vector2 Pos(){return transform.position; }

    void Awake(){
        area = GetComponentInParent<PathArea>();
    }

    void Start(){
        //foreach(area)
    }
}