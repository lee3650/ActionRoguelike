using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridElement : MonoBehaviour
{
    public TalentPolicy Talent;
    public List<Image> Images;

    private Color normalColor;

    private float offset;
    private float pulseSpeed = 3.5f;

    private List<GridElementState> States;

    public void Initialize(TalentPolicy tp, List<Image> images, Color defaultColor)
    {
        Talent = tp;
        Images = images;
        normalColor = defaultColor;
        States = new List<GridElementState>();
        States.Add(GridElementState.Default);
    }

    public void HandleEvent(GridElementEvent e)
    {
        switch (States[0])
        {
            case GridElementState.Default:
                switch (e)
                {
                    case GridElementEvent.Selected:
                        PushState(GridElementState.Selected);
                        break;
                    case GridElementEvent.MouseEnter:
                        PushState(GridElementState.MousedOver);
                        break;
                    case GridElementEvent.Upgradable:
                        PushState(GridElementState.Upgradable);
                        break;
                }
                break;
            case GridElementState.MousedOver:
                switch (e)
                {
                    case GridElementEvent.Selected:
                        PopState();
                        PushState(GridElementState.Selected);
                        break;
                    case GridElementEvent.MouseExit:
                        PopState();
                        break;
                    case GridElementEvent.Deselected:
                        //well, that's interesting. 
                        PopState();
                        PopState();
                        break;
                }
                break;
            case GridElementState.Selected:
                switch (e)
                {
                    case GridElementEvent.Deselected:
                        PopState();
                        break; 
                }
                break;
            case GridElementState.Upgradable:
                switch (e)
                {
                    case GridElementEvent.Selected:
                        PushState(GridElementState.Selected);
                        break;
                    case GridElementEvent.MouseEnter:
                        PushState(GridElementState.MousedOver);
                        break;
                }
                break;
        }
    }

    private void PushState(GridElementState state)
    {
        States.Insert(0, state);

        EnterState(state);
    }

    public void ResetState()
    {
        States = new List<GridElementState>();
        PushState(GridElementState.Default);
    }

    private void EnterState(GridElementState state)
    {
        switch (state)
        {
            case GridElementState.Default:
                SetImageColors(normalColor);
                break;
            case GridElementState.MousedOver:
                SetImageColors(Color.black);
                break;
            case GridElementState.Selected:
                SetImageColors(Color.yellow);
                break;
            case GridElementState.Upgradable:
                offset = Random.Range(0, 1f);
                break;
        }
    }

    private void PopState()
    {
        GridElementState cur = States[0];

        States.RemoveAt(0);
        if (States.Count == 0)
        {
            PushState(GridElementState.Default);
        }

        switch (cur)
        {
            case GridElementState.Upgradable:
                SetImageColors(normalColor);
                break;
        }

        EnterState(States[0]);
    }

    public void Update()
    {
        switch (States[0])
        {
            case GridElementState.Upgradable:
                Pulse();
                break; 
        }
    }

    private void Pulse()
    {
        SetImageColors(Color.Lerp(normalColor, Color.white, Mathf.Sin((Time.realtimeSinceStartup - offset) * pulseSpeed)));
    }

    private void SetImageColors(Color c)
    {
        foreach (Image i in Images)
        {
            i.color = c;
        }
    }
}

public enum GridElementState
{
    Default,
    Upgradable, 
    Selected, 
    MousedOver,
}

public enum GridElementEvent
{
    MouseEnter,
    MouseExit,
    Upgradable,
    Selected,
    Deselected, 
}
