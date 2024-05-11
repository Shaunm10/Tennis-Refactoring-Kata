using System;
using System.Runtime.CompilerServices;

namespace Tennis;

public class TennisGame2 : ITennisGame
{
    private int player1Points;
    private int player2Points;

    // private string player1Result = "";
    // private string player2Result = "";
    // private string player1Name;
    // private string player2Name;

    private const string Love = "Love";
    private const string Fifteen = "Fifteen";
    private const string Thirty = "Thirty";
    private const string Deuce = "Deuce";
    private const string Forty = "Forty";

    private const string All = "All";


    public TennisGame2(string player1Name, string player2Name)
    {
        // this.player1Name = player1Name;
        player1Points = 0;
        player2Points = 0;
        //this.player2Name = player2Name;
    }

    public string GetScore()
    {
        GameScoreMode gameMode = this.CalculateGameScoreMode();

        var result = gameMode switch
        {
            GameScoreMode.TieScore => GetTieScoreResult(),
            GameScoreMode.Advantage => this.GetAdvantageResult(),
            GameScoreMode.BelowFour => GetBelowFourResult(),
            GameScoreMode.Winner => this.GetWinnerResult(),
            _ => "Default"

        };

        return result;
    }

    private string GetAdvantageResult()
    {
        if (this.player1Points > this.player2Points)
        {
            return "Advantage player1";
        }
        return "Advantage player2";
    }

    private string GetWinnerResult()
    {
        if (this.player1Points > this.player2Points)
        {
            return "Win for player1";
        }
        return "Win for player2";
    }

    private string GetBelowFourResult()
    {
        return $"{TranslateScoreToName(this.player1Points)}-{TranslateScoreToName(this.player2Points)}";

        string TranslateScoreToName(int score)
        {
            switch (score)
            {
                case 0:
                    return Love;

                case 1:
                    return Fifteen;

                case 2:
                    return Thirty;

                case 3:
                    return Forty;

                default:
                    return "Unknown";

            }
        }
    }


    private string GetTieScoreResult()
    {
        var result = string.Empty;
        switch (this.player1Points)
        {
            case 0:
                result = $"{Love}-{All}";
                break;
            case 1:
                result = $"{Fifteen}-{All}";
                break;
            case 2:
                result = $"{Thirty}-{All}";
                break;
            default:
                result = Deuce;
                break;

        }
        return result;
    }


    private GameScoreMode CalculateGameScoreMode()
    {
        var hasWinningScoreBeenReached = this.player1Points > 3 || this.player2Points > 3;

        if (this.player1Points == this.player2Points)
        {
            return GameScoreMode.TieScore;
        }

        if (this.player1Points < 4 && this.player1Points < 4 && !hasWinningScoreBeenReached)
        {
            return GameScoreMode.BelowFour;
        }

        var pointDifference = Math.Abs(player1Points - player2Points);
        if (pointDifference > 1)
        {
            return GameScoreMode.Winner;
        }

        return GameScoreMode.Advantage;
    }

    public void SetP1Score(int number)
    {
        P1Score(number);

    }

    public void SetP2Score(int number)
    {
        P2Score(number);
    }

    private void P1Score(int score)
    {
        player1Points += score;
    }

    private void P2Score(int score)
    {
        player2Points += score;
    }

    public void WonPoint(string player)
    {
        if (player == "player1")
            P1Score(1);
        else
            P2Score(1);
    }
}