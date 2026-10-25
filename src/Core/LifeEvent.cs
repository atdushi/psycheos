namespace PsycheOS.Core;

public enum LifeEvent
{
    /// <summary>
    /// Критика от руководства или коллег
    /// </summary>
    Criticism,

    /// <summary>
    /// Неожиданное повышение или успех
    /// </summary>
    Promotion,

    /// <summary>
    /// Новые или кризисные отношения
    /// </summary>
    Relationships,

    /// <summary>
    /// Бытовая или рабочая рутина
    /// </summary>
    Routine
}

public static class LifeEventExtensions
{
    // Хелпер, возвращающий строковый триггер для совместимости со старыми комплексами
    public static string ToLegacyTrigger(this LifeEvent ev) => ev switch
    {
        LifeEvent.Criticism => "критика",
        LifeEvent.Promotion => "повышение",
        LifeEvent.Relationships => "отношения",
        LifeEvent.Routine => "рутина",
        _ => throw new ArgumentOutOfRangeException(nameof(ev))
    };
}