using UnityEngine;
using System.Collections.Generic;
public class Trace {
    
    public List<float> SNRs {get; private set;}
    public List<int> MCSs {get; private set;}
    public Trace(){
        SNRs = new List<float>(100);
        MCSs = new List<int>(100);        
    }

}