namespace ApiGateway.Client.Tests.Extensions
{
    public static class CodeSolution
    {
        public static string Tutorial()
        {
            return PrintMessage("Hello cat!");
        }

        public static string Task5()
        {
            return PrintMessage("Hello cat!");
        }

        public static string PrintMessage(string message)
        {
            return "#include <iostream>\r\n#include <stdio.h>\r\n\r\nint main()\r\n{\r\n    printf(\"" + message + "\");\r\n}\r\n";
        }
    }
}