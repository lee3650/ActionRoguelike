using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GridDS : MonoBehaviour
{
    [SerializeField] GridElement GridElementPrefab;
    [SerializeField] Image ImagePrefab;

    public const float CellSize = 100;

    private TalentPolicy[,] TalentGrid;
    private Image[,] ImageGrid; 
    
    private List<GridElement> GridElements;

    public void RemovePolicy(TalentPolicy tp)
    {
        GridElement e = FindElement(tp);

        if (e != null)
        {
            GridElements.Remove(e);

            foreach (Image i in e.Images)
            {
                //that'll need to change at some point most likely 
                i.color = Color.white; 
            }

            List<Vector2Int> positions = GetTalentPositions(tp);

            foreach (Vector2Int p in positions)
            {
                TalentGrid[p.x, p.y] = null;
            }

            Destroy(e.gameObject);
        }
    }

    public Vector3 GetWorldPos(Vector2Int index)
    {
        print("searching for world pos (" + index.x + ", " + index.y + ") with grid size (" + ImageGrid.GetLength(0) + ", " + ImageGrid.GetLength(1) + ")");
        return ImageGrid[index.x, index.y].transform.position;
    }

    public void CreateGrid(int Width, int Height)
    {
        GridElements = new List<GridElement>();

        TalentGrid = new TalentPolicy[Width, Height];
        ImageGrid = new Image[Width, Height];

        float xCenter = (Width - 1) / 2f;
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

    public void ResetElementStates()
    {
        foreach (GridElement ge in GridElements)
        {
            ge.ResetState();
        }
    }

    public GridElement AddGridElement(TalentPolicy tp, Vector3 worldPos)
    {
        GridElement newElement = Instantiate(GridElementPrefab);

        Vector2Int index = GetItemIndex(worldPos);

        TalentPolicy[,] shape = tp.GetShape();

        Color assignment = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);

        List<Image> images = new List<Image>();

        for (int x = 0; x < shape.GetLength(0); x++)
        {
            for (int y = 0; y < shape.GetLength(1); y++)
            {
                if (shape[x, y] != null)
                {
                    TalentGrid[x + index.x, y + index.y] = shape[x, y];
                    ImageGrid[x + index.x, y + index.y].color = assignment;
                    images.Add(ImageGrid[x + index.x, y + index.y]);
                }
            }
        }

        newElement.Initialize(tp, images, assignment);

        GridElements.Add(newElement);

        return newElement; 
    }

    public List<TalentPolicy> GetUpgradableTalents()
    {
        List<TalentPolicy> temp = new List<TalentPolicy>();

        for (int x = 0; x < TalentGrid.GetLength(0); x++)
        {
            for (int y = 0; y < TalentGrid.GetLength(1); y++)
            {
                if (TalentGrid[x, y] != null && TalentGrid[x, y].Upgradable && !temp.Contains(TalentGrid[x, y]))
                {
                    temp.Add(TalentGrid[x, y]);
                }
            }
        }

        return temp;
    }

    public List<TalentPolicy> GetRandomUpgradeMask()
    {
        List<TalentPolicy> temp = GetUpgradableTalents();

        temp = (List<TalentPolicy>)UtilityRandom.SortByRandom(temp);

        List<TalentPolicy> result = new List<TalentPolicy>();

        int num = Random.Range(1, 3);

        for (int i = 0; i < num; i++)
        {
            if (i < temp.Count)
            {
                temp[i].RandomizeUpgradeOrder();

                result.Add(temp[i]);
            }
        }

        return result; 
    }

    public TalentPolicy GetPolicy(Vector3 worldPos)
    {
        GridElement e = FindNearestGridElement(worldPos);
        if (e != null)
        {
            return e.Talent;
        }
        return null;
    }

    public void ApplyEventToElements(List<TalentPolicy> talents, GridElementEvent ev)
    {
        foreach (TalentPolicy t in talents)
        {
            SendElementEvent(t, ev);
        }
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

    public void SendElementEvent(TalentPolicy tp, GridElementEvent ev)
    {
        GridElement ge = FindElement(tp);
        if (ge != null)
        {
            ge.HandleEvent(ev);
        }
    }

    private GridElement FindElement(TalentPolicy tp)
    {
        foreach (GridElement e in GridElements)
        {
            if (tp == e.Talent)
            {
                return e;
            }
        }

        return null;
    }

    public GridElement FindNearestGridElement(Vector3 worldPos)
    {
        (Vector3 pos, float dist) = GetNearestGridItem(worldPos);
        Vector2Int index = GetItemIndex(pos);
        TalentPolicy talent = TalentGrid[index.x, index.y];
        if (talent == null)
        {
            return null;
        }

        return FindElement(talent);
    }

    public Vector2Int GetItemIndex(Vector3 worldPos)
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

    private List<Vector2Int> GetTalentPositions(TalentPolicy talent)
    {
        List<Vector2Int> result = new List<Vector2Int>();

        for (int x = 0; x < TalentGrid.GetLength(0); x++)
        {
            for (int y = 0; y < TalentGrid.GetLength(1); y++)
            {
                if (TalentGrid[x, y] == talent)
                {
                    result.Add(new Vector2Int(x, y));
                }
            }
        }

        return result;
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
}
