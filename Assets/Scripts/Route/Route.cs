using UnityEngine;
using UnityEngine.UI;
public class Route : MonoBehaviour{
    public PathDot start {get; set;}
    public PathDot end {get; set;}
    [SerializeField] InputField start_dot, end_dot;
    public Color color;
    void Awake(){
        switch(name){
            case "Route A": 
                route_A = this;
                start_dot.text = "A";
                end_dot.text = "B";
                return;
            case "Route B":
                route_B = this;
                start_dot.text = "B";
                end_dot.text = "C";
                return;
            case "Route C":
                route_C = this;
                start_dot.text = "C";
                end_dot.text = "D";
                return;
            case "Route D":
                route_D = this;
                start_dot.text = "D";
                end_dot.text = "A";
                return;
        }
        start = PathArea.proxy.GetDot(start_dot.text);
        end = PathArea.proxy.GetDot(end_dot.text);
    }

    void FixedUpdate(){
        start = PathArea.proxy.GetDot(start_dot.text);
        end = PathArea.proxy.GetDot(end_dot.text);
        start_dot.textComponent.color = start == null ? Color.red : Color.black;
        end_dot.textComponent.color = end == null ? Color.red : Color.black;
    }

    public Vector2 NextStep(PathDot next_spot, Vector2 current_pos, float step, float tremble){
        if(Vector2.Distance(current_pos, next_spot.Pos()) <= step) return next_spot.Pos();

        float shift = Random.Range(- tremble, tremble);
        Vector2 pos = Vector2.MoveTowards(current_pos, next_spot.Pos(), step);
        Vector2 ort = new Vector2(pos.y, pos.x);
        pos = Vector2.MoveTowards(pos, pos + ort, shift);
        return pos;
    }


    public PathDot NextDot(PathDot current_spot){
        return PathArea.proxy.Next(current_spot, end);
    }

    static Route route_A, route_B, route_C, route_D;
    public static Route RouteA(){ return route_A; }
    public static Route RouteB(){ return route_B; }
    public static Route RouteC(){ return route_C; }
    public static Route RouteD(){ return route_D; }

}
