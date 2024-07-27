public class Game
{
    public static IInputService InputService { get; private set; }

    public Game() =>
        InputService = new InputServiceYG();
} 