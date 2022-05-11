using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ModuleGrid : MonoBehaviour, Initializable, IPointerClickHandler
{
    [SerializeField] int Width;
    [SerializeField] int Height;
    [SerializeField] Image ImagePrefab;
    [SerializeField] UpgradePoller UpgradePoller;
    [SerializeField] RectTransform canvas;
    [SerializeField] XPManager XPManager;
    [SerializeField] UpgradeController UpgradeController;

    public const float CellSize = 100;

    private TalentPolicy[,] TalentGrid;
    private Image[,] ImageGrid;

    private TalentPolicy LastSelected = null;

    public void Init()
    {
        XPManager.ModuleComplete += ModuleComplete;
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

    private void ModuleComplete()
    {
        UpgradePoller.SetDefaultPolicy(null);
    }

    private void SetNewPolicy(TalentPolicy policy)
    {
        UpgradePoller.ResetPolls();
        UpgradePoller.SetDefaultPolicy(policy);
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

    public void ApplyUpgrade(TalentPolicy upgrade)
    {
        XPManager.SetCurrentPolicy(upgrade);
        SetNewPolicy(upgrade);
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
                if (shape[x, y] != null)
                {
                    TalentGrid[x + index.x, y + index.y] = shape[x, y];
                    ImageGrid[x + index.x, y + index.y].color = assignment;
                }
            }
        }

        Prereq.Assert(policy.Progress == 0, "Policy progress was not zero for policy " + policy.Title);
        Prereq.Assert(policy.GetCost() > 0, "Policy cost was <= zero for policy " + policy.Title);
        XPManager.SetCurrentPolicy(policy);
        SetNewPolicy(policy);
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

            if (LastSelected == TalentGrid[index.x, index.y])
            {
                //deselect
                if (XPManager.HasPolicyInProgress())
                {
                    UpgradePoller.SetDefaultPolicy(XPManager.GetCurrentPolicy());
                } 
                else
                {
                    UpgradePoller.SetDefaultPolicy(null);
                    UpgradePoller.ResetPolls();
                    UpgradeController.ShowPreviousOptions();
                }

                LastSelected = null;
            } 
            else
            {
                //select
                LastSelected = TalentGrid[index.x, index.y];
                UpgradePoller.SetDefaultPolicy(TalentGrid[index.x, index.y]);

                if (!XPManager.HasPolicyInProgress())
                {
                    //show upgrades, if applicable
                    UpgradePoller.ResetPolls();
                    UpgradeController.ShowUpgrades(LastSelected);
                }
            }
        }
    }
}
