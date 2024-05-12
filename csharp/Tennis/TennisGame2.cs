using System;
using System.Runtime.CompilerServices;

namespace Tennis;

public class TennisGame2 : ITennisGame
{

    /// <summary>
    /// The number points player 1 has.
    /// </summary>
    private int playerOnePoints;

    /// <summary>
    /// The number points player 2 has.
    /// </summary>
    private int playerTwoPoints;


    /// <summary>
    /// Represents a score of "0".
    /// </summary>
    private const string Love = "Love";

    /// <summary>
    /// Represents a score of "1".
    /// </summary>
    private const string Fifteen = "Fifteen";

    /// <summary>
    /// Represents a score of "2".
    /// </summary>
    private const string Thirty = "Thirty";

    /// <summary>
    /// Represents a score of "3".
    /// </summary>
    private const string Forty = "Forty";

    /// <summary>
    /// Represents a tied score.
    /// </summary>
    private const string Deuce = "Deuce";

    /// <summary>
    /// 
    /// </summary>
    private const string All = "All";


    public TennisGame2(string player1Name, string player2Name)
    {
        playerOnePoints = 0;
        playerTwoPoints = 0;
    }

    public string GetScore()
    {
        GameScoreMode gameMode = this.CalculateGameScoreMode();

        var gameScore = gameMode switch
        {
            GameScoreMode.TieScore => this.GetTieScoreResult(),
            GameScoreMode.Advantage => this.GetAdvantageResult(),
            GameScoreMode.GameInProgress => this.GetGameInProgressResult(),
            GameScoreMode.Winner => this.GetWinnerResult(),
            _ => "GameScoreMode Not Found"
        };

        return gameScore;
    }

    public void SetP1Score(int number)
    {
        IncrementPlayerOneScore(number);
    }

    public void SetP2Score(int number)
    {
        IncrementPlayerTwoScore(number);
    }

    private string GetAdvantageResult()
    {
        if (this.playerOnePoints > this.playerTwoPoints)
        {
            return "Advantage player1";
        }
        return "Advantage player2";
    }

    private string GetWinnerResult()
    {
        if (this.playerOnePoints > this.playerTwoPoints)
        {
            return "Win for player1";
        }
        return "Win for player2";
    }

    private string GetGameInProgressResult()
    {
        return $"{TranslateScoreToName(this.playerOnePoints)}-{TranslateScoreToName(this.playerTwoPoints)}";

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
        switch (this.playerOnePoints)
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
        var hasWinningScoreBeenReached = this.playerOnePoints > 3 || this.playerTwoPoints > 3;

        if (this.playerOnePoints == this.playerTwoPoints)
        {
            return GameScoreMode.TieScore;
        }

        if (this.playerOnePoints < 4 && this.playerOnePoints < 4 && !hasWinningScoreBeenReached)
        {
            return GameScoreMode.GameInProgress;
        }

        var pointDifference = Math.Abs(playerOnePoints - playerTwoPoints);
        if (pointDifference > 1)
        {
            return GameScoreMode.Winner;
        }

        return GameScoreMode.Advantage;
    }

    private void IncrementPlayerOneScore(int score)
    {
        playerOnePoints += score;
    }

    private void IncrementPlayerTwoScore(int score)
    {
        playerTwoPoints += score;
    }

    public void WonPoint(string player)
    {
        if (player == "player1")
            IncrementPlayerOneScore(1);
        else
            IncrementPlayerTwoScore(1);
    }
}