namespace MarkoStudio.Twist.Application.Models
{
    public class ToxicityScore
    {
        public double Value { get; }

        public string Label => GetLabel();

        public ToxicityScore(double value)
        {
            Value = value;
        }

        private string GetLabel()
        {
            return Value <= 50 ? "Non-toxic" : "Toxic";
        }
    }
}
