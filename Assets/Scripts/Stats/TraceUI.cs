using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class TraceUI : MonoBehaviour{
    public Trace trace {get; private set;}
    
    List<Trace> traces;
    
    void Awake(){
        traces = new List<Trace>(100);
        Core.traceUI = this;
    }

    public void AddToHistory(Trace trace){
        traces.Add(trace);
    }

    public void PrintHistory(){
        /*
        string output = " ";//[ \n";
        for(int i = 0; i < traces.Count; i++){
            output += "";
            for(int j = 0; j < traces[i].hexes.Count - 1; j++){
                output+= (traces[i].hexes[j].x + traces[i].hexes[j].y * Core.field.W).ToString() + ", ";
            }
            output+= (traces[i].hexes[traces[i].hexes.Count - 1].x + traces[i].hexes[traces[i].hexes.Count - 1].y * Core.field.W).ToString() + "\n";
        }
        output += "";

        string path = "output.csv";
        File.WriteAllText(path, output);

        Debug.Log(output);
        */

        string output = " ";//[ \n";
        for(int i = 0; i < traces.Count; i++){
            output += "";
            for(int j = 0; j < traces[i].SNRs.Count - 1; j++){
                output+= traces[i].SNRs[j] + "; ";
            }
            output+= traces[i].SNRs[traces[i].SNRs.Count - 1] + "\n";
        }
        output = output.Replace(',', '.');
        output = output.Replace(';', ',');
        File.WriteAllText("output_SNR.csv", output);
        Debug.Log("SINR: " + output);

        output = " ";//[ \n";
        for(int i = 0; i < traces.Count; i++){
            output += "";
            for(int j = 0; j < traces[i].MCSs.Count - 1; j++){
                output+= traces[i].MCSs[j] + ", ";
            }
            output+= traces[i].MCSs[traces[i].MCSs.Count - 1] + "\n";
        }
        File.WriteAllText("output_MCS.csv", output);
        Debug.Log("MCS: " + output);

        
    }


}