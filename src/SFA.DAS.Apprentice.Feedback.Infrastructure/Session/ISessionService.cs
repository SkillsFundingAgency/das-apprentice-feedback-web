﻿namespace SFA.DAS.ApprenticeFeedback.Infrastructure.Session
{
    public interface ISessionService
    {
        void Set(string key, object value);
        void Remove(string key);
        T Get<T>(string key);
        bool Exists(string key);
    }
}
