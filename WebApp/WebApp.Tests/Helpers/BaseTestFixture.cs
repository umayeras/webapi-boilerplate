﻿using Moq;

 namespace WebApp.Tests.Helpers
{
    public class BaseTestFixture
    {
        public T VerifyAny<T>()
        {
            return Any<T>();
        }

        public T SetupAny<T>()
        {
            return Any<T>();
        }

        private T Any<T>()
        {
            return It.IsAny<T>();
        }
    }
}
