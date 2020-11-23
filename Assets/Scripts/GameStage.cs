using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stages { Firstrun, Play, End, EndReturn, Replay }
public static class GameStage
{
    public static Stages stage;
}
