using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ModuleGrid : MonoBehaviour, LateInitializable, IPointerClickHandler
{
    [SerializeField] int Width;
    [SerializeField] int Height;
    [SerializeField] Image ImagePrefab;
    [SerializeField] UpgradePoller UpgradePoller;
    [SerializeField] RectTransform canvas;

    public const float CellSize = 100;

    private TalentPolicy[,] TalentGrid;
    private Image[,] ImageGrid; 

    public void LateInit()
    {
        TalentGrid = new TalentPolicy[Width, Height];
        ImageGrid = new Image[Width, Height];

        float xCenter = (Width - 1)/ 2f;
        float yCenter = (Height - 1) / 2f;

        Vector3 origin = new Vector3(-xCenter * CellSize, -yCenter * CellSize);

        for (int x = 0; x < ImageGrid.GetLength(0); x++)
        {
            for (int y = 0; y < ImageGrid.GetLength(1); y++)
            {
                Image image = Instantiate(ImagePrefab);
                image.transform.SetParent(transform);
                image.transform.localScale = new Vector3(1, 1, 1);
                image.transform.localPosition = new Vector3(x * CellSize, y * CellSize, 0) + origin;
                ImageGrid[x, y] = image; 
            }
        }
    }

    public (Vector3, float) GetNearestGridItem(Vector3 current)
    {
        float min = 100000f;
        Vector3 closest = new Vector3();

        for (int x = 0; x < ImageGrid.GetLength(0); x++)
        {
            for (int y = 0; y < ImageGrid.GetLength(1); y++)
            {
                Transform t = ImageGrid[x, y].transform;
                float dist = Vector3.Distance(t.position, current);
                if (dist < min)
                {
                    min = dist;
                    closest = t.position;
                }
            }
        }

        return (closest, min);
    }

    public void ApplyUpgrade(Vector3 worldPos, TalentPolicy upgrade)
    {
        //delegate
    }

    public bool CanUpgradeBeApplied(Vector3 worldPos, TalentPolicy parent)
    {
        TalentPolicy[,] shape = parent.GetShape();

        Vector2Int offset = GetItemIndex(worldPos);

        for (int x = 0; x < shape.GetLength(0); x++)
        {
            for (int y = 0; y < shape.GetLength(1); y++)
            {
                if (offset.x + x >= TalentGrid.GetLength(0) || offset.y + y >= TalentGrid.GetLength(1))
                {
                    return false; 
                }
                if (shape[x, y] != null && TalentGrid[offset.x + x, offset.y + y] == null)
                {
                    return false; 
                }
                if (shape[x, y] != null && TalentGrid[offset.x + x, offset.y + y] != shape[x, y])
                {
                    return false; 
                }
            }
        }

        return true; 
    }

    public bool IsPositionValid(Vector3 worldPos, TalentPolicy tp)
    {
        Vector2Int index = GetItemIndex(worldPos);

        TalentPolicy[,] shape = tp.GetShape();

        for (int x = 0; x < shape.GetLength(0); x++)
        {
            for (int y = 0; y < shape.GetLength(1); y++)
            {
                if (index.x + x >= TalentGrid.GetLength(0) || index.y + y >= TalentGrid.GetLength(1))
                {
                    return false; 
                }

                if (TalentGrid[index.x + x, index.y + y] != null && shape[x, y] != null)
                {
                    return false;
                }
            }
        }

        return true; 
    }

    public void WriteToGrid(TalentPolicy policy, Vector3 worldPos)
    {
        Vector2Int index = GetItemIndex(worldPos);

        TalentPolicy[,] shape = policy.GetShape();

        Color assignment = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);

        for (int x = 0; x < shape.GetLength(0); x++)
        {
            for (int y = 0; y < shape.GetLength(1); y++)
            {
                print(string.Format("Index: {0}, x, y: ({1}, {2}), grid size: ({3}, {4}), shape size ({5}, {6})", index, x, y, TalentGrid.GetLength(0), TalentGrid.GetLength(1), shape.GetLength(0), shape.GetLength(1)));
                if (shape[x, y] != null)
                {
                    TalentGrid[x + index.x, y + index.y] = shape[x, y];
                    ImageGrid[x + index.x, y + index.y].color = assignment;
                }
            }
        }
    }

    private Vector2Int GetItemIndex(Vector3 worldPos)
    {
        for (int x = 0; x < ImageGrid.GetLength(0); x++)
        {
            for (int y = 0; y < ImageGrid.GetLength(1); y++)
            {
                if (ImageGrid[x, y].transform.position == worldPos)
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        throw new System.Exception("Could not find index for world pos " + worldPos);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 globalPos;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas, eventData.position, eventData.pressEventCamera, out globalPos))
        {
            (Vector3 worldPos, float dist) = GetNearestGridItem(globalPos);
            Vector2Int index = GetItemIndex(worldPos);
            UpgradePoller.SetDefaultPolicy(TalentGrid[index.x, index.y]);
        }
    }
}
