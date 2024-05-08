﻿namespace DI_Project.Services.LifetimeExample
{
    public class ScopedService
    {
        private readonly Guid guid;

        public ScopedService()
        {
            guid = Guid.NewGuid();
        }
        public string GetGuid() => guid.ToString();
    }
}
