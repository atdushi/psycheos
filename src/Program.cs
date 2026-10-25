using Spectre.Console;
using PsycheOS.Core;
using PsycheOS.Therapy;

Console.OutputEncoding = System.Text.Encoding.UTF8;
var app = new IndividualMind();
var therapist = new Psychoanalyst();

while (true)
{
    AnsiConsole.Clear();

    // 1. ЗАГОЛОВОК И ТЕКУЩИЕ РЕСУРСЫ СИСТЕМЫ (ЛИБИДО)
    AnsiConsole.Write(new FigletText("Psyche OS").Centered().Color(Color.Purple));

    AnsiConsole.Write(new Panel(
        $"[bold yellow]Доступно памяти Эго (Воля):[/] {app.Libido.AvailableForEgo:F0}%\n" +
        $"[bold red]Заблокировано в Тени:[/] {app.Libido.SuppressedByShadow:F0}%\n" +
        $"[bold orange3]Потреблено комплексами:[/] {app.Libido.ConsumedByComplexes:F0}%\n" +
        $"[bold grey]Содержимое Тени:[/] [italic white]\"{app.GetShadowStatusForDebug()}\"[/]" // Вот здесь используется поле!
    ).Header("📊 МОНИТОР ПСИХИЧЕСКОЙ ЭНЕРГИИ (ЛИБИДО)").BorderColor(Color.Blue).Expand());

    // 2. ИНТЕРФЕЙС ТЕКУЩЕЙ АРХИТЕКТУРЫ
    var table = new Table().Border(TableBorder.Rounded).Expand();
    table.AddColumn("[bold purple]Компонент[/]");
    table.AddColumn("[bold green]Состояние рантайма[/]");

    table.AddRow("🎭 [bold]Персона[/]", app.Libido.AvailableForEgo > 20 ? "[green]Интерфейс отвечает (Вежливый режим)[/]" : "[red]Троттлинг. Персона недоступна[/]");
    table.AddRow("👤 [bold]Эго Context[/]", $"Поток [yellow]{app.EgoName}[/] загружен на {100 - app.Libido.AvailableForEgo:F0}%");

    AnsiConsole.Write(table);
    AnsiConsole.WriteLine();

    // 3. МЕНЮ ДЕЙСТВИЙ (ЖИЗНЕННЫЙ ЦИКЛ)
    var action = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[bold cyan]Выберите действие или событие среды:[/]\n")
            .AddChoices([
                            "💼 Обычная рабочая рутина (Расход 5%)",
                            "⚡ Критика от тимлида на код-ревью (Триггер Матери)",
                            "📈 Попытка попросить повышение зарплаты (Триггер Отца)",
                            "💖 Новое знакомство / Романтические отношения (Триггер Анимы)",
                            "🛋️ Запустить сессию дебага (Психоанализ / Оптимизация)",
                            "❌ Завершить симуляцию психики"
            ]));

    if (action.Contains("❌")) break;

    // 4.ОБРАБОТКА ДЕЛЬТЫ ЭНЕРГИИ В ЗАВИСИМОСТИ ОТ ВЫБОРА
    if (action.Contains("💼"))
    {
        app.HandleLifeEvent(LifeEvent.Routine);
        app.DrainEnergyToComplex(5.0);
        app.DisplayPoliteBehavior();
        Thread.Sleep(1000);
    }
    else if (action.Contains("⚡", StringComparison.InvariantCultureIgnoreCase))
    {
        // Комплекс перехватывает управление и сжирает ресурсы
        app.HandleLifeEvent(LifeEvent.Criticism);
        app.DrainEnergyToComplex(30.0);
        Thread.Sleep(2000);
    }
    else if (action.Contains("📈"))
    {
        // Комплекс перехватывает управление и сжирает ресурсы
        app.HandleLifeEvent(LifeEvent.Promotion);
        app.DrainEnergyToComplex(30.0);
        Thread.Sleep(2000);
    }
    else if (action.Contains("🛋️"))
    {
        AnsiConsole.Progress()
            .Columns([new TaskDescriptionColumn(), new ProgressBarColumn(), new PercentageColumn()])
            .Start(ctx =>
            {
                var task = ctx.AddTask("[cyan]Анализ метаданных Тени (Reflection)...[/]");
                
                while (!task.IsFinished) {
                    task.Increment(5); 
                    Thread.Sleep(40); 
                }
            });

        therapist.ScanShadow(app);
        therapist.OptimizeLibidoAllocation(app);
        Thread.Sleep(2500);
    }
    else if (action.Contains("💖"))
    {
        app.HandleLifeEvent(LifeEvent.Relationships);
        app.DrainEnergyToComplex(15.0); // Проекция сжирает часть Либидо Эго
        Thread.Sleep(2500);
    }
}

AnsiConsole.MarkupLine("\n[bold red]🧠 Psyche OS остановлена.[/] Поток сознания завершен со знаком 0.");
