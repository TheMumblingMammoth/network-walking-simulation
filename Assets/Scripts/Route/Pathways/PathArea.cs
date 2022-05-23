using UnityEngine;
public class PathArea : MonoBehaviour{
    public PathDot [] dots {get; private set;}
    public static PathArea proxy;
    void Awake(){
        proxy = this;
        dots = GetComponentsInChildren<PathDot>();
    }
    float [][] direct_matrix;
    float [][] total_matrix;

    void Start(){
        
        direct_matrix = new float[dots.Length][];
        total_matrix = new float[dots.Length][];
        for(int i = 0; i < dots.Length; i++){
            direct_matrix[i] = new float[dots.Length];
            total_matrix[i] = new float[dots.Length];
            for(int j = 0; j < dots.Length; j++){
                if(i == j) direct_matrix[i][j] = 0;
                else    if(dots[i].nears.Contains(dots[j]))
                    direct_matrix[i][j] = Vector2.Distance(dots[i].transform.position, dots[j].transform.position);
                else 
                    direct_matrix[i][j] = float.PositiveInfinity;
                total_matrix[i][j] = direct_matrix[i][j];
            }
        }        
        
        for(int i = 0; i < Mathf.Sqrt(direct_matrix.Length) + 1; i++){
            GoDeeper();
        }
        
    }

    void LogMatrix(string matrix_name, float [][] matrix){
        string line = matrix_name + ":\n";
        for(int i = 0; i < dots.Length; i++){
            for(int j = 0; j < dots.Length; j++){
                switch(matrix[i][j]){
                    case 0f: line += " 0 "; break;
                    case float.PositiveInfinity: line += " * "; break;
                    default: line += matrix[i][j].ToString("#.#"); break;
                }
                line+= "  ";
            }
            line += "\n";
        }
        Debug.Log(line);
    }

    void GoDeeper(){
        for(int t = 0; t < dots.Length; t++)
            for(int i = 0; i < dots.Length; i++)
                for(int j = 0; j < dots.Length; j++)
                    if(i != j && i != t && j != t){
                        //Debug.LogWarning((total_matrix[t][j] + total_matrix[j][i]).ToString() + " ? " + total_matrix[t][i].ToString());
                        if(total_matrix[t][j] + total_matrix[j][i] < total_matrix[t][i])
                            total_matrix[t][i] = total_matrix[t][j] + total_matrix[j][i];
                    }
    }

    public PathDot Next(PathDot current, PathDot destanation){
        if(current == destanation) return current;
        int I = -1;
        do{
            I++;
            if(I >= dots.Length) return null;
        }while(dots[I] != current);
        int J = -1;
        do{
            J++;
            if(J >= dots.Length) return null;
        }while(dots[J] != destanation);
        if(direct_matrix[I][J] != float.PositiveInfinity) return destanation;
        int temp = -1;
        for(int t = 0; t < dots.Length; t++)
            if(t != I && t != J){
                if(temp != -1)
                    if(direct_matrix[I][t] + total_matrix[t][J] < total_matrix[I][temp] + total_matrix[temp][J])
                        temp = t;
                if(temp == -1)
                    if(direct_matrix[I][t] + total_matrix[t][J] < float.PositiveInfinity)
                        temp = t;
            }
        if(temp == -1)  return null;
        return dots[temp];
    }

    public PathDot GetDot(string dot_name){
        foreach(PathDot dot in dots)
            if(dot.name == dot_name)
                return dot;
        return null;
    }                
               

}