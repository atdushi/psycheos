namespace PsycheOS.Core
{
    public class PsychicEnergy
    {
        public double TotalCapacity { get; } = 100.0;
        private double _suppressedByShadow = 50.0; // Базовый расход на удержание вытесненного
        private double _consumedByComplexes = 20.0; // Расход на фоновые триггеры

        public double SuppressedByShadow
        {
            get => _suppressedByShadow;
            set => _suppressedByShadow = Math.Clamp(value, 0, TotalCapacity - _consumedByComplexes);
        }

        public double ConsumedByComplexes
        {
            get => _consumedByComplexes;
            set => _consumedByComplexes = Math.Clamp(value, 0, TotalCapacity - _suppressedByShadow);
        }

        // Эго троттлит, если Тень и Комплексы забирают слишком много ресурсов
        public double AvailableForEgo => Math.Max(0, TotalCapacity - _suppressedByShadow - _consumedByComplexes);
    }
}
