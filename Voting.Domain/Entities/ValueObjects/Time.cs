namespace Voting.Domain.Entities.ValueObjects
{
    public class Time
    {
        public Time(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }

        public int Hour { get; private set; }
        public int Minute { get; private set; }
    }
}