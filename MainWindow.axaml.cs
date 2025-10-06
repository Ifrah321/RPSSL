using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaApplicationRPSSL;
public partial class MainWindow : Window
{

    private enum Choice { Rock, Paper, Scissors, Lizard, Spock }
    private enum Outcome { Draw, Player, Agent }
    
    private const int MaxScore = 3;
    private readonly Random _random = new();
    private int _playerScore = 0;
    private int _agentScore = 0;
    
    private TextBlock? _roundText, _scoreText, _choicesText;
    private Button? _btnRock, _btnPaper, _btnScissors, _btnLizard, _btnSpock;
    private Button? _btnReset, _btnClose;

    public MainWindow()
    {
        InitializeComponent();
        BindControls();
        AttachEvents();
        UpdateUI();
    }


    private void BindControls()
    {
        _roundText   = this.FindControl<TextBlock>("RoundText");
        _scoreText   = this.FindControl<TextBlock>("ScoreText");
        _choicesText = this.FindControl<TextBlock>("ChoicesText");

        _btnRock     = this.FindControl<Button>("BtnRock");
        _btnPaper    = this.FindControl<Button>("BtnPaper");
        _btnScissors = this.FindControl<Button>("BtnScissors");
        _btnLizard   = this.FindControl<Button>("BtnLizard");
        _btnSpock    = this.FindControl<Button>("BtnSpock");

        _btnReset    = this.FindControl<Button>("BtnReset");
        _btnClose    = this.FindControl<Button>("BtnClose");
    }
    
    private void AttachEvents()
    {
        _btnRock!.Click     += OnPlayerChoice;
        _btnPaper!.Click    += OnPlayerChoice;
        _btnScissors!.Click += OnPlayerChoice;
        _btnLizard!.Click   += OnPlayerChoice;
        _btnSpock!.Click    += OnPlayerChoice;

        _btnReset!.Click += OnReset;
        _btnClose!.Click += (_, _) => Close();
    }
    
    private void OnPlayerChoice(object? sender, RoutedEventArgs e)
    {
        if (_playerScore >= MaxScore || _agentScore >= MaxScore)
            return;

        var btn = (Button)sender!;
        var playerChoice = ParseChoice(btn.Content?.ToString());
        var agentChoice = (Choice)_random.Next(5);

        var result = DetermineOutcome(playerChoice, agentChoice);
        ApplyOutcome(playerChoice, agentChoice, result);
    }
    
    private static Choice ParseChoice(string? text) => text switch
    {
        "Rock" => Choice.Rock,
        "Paper" => Choice.Paper,
        "Scissors" => Choice.Scissors,
        "Lizard" => Choice.Lizard,
        "Spock" => Choice.Spock,
        _ => throw new ArgumentException($"Ukendt valg: {text}")
    };
    
    private static Outcome DetermineOutcome(Choice player, Choice agent)
    {
        if (player == agent) return Outcome.Draw;

        return (player, agent) switch
        {
            (Choice.Rock, Choice.Scissors) or (Choice.Rock, Choice.Lizard) => Outcome.Player,
            (Choice.Paper, Choice.Rock) or (Choice.Paper, Choice.Spock) => Outcome.Player,
            (Choice.Scissors, Choice.Paper) or (Choice.Scissors, Choice.Lizard) => Outcome.Player,
            (Choice.Lizard, Choice.Spock) or (Choice.Lizard, Choice.Paper) => Outcome.Player,
            (Choice.Spock, Choice.Rock) or (Choice.Spock, Choice.Scissors) => Outcome.Player,
            _ => Outcome.Agent
        };
    }
    
    private void ApplyOutcome(Choice player, Choice agent, Outcome result)
    {
        switch (result)
        {
            case Outcome.Player:
                _playerScore++;
                _roundText!.Text = $"Du vandt runden! 🎉 {player} slår {agent}.";
                break;
            case Outcome.Agent:
                _agentScore++;
                _roundText!.Text = $"Agenten vandt runden. 🤖 {agent} slår {player}.";
                break;
            case Outcome.Draw:
                _roundText!.Text = "Uafgjort! 🤝";
                break;
        }

        _choicesText!.Text = $"Du: {player}   |   Agent: {agent}";
        _scoreText!.Text = $"Du: {_playerScore}   |   Agent: {_agentScore}";
        
        if (_playerScore >= MaxScore || _agentScore >= MaxScore)
        {
            string winnerMsg = _playerScore > _agentScore
                ? "🏆 Du vandt spillet!"
                : "💻 Agenten vandt spillet.";
            _roundText!.Text = winnerMsg;
        }
    }
    
    private void OnReset(object? sender, RoutedEventArgs e)
    {
        _playerScore = 0;
        _agentScore = 0;
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        _roundText!.Text = "Klik en knap for at starte…";
        _choicesText!.Text = "Du: –   |   Agent: –";
        _scoreText!.Text = $"Du: {_playerScore}   |   Agent: {_agentScore}";
    }
}



