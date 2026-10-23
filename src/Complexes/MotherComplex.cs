using PsycheOS.Core;

namespace PsycheOS.Complexes
{
    public class MotherComplex : Archetype
    {
        public double EmotionalCharge { get; set; } = 0.9; // Сила аффекта (0.0 - 1.0)
        public event Action<string> OnAffectTriggered; // Сигнал для перехвата потока Эго

        public MotherComplex() : base("ARCH_MOTHER", "Безопасность и принятие") { }

        public override void ProcessTrigger(string context)
        {
            if (EmotionalCharge > 0.7 && context.Contains("критика"))
                OnAffectTriggered?.Invoke("🚨 [АФФЕКТ]: Комплекс Матери затопил Эго. Включен паралич воли.");
        }
    }
}