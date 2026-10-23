namespace PsycheOS.Core
{
    // Абстрактный паттерн, заложенный в архитектуру психики
    public abstract class Archetype
    {
        public string Id { get; }
        protected string CoreDrive { get; }

        protected Archetype(string id, string coreDrive) => (Id, CoreDrive) = (id, coreDrive);
        public abstract void ProcessTrigger(string context);
    }
}
