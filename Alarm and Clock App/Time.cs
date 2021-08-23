namespace Alarm_and_Clock_App
{
    public class Time
    {
        private int second;
        private int minute;
        private int hour;
        private string half;
        private int snooze;

        public int Snooze
        {
            get => snooze;
            set => snooze = value;
        }

        public string Half
        {
            get => half;
            set => half = value;
        }

        public int Second
        {
            get => second;
            set => second = value;
        }

        public int Minute
        {
            get => minute;
            set => minute = value;
        }

        public int Hour
        {
            get => hour;
            set => hour = value;
        }
    }
}