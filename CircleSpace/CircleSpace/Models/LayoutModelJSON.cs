using CircleSpaceGeneralModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CircleSpace.Models
{
    public class LayoutModelJSON
    {
        public readonly object JSON;

        public LayoutModelJSON(LayoutModel layout)
        {
            MatchCollection cssRulesMatches = Regex.Matches(layout.CSS, "[\\)\\(\\]\\[:\\w, \" =\\-\\*^#\\.\\@>\\n\\+]+\\{\\s*[^}{]+\\s*\\}");
            List <object> cssRules = new List<object>();
            for (int i = 0; i < cssRulesMatches.Count; i++)
            {
                cssRules.Add(cssRulesMatches[i].Value);
            }
            JSON = new { Content = layout.Content, CSS = cssRules.ToArray(), Type = layout.Type.ToString() };
        }
    }
}