using System.Collections.Generic;
using CodeEditorModels.ProgLanguages;

namespace RestAPIWrapper
{
    public static class ProgLanguageParam
    {
        public static Dictionary<ProgLanguage, string> Lang = new Dictionary<ProgLanguage, string>()
        {
            [ProgLanguage.C] = "c",
            [ProgLanguage.Python] = "py"
        };
    }
}