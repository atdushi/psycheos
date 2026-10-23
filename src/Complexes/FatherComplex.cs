using PsycheOS.Core;

namespace PsycheOS.Complexes
{
    public class FatherComplex : Archetype
    {
        public double StrictnessLevel { get; set; } = 0.85; // Уровень критичности/строгости (0.0 - 1.0)
        public event Action<string> OnCodeReviewFailed; // Сигнал жесткой цензуры воли

        public FatherComplex() : base("ARCH_FATHER", "Закон, структура, социальный успех и авторитет") { }

        public override void ProcessTrigger(string context)
        {
            // Если Эго пытается заявить о себе (повышение, амбиции) при высокой строгости отца
            if (StrictnessLevel > 0.7 && context.Contains("повышение зарплаты"))
            {
                OnCodeReviewFailed?.Invoke(
                    "❌ [[ЦЕНЗУРА ОТЦА]]: Запрос заблокирован внутренним валидатором!\n" +
                    "-> Причина: Синдром самозванца. 'Ты еще недостаточно хорош', 'Просьба отклонена до рантайма'."
                );
            }
        }
    }
}
