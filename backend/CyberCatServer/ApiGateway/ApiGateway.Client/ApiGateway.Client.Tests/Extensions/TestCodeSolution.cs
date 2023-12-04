namespace ApiGateway.Client.Tests.Extensions
{
    public class TestCodeSolution
    {
        public string SolveTutorial()
        {
            return PrintMessage("Hello cat!");
        }

        public string PrintMessage(string message)
        {
            return "#include <iostream>\r\n#include <stdio.h>\r\n\r\nint main()\r\n{\r\n    printf(\"" + message + "\");\r\n}\r\n";
        }
    }
}