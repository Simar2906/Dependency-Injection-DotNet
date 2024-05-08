namespace DI_Project.Services.LifetimeExample
{
    public class SingletonService
    {
        private readonly Guid guid;

        public SingletonService()
        {
            guid = Guid.NewGuid();
        }
        public string GetGuid() => guid.ToString();
    }
}
