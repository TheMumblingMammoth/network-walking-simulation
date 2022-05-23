using UnityEngine;
using System.Collections.Generic;

public class User : MonoBehaviour{
    public static float min_speed = 1f;
    public static float max_speed = 10f;
    float speed;
    public static float min_tremble = 0.1f;
    public static float max_tremble = 1f;
    float tremble;
    Vector2 target;
    
    public Trace trace {get; private set;}
    //[SerializeField] float wandering = 1.5f;
    PathDot current_spot;
    PathDot next_spot;
    Route route;
    int spot_id;
    public bool walking {get; private set;}
    public int MCS {get; private set;}
    
    bool predicted;
    public float SNR {get; private set;}
    public List<float> SNR_history {get; private set;}
    public int hisory_time {get; private set;}
    int lose_time = 10;
    int lose_timer = 0;
    
    public void WalkRoute(Route route){
        GetComponent<SpriteRenderer>().color = route.color;
        speed = Random.Range(min_speed, max_speed) / Core.MPU;
        tremble = Random.Range(min_tremble, max_tremble) / Core.MPU;
        this.route = route;
        trace = new Trace();
        //if(Core.predictor.userCount == 0)
        StartAt(route.start);
    }
    void StartAt(PathDot spot){
        current_spot = spot;
        next_spot = route.NextDot(current_spot);
        transform.position = spot.transform.position;
        target = route.NextStep(next_spot, transform.position, speed, tremble);
        spot_id = 1;
        
        SNR_history = new List<float>(72*16);
        SNR = Core.field.SINR(transform.position);
        trace.SNRs.Add(SNR);
        trace.MCSs.Add(((int)((SNR - Field.min_SNR)/(Field.max_SNR - Field.min_SNR) * 64)));

        hisory_time = 0;
        walking = true;
        MCS = 16;
    }

    void NextSpot(){
        current_spot = next_spot;
        next_spot = route.NextDot(current_spot);
        if(current_spot == route.end){
            gameObject.SetActive(false);
            Core.traceUI.AddToHistory(trace);
            walking = false;
            //if(Core.userCount < 100)
                //Core.field.SpawnCommonUser();
            return;
        }        
        target = route.NextStep(next_spot, transform.position, speed, tremble);
    }
    
    void FixedUpdate(){
        if(!walking) return;
        if(!Core.runing) return;
        
        SNR = Core.field.SINR(transform.position);
        trace.SNRs.Add(SNR);
        trace.MCSs.Add(((int)((SNR - Field.min_SNR)/(Field.max_SNR - Field.min_SNR) * 64)));
        SNR_history.Add(Core.field.SINR(transform.position));
        hisory_time++;
        
        //if(Random.Range(0f, 1f) >= 0.8f)
        //SendFrame();
        float step = speed * Time.deltaTime * Core.timeBoost;
        float dist;
        do{
            dist = Vector2.Distance(transform.position, target);
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            step -= dist;
            if((Vector2)transform.position == route.NextStep(next_spot,transform.position, speed, tremble) )
                NextSpot();
            else
                if((Vector2)transform.position == target)
                    target = route.NextStep(next_spot, transform.position, speed, tremble);
        }while(step > 0 && walking);
    }

}
