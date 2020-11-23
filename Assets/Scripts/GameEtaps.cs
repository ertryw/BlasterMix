using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameEtaps : MonoBehaviour
{
    public FloatReference GameSpeed;
    public FloatReference Scores;
    public FloatReference Lifes;
    public FloatReference Combo;
    public GameObject Player;
    public GameObject EndPanel;
    public GameObject PlayerSpawn;
    public Text ScoresTxt;

    private void Awake()
    {
        GameStage.stage = Stages.Firstrun;
    }

    void ResetVaribles()
    {
        GameSpeed.Variable.Value = 0.3f;
        Scores.Variable.Value = 0;
        Lifes.Variable.Value = 10;
        Combo.Variable.Value = 0;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayAgainGame()
    {
        GameStage.stage = Stages.Firstrun;
        EndPanel.transform.LeanMoveLocalY(800, 1f);
        Player.transform.LeanMove(PlayerSpawn.transform.position, 2f);
        Player.transform.LeanScale(new Vector3(1, 1, 1), 2f);
    }

    public void EndComplete()
    {
        GameStage.stage = Stages.EndReturn;
    }


    public void DestroyShields()
    {
        GameObject[] shields;
        shields = GameObject.FindGameObjectsWithTag("Shields");

        foreach (GameObject s in shields)
        {
            s.transform.LeanScale(new Vector3(0.1f, 0.1f, 0.1f), 0.7f).setOnComplete(() => Destroy(s)); ;
        }



    }


    // Update is called once per frame
    void Update()
    {
        if (GameStage.stage == Stages.Firstrun)
        {
            ResetVaribles();
            GameStage.stage = Stages.Play;
        }

        if (Lifes.Variable.Value == 0)
        {
            GameStage.stage = Stages.End;
            Player.transform.LeanScale(new Vector3(0.1f, 0.1f, 0.1f), 2f);
            Player.transform.LeanMove(new Vector3(PlayerSpawn.transform.position.x, PlayerSpawn.transform.position.y - 10, PlayerSpawn.transform.position.z), 3f);
            EndPanel.transform.LeanMoveLocalY(0, 1f).setOnComplete(EndComplete).setOnComplete(DestroyShields);
            ScoresTxt.text = Scores.Variable.Value.ToString();
            Lifes.Variable.Value = -1;
        }
        
    }



}
