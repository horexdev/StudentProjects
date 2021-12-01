using System;

namespace StudentProjects
{
    public static class ErrorsHandler
    {
        public static void Throw(ErrorType errorType)
        {
            throw new Exception($"Произошёл техническй сбой. Номер ошибки #{(int)errorType:D3}");
        }
    }
}