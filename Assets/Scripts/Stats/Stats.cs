using UnityEngine;
using UnityEngine.UI;
public class Stats : MonoBehaviour{
    [SerializeField] Text text_loses;
    int loses;
    [SerializeField] Text text_messages;
    int messages;

    [SerializeField] Text text_prediction_loses;
    int prediction_loses;
    [SerializeField] Text text_prediction_messages;
    int prediction_messages;


    [SerializeField] Text text_MCS;
    [SerializeField] Text text_SNR;
    public Splin splin;
    
    void Awake(){
        Core.stats = this;
        Reset();
    }
    public void Reset(){
        messages = 0;
        loses = 0;
        prediction_loses = 0;
        prediction_messages = 0;
        text_messages.text = "Messages send: " + messages.ToString();
        text_loses.text = "Messages lost: " + loses.ToString();
        text_prediction_messages.text = "Messages send with prediction: " + prediction_messages.ToString();
        text_prediction_loses.text = "Messages lost with prediction: " + prediction_loses.ToString();
    }
    public void Message(){
        messages++;
        text_messages.text = "Messages send: " + messages.ToString();
    }

    public void LostFrame(){
        loses++;
        text_loses.text = "Messages lost: " + loses.ToString();
    }

    public void PredictionMessage(){
        prediction_messages++;
        text_prediction_messages.text = "Messages send with prediction: " + prediction_messages.ToString();
    }

    public void PredictionLostFrame(){
        prediction_loses++;
        text_prediction_loses.text = "Messages lost with prediction: " + prediction_loses.ToString();
    }

    void Update(){
        if(Splin.user != null)
            if(Splin.user.walking){
                text_SNR.text = "Current SNR: " + Splin.user.SNR.ToString("0.00");
                text_MCS.text = "Current MCS: " + Splin.user.MCS.ToString();
            }


    }

}
