using UnityEngine;

[CreateAssetMenu(fileName = "TransformArrayData", menuName = "Data/Array/TransformArrayData")]
public class TransformArrayData : ScriptableObject, IDrawGizmo
{
    public Transform[,] Array2D;

    public Transform this[int i, int j]
    {
        get => Array2D[i,j];
        set => Array2D[i,j] = value;
    }

    public void InitializeArraySize(int sizeX, int sizeY)
    {
        Array2D = new Transform[sizeX, sizeY];
    }
    
    public Vector3 GetMinPosition()
    {
        Vector3 min = Array2D[0, 0].position;
    
        for(int i = 0; i < Array2D.GetLength(0); i++)
        {
            for(int j = 0; j < Array2D.GetLength(1); j++)
            {
                min = Vector3.Min(min, Array2D[i, j].position);
            }
        }
    
        return min;
    }

    public Vector3 GetMaxPosition()
    {
        Vector3 max = Array2D[0, 0].position;
    
        for(int i = 0; i < Array2D.GetLength(0); i++)
        {
            for(int j = 0; j < Array2D.GetLength(1); j++)
            {
                max = Vector3.Max(max, Array2D[i, j].position);
            }
        }
    
        return max;
    }
    
    public void OnDrawGizmos()
    {
        if (Array2D != null)
        {
            for (int i = 0; i < Array2D.GetLength(0); i++)
            {
                for (int j = 0; j < Array2D.GetLength(1); j++)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(Array2D[i, j].position, 0.1f);
                
                    #if UNITY_EDITOR
                    UnityEditor.Handles.color = Color.black;
                    UnityEditor.Handles.Label(Array2D[i, j].position, $"({i},{j})");
                    #endif
                }
            }
        }
    }
}
