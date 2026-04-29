class SpeedLimitExceededException : Exception
{
    public int Speed { get; }
    public SpeedLimitExceededException() : base("Превышение скорости!") { }
    public SpeedLimitExceededException(int speed) : base($"Скорость {speed} км/ч превышает лимит 120 км/ч!") => Speed = speed;
    public SpeedLimitExceededException(int speed, Exception inner) : base($"Скорость {speed} км/ч!", inner) => Speed = speed;
}
