using UnityEngine;

public class Station : MonoBehaviour {

    public float power = 1;
    public Vector2 pos;
    public bool on = true;
    void Awake(){
        pos = transform.position * Core.MPU;
        GetComponent<SpriteRenderer>().color = on ? Color.green : Color.red;
    }
    
    
    public void Update(){
        pos = transform.position * Core.MPU;
        if(dragged){
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            if(Input.GetMouseButtonDown(1)){
                on = !on;
                GetComponent<SpriteRenderer>().color = on ? Color.green : Color.red;
            }
            /*if(Input.mouseScrollDelta.y > 0) power += 1;
            if(Input.mouseScrollDelta.y < 0) power -= 1;
            power = Mathf.Max(power, 0);
            //power = Mathf.Min(power, 20000);*/
            if(RuntimeDrawButton.run_time_drawing)
                Core.field.Draw();
        }
    }
    bool dragged = false;
    public void OnMouseDown(){
        Debug.Log("down");
        dragged = true;
        transform.localScale = new Vector3(2, 2, 2);
        StationText.proxy.SetStation(this);
    }

    public void OnMouseUp(){
        Debug.Log("up");
        dragged = false;
        transform.localScale = new Vector3(1, 1, 1);
        Core.field.Draw();
        StationText.proxy.Off();
    }
    
    
    
}
