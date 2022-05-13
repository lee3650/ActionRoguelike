using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleCreator : MonoBehaviour, LateInitializable
{
    [SerializeField] TalentGetter TalentGetter;
    [SerializeField] Transform ImagePrefab;

    public void LateInit()  
    {
        print("constructing module!");
        TalentPolicy[,] shape = TalentGetter.Policy.GetShape();
        for (int x = 0; x < shape.GetLength(0); x++)
        {
            for (int y = 0; y < shape.GetLength(1); y++)
            {
                if (shape[x, y] != null)
                {
                    Transform image = Instantiate(ImagePrefab, transform);
                    image.localPosition = new Vector3(x * GridDS.CellSize, y * GridDS.CellSize, 0);
                }
            }
        }
    }
}
