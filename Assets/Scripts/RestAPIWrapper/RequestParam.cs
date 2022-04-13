using System.Collections.Generic;
using CodeEditorModels.ProgLanguages;

namespace RestAPIWrapper
{
    public static class RequestParam
    {
        public static Dictionary<ProgLanguage, string> ProgLanguages = new Dictionary<ProgLanguage, string>()
        {
            [ProgLanguage.C] = "c",
            [ProgLanguage.Python] = "py",
            [ProgLanguage.Pascal] = "pas"
        };
    }
}