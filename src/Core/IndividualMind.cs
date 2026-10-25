using PsycheOS.Interfaces;
using PsycheOS.Complexes;

namespace PsycheOS.Core
{
    public class IndividualMind : IPersona
    {
        public string EgoName { get; } = "Святослав";
        
        private readonly MotherComplex _motherComplex = new();

        private readonly FatherComplex _fatherComplex = new();

        private readonly AnimaProxy _animaProxy = new(); // Добавили Аниму
        
        private string ShadowDesires = "Скрытая агрессия и зависть"; // ТЕНЬ: Приватное поле для Reflection

        public IndividualMind()
        {
            _motherComplex.OnAffectTriggered += msg => Console.WriteLine(msg);
            _fatherComplex.OnCodeReviewFailed += msg => Spectre.Console.AnsiConsole.MarkupLine($"[bold orange3]{msg}[/]");
            _animaProxy.OnProjectionActivated += msg => Spectre.Console.AnsiConsole.MarkupLine($"[bold fuchsia]{msg}[/]"); // Розовый лог для Анимы        
        }

        public void DisplayPoliteBehavior() => Console.WriteLine("😀 [Персона]: 'Всё отлично, спасибо!'");
        
        public void ExecuteWorkTask() => Console.WriteLine("💼 [Персона]: 'Задача выполняется по регламенту.'");
        
        public void HandleLifeEvent(LifeEvent ev)
        {
            // Переводим enum в легаси-строку для обратной совместимости с логикой комплексов
            string legacyEventStr = ev.ToLegacyTrigger();

            // 1. Прогоняем событие через врожденные триггеры комплексов
            _motherComplex.ProcessTrigger(legacyEventStr);
            _fatherComplex.ProcessTrigger(legacyEventStr);
            _animaProxy.ProcessTrigger(legacyEventStr);

            // 2. СБРОС ИНТЕГРИРОВАННОГО СОСТОЯНИЯ: 
            // Если Тень была чистой после терапии, новый стресс перезаписывает её с нуля
            if (ShadowDesires.Contains("Осознанная"))
            {
                ShadowDesires = ""; // Очищаем буфер интеграции перед записью нового бага
            }

            // 3. ДИНАМИЧЕСКИЙ ЗАПИСЬ В ТЕНЬ: Стресс инкапсулируется в приватное поле
            switch (ev)
            {
                case LifeEvent.Criticism:
                    ShadowDesires += (ShadowDesires == "" ? "" : " + ") + "Подавленная обида на критику";
                    break;
                case LifeEvent.Promotion:
                    ShadowDesires += (ShadowDesires == "" ? "" : " + ") + "Чувство стыда и неполноценности";
                    break;
                case LifeEvent.Relationships:
                    ShadowDesires += (ShadowDesires == "" ? "" : " + ") + "Страх быть отвергнутым / Идеализация";
                    break;
                case LifeEvent.Routine:
                    ShadowDesires += (ShadowDesires == "" ? "" : " + ") + "Вытесненная экзистенциальная тоска";
                    break;
            }
        }

        // Добавим публичный метод для снятия проекций психоаналитиком
        public void IntegrateAnima() => _animaProxy.IsProjected = false;

        // Либидо
        public PsychicEnergy Libido { get; } = new PsychicEnergy();

        public void DrainEnergyToComplex(double amount)
        {
            Console.WriteLine($"\n📉 [Рантайм Психики]: Утечка ресурсов! {amount}% Либидо перехвачено бессознательным.");
            Libido.ConsumedByComplexes += amount;

            if (Libido.AvailableForEgo < 20.0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("⚠️ [🛑 CRITICAL OUT OF MEMORY]: Ресурс Воли Эго истощен! Система уходит в депрессивный safe-mode.");
                Console.ResetColor();
            }
        }

        public string GetShadowStatusForDebug()
        {
            // Само Эго может прочитать свою Тень только в режиме рефлексии, 
            // но для дашборда мы возвращаем обертку
            return ShadowDesires;
        }
    }
}