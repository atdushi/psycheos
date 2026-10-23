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
        
        public void HandleLifeEvent(string ev)
        {
            // 1. Прогоняем событие через врожденные триггеры
            _motherComplex.ProcessTrigger(ev);
            _fatherComplex.ProcessTrigger(ev);
            _animaProxy.ProcessTrigger(ev);

            // 2. СБРОС ИНТЕГРИРОВАННОГО СОСТОЯНИЯ: 
            // Если Тень была чистой после терапии, новый стресс перезаписывает её с нуля
            if (ShadowDesires.Contains("Осознанная"))
            {
                ShadowDesires = ""; // Очищаем буфер интеграции перед записью нового бага
            }

            // 2. ДИНАМИЧЕСКИЙ ЗАПИСЬ В ТЕНЬ: Стресс инкапсулируется в приватное поле
            if (ev.Contains("критика"))
            {
                ShadowDesires += (ShadowDesires == "" ? "" : " + ") + "Подавленная обида на критику";
            }
            else if (ev.Contains("повышение"))
            {
                ShadowDesires += (ShadowDesires == "" ? "" : " + ") + "Чувство стыда и неполноценности";
            }
            else if (ev.Contains("отношения"))
            {
                ShadowDesires += (ShadowDesires == "" ? "" : " + ") + "Страх быть отвергнутым / Идеализация";
            }
            else if (ev.Contains("рутина"))
            {
                ShadowDesires += (ShadowDesires == "" ? "" : " + ") + "Вытесненная экзистенциальная тоска";
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