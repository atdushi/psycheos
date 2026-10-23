using PsycheOS.Core;
using Spectre.Console;
using System.Reflection;

namespace PsycheOS.Therapy
{
    public class Psychoanalyst
    {
        public void ScanShadow(IndividualMind patient)
        {
            // Используем рефлексию для обхода инкапсуляции private-поля
            var shadowField = typeof(IndividualMind).GetField("ShadowDesires", BindingFlags.NonPublic | BindingFlags.Instance);

            string content = (string)shadowField?.GetValue(patient);

            AnsiConsole.MarkupLine($"\n🛋️ [bold cyan][[Психоанализ]]:[/] Вскрыты private-данные Тени -> [red]\"{content}\"[/]");

            // РЕФАКТОРИНГ: Перезаписываем Тень, превращая вытесненный страх в осознанный ресурс
            shadowField?.SetValue(patient, "Осознанная и контролируемая сила воли (Анима и Тень интегрированы)");
        }

        public void OptimizeLibidoAllocation(IndividualMind patient)
        {
            Console.WriteLine("\n🧹 [Рефакторинг / Психоанализ]: Высвобождение заблокированных ресурсов...");

            // Осознание снижает необходимость тратить силы на жесткую инкапсуляцию приватных полей
            patient.Libido.SuppressedByShadow -= 35.0;
            patient.Libido.ConsumedByComplexes = 0.0; // Гасим аффекты
            patient.IntegrateAnima(); // Снимаем проекции Анимы, возвращая прозрачность интерфейсов

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✨ Успешно! Свободный ресурс Эго увеличен до: {patient.Libido.AvailableForEgo}%");
            Console.WriteLine("✨ Успешно![/] Прокси Анимы переведен в прозрачный режим (Муза).");
            Console.ResetColor();
        }
    }
}