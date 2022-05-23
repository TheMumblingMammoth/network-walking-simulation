using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class Field : MonoBehaviour{

    [SerializeField] public Vector2 size;
    [SerializeField] public Vector2Int dimension;
    
    public Station[] stations;
    SpriteRenderer sprite;
    Texture2D texture;
    [SerializeField] Texture2D textureSample;
    //const float PPM = 10; // pixels per meter
    void Awake(){
        sprite = GetComponent<SpriteRenderer>();
        stations = GetComponentsInChildren<Station>();
        
        max_SNR = 20;
        min_SNR = 0;
        Debug.Log("max " + max_SNR);
        Debug.Log("min " + min_SNR);
        size = new Vector2(dimension.x/16f, dimension.y/16f);
        Camera.main.orthographicSize = size.y / 2;
        Camera.main.transform.position = new Vector3(size.x/2, size.y/2, -10);
        part_A.text = part_B.text = part_C.text = part_D.text = "25";
        
        Core.field = this;
        
        Draw();

    }
    
    [ContextMenu("Draw")]
    public void Draw(){
        texture = new Texture2D(dimension.x , dimension.y );// Instantiate<Texture2D>(textureSample);c
        float x0;
        float y0;
        for(int i = 0; i < dimension.x; i++)
            for(int j = 0; j < dimension.y; j++){
                x0 = i * (size.x  / dimension.x);
                y0 = j * (size.y  / dimension.y);
                texture.SetPixel(i, j, GetColor(SINR(new Vector2(x0, y0))));
            }

        texture.Apply();
        sprite.sprite = Sprite.Create(texture, new Rect(0, 0, dimension.x, dimension.y), new Vector2(0f, 0f), dimension.x / size.x);
    }

    public static float max_SNR;
    public static float min_SNR;
    public static Color GetColor(float SNR){
        SNR = (SNR - min_SNR)/(max_SNR - min_SNR);
        return new Color((1-SNR)*0.75f + 0.25f, SNR*0.75f + 0.25f, 0f, 1);
    }
    float PowerInWatt(float power){
        return Mathf.Pow(10f, power / 10f) / 1000f;
    }
    public float SINR(Vector2 pos){
        pos *= Core.MPU;

        //10 ^ (70 / 10) / 1000;
        
        //10000  / (2500 )
        float minDist = float.MinValue, sumDist = 0;
        
        for(int i = 0; i < stations.Length; i++){
            if(!stations[i].on) continue;
            float temp = 1 / Mathf.Pow(Vector2.Distance(stations[i].pos, pos) + 0.01f, 2);
            sumDist += temp;
            if(minDist < temp)
                minDist = temp;
        }
        sumDist -= minDist;

        float SNR; // =  PowerInWatt(stations[0].power)/ Mathf.Pow(Vector2.Distance(stations[0].pos, pos) + 0.01f, 2);
        SNR = 10 * Mathf.Log10(minDist / sumDist);

        //// диаграмма Воронова

        //банальный шум
        /*
        float noize = stations.Length == 1? 1f : 0f;
        for(int i = 1; i < stations.Length; i++)
            noize += PowerInWatt(stations[i].power)/ Mathf.Pow(Vector2.Distance(stations[i].pos, pos) + 0.01f, 2);            
        if(noize > 1)
            SNR /= noize;
        */
        SNR = Mathf.Min(SNR, max_SNR);
        SNR = Mathf.Max(SNR, min_SNR); 

        return SNR;
    }

    [SerializeField] Text a_count_text, b_count_text, c_count_text, d_count_text;
    int a_count = 0, b_count = 0, c_count = 0, d_count = 0;
    

    [SerializeField] InputField part_A, part_B, part_C, part_D;
    int per_A, per_B, per_C, per_D;
    void FixedUpdate(){
        if(TimeBar.pause) return;
        int a;
        bool fl = true;
        if(int.TryParse(part_A.text, out a)) per_A = a;
        else fl = false;
        part_A.textComponent.color = fl ? Color.black : Color.red;
        if(int.TryParse(part_B.text, out a)) per_B = a;
        else fl = false;
        part_B.textComponent.color = fl ? Color.black : Color.red;
        if(int.TryParse(part_C.text, out a)) per_C = a;
        else fl = false;
        part_C.textComponent.color = fl ? Color.black : Color.red;
        if(int.TryParse(part_D.text, out a)) per_D = a;
        else fl = false;
        part_D.textComponent.color = fl ? Color.black : Color.red;

        if(IntensityInput.pause) return;
        float dt = Time.deltaTime * Core.timeBoost * IntensityInput.intensity;
        Debug.Log(total + "   :  " + dt);
        for(int i = (int)total; i < (int)(total + dt) ; i++){
            int rng = Random.Range(0, 100);
            if(rng<per_A){
                SpawnUserA();
                continue;
            }
            rng -= per_A;
            if(rng<per_B){
                SpawnUserB();
                continue;
            }
            rng -= per_B;
            if(rng<per_C){
                SpawnUserC();
                continue;
            }
            SpawnUserD();
        }
        total += dt;
    }
    float total = 0;

    public void SpawnUserA(){
        a_count++;
        a_count_text.text = a_count.ToString();
        User user = Instantiate(Resources.Load<User>("User"));
        user.WalkRoute(Route.RouteA());
    }

    public void SpawnUserB(){
        b_count++;
        b_count_text.text = b_count.ToString();
        User user = Instantiate(Resources.Load<User>("User"));
        user.WalkRoute(Route.RouteB());
    }

    public void SpawnUserC(){
        c_count++;
        c_count_text.text = c_count.ToString();
        User user = Instantiate(Resources.Load<User>("User"));
        user.WalkRoute(Route.RouteC());
    }

    public void SpawnUserD(){
        d_count++;
        d_count_text.text = d_count.ToString();
        User user = Instantiate(Resources.Load<User>("User"));
        user.WalkRoute(Route.RouteD());
    }

        
}

