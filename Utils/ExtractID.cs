using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FleetCommandAPI.Utils
{
    public static class ExtractID
    {
         public static int FromUrl(string url)
    {
        Regex regex = new Regex(@"/(\d+)/$");
        Match match = regex.Match(url);

        if (match.Success)
        {
            return int.Parse(match.Groups[1].Value);
        }

        return -1; 
    }
    }
}