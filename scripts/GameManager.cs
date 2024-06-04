using Godot;
using System;

public partial class GameManager : Node
{
    int score = 0;

    Label scoreLabel => GetNode<Label>("Score");

    public void IncreaseScore(int addedScoreValue)
    {
        score += addedScoreValue;
        scoreLabel.Text = "Collected " + score.ToString() + " coins";
    }

    public int GetScore()
    {
        return score;
    }
}