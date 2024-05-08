namespace DI_Project.Services.LifetimeExample
{
    public class TransientService
    {
        private readonly Guid guid;

        public TransientService()
        {
            guid = Guid.NewGuid();
        }
        public string GetGuid() => guid.ToString();
    }
}
