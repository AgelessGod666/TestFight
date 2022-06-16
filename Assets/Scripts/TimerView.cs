using TMPro;

public class TimerView
{
    private readonly IFightTimer fightTimer;
    private readonly TextMeshProUGUI timer;

    public TimerView(TimerHolder timerHolder, IFightTimer fightTimer)
    {
        timer = timerHolder.timer;
        this.fightTimer = fightTimer;
        Initialize();
    }

    private void Initialize()
    {
        fightTimer.OnFightEnded += SetTime;
    }

    private void SetTime(int time)
    {
        timer.text = time + " seconds";
    }
}