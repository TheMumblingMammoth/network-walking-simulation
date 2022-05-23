using UnityEngine;
using UnityEngine.UI;

public class StationText : MonoBehaviour
{
    public static StationText proxy;
    Text text;

    void Awake(){
        proxy = this;
        text = GetComponent<Text>();
        gameObject.SetActive(false);
    }
    Station station;
    public void SetStation(Station station){ 
        gameObject.SetActive(true);
        this.station = station;
    }
    
    public void Off(){
        gameObject.SetActive(false);
    }
    
    void Update(){
        text.text = station.on? " ON " : " OFF "; //((int)station.power / 1).ToString();
        transform.position = Camera.main.WorldToScreenPoint(station.transform.position + new Vector3(0, 3, 0));
    }
}
