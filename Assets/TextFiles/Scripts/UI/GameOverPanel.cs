using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour, LateInitializable
{
    [SerializeField] HealthManager player;
    [SerializeField] GameObject EndPanel;
    [SerializeField] float panelDelay = 0.5f;

    public void LateInit()
    {
        player.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        StartCoroutine(EnableEndPanel()); 
    }

    private IEnumerator EnableEndPanel()
    {
        yield return new WaitForSeconds(panelDelay);
        EndPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
