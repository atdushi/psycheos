using PsycheOS.Core;

namespace PsycheOS.Complexes
{
    public class AnimaProxy : Archetype
    {
        public bool IsProjected { get; set; } = true; // Режим проекции включен по умолчанию

        public event Action<string> OnProjectionActivated;

        public AnimaProxy() : base("ARCH_ANIMA", "Вдохновение, чувства, образ души") { }

        public override void ProcessTrigger(string context)
        {
            // Если Эго встречает нового человека/проект в режиме активной проекции
            if (IsProjected && context.Contains("отношения"))
                OnProjectionActivated?.Invoke("✨ [[ANIMA PROXY]]: Обнаружен внешний объект. Запущен оверлей проекции!\n" +
                                               "-> Результат: Реальные баги объекта скрыты. Сгенерирован образ 'Идеала/Музы'.");
        }
    }
}
