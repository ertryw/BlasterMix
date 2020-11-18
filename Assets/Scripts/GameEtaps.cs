using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum stages { firstrun,  play, end , endreturn, replay }


public class GameEtaps : MonoBehaviour
{
    public FloatReference GameSpeed;
    public FloatReference Scores;
    public FloatReference Lifes;
    public FloatReference Combo;
    public Rigidbody2D PlayerRB;
    public GameObject Player;
    public GameObject EndPanel;
    public GameObject PlayerSpawn;

    public Text ScoresTxt;
    public stages Stage = stages.firstrun;


    // Start is called before the first frame update
    void Start()
    {
       // Screen.SetResolution(700, 900,true);
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
        Stage = stages.firstrun;
        EndPanel.transform.LeanMoveLocalY(800, 1f);
        Player.transform.LeanMove(PlayerSpawn.transform.position, 2f);
        Player.transform.LeanScale(new Vector3(1, 1, 1), 2f);
        // PlayerRB.bodyType = RigidbodyType2D.Static;
    }

    public void EndComplete()
    {
        Stage = stages.endreturn;
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
        if (Stage == stages.firstrun)
        {
            ResetVaribles();
            Stage = stages.play;
        }

        if (Lifes.Variable.Value == 0)
        {
            Stage = stages.end;
            //PlayerRB.bodyType = RigidbodyType2D.Dynamic;
            Player.transform.LeanScale(new Vector3(0.1f, 0.1f, 0.1f), 2f);
            Player.transform.LeanMove(new Vector3(PlayerSpawn.transform.position.x, PlayerSpawn.transform.position.y - 10, PlayerSpawn.transform.position.z), 3f);
            EndPanel.transform.LeanMoveLocalY(0, 1f).setOnComplete(EndComplete).setOnComplete(DestroyShields);
            ScoresTxt.text = Scores.Variable.Value.ToString();
            Lifes.Variable.Value = -1;
        }
        
    }



}
