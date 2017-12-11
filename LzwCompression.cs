using System;
using System.Collections.Generic;


public static class Extensions
{
    public static string LzwDecode(this string s)
    {
        var dict = new Dictionary<int, string>();
        var data = (s + "").ToCharArray();
        var currChar = data[0];
        var oldPhrase = currChar.ToString();
        var _out = new string[] { currChar.ToString() };
        var code = 256;

        for (var i = 1; i < data.Length; i++)
        {
            var currCode = data[i];
            var phrase = "";
            if (currCode < 256)
            {
                phrase = data[i].ToString();
            }
            else
            {
                phrase = dict.ContainsKey(currCode) ? dict[currCode] : (oldPhrase + currChar).ToString();
            }
            Array.Resize(ref _out, _out.Length + 1);
            _out[_out.Length - 1] = phrase;
            currChar = phrase[0];
            dict[code] = (oldPhrase + currChar);
            code++;
            oldPhrase = phrase;
        }
        return string.Join("", _out);
    }

    public static string LzwEncode(this string s)
    {
        if (!string.IsNullOrWhiteSpace(s))
        {
            var dict = new Dictionary<string, int>();
            var data = (s + "").ToCharArray();
            var _out = new int[] { };
            var phrase = data[0].ToString();
            var code = 256;
            for (var i = 1; i < data.Length; i++)
            {
                var currChar = data[i];
                if (dict.ContainsKey(phrase + currChar))
                {
                    phrase += currChar;
                }
                else
                {
                    Array.Resize(ref _out, _out.Length + 1);
                    _out[_out.Length - 1] = (phrase.Length > 1 ? dict[phrase] : phrase[0]);
                    dict[phrase + currChar] = code;
                    code++;
                    phrase = currChar.ToString();
                }
            }
            Array.Resize(ref _out, _out.Length + 1);
            _out[_out.Length - 1] = (phrase.Length > 1 ? dict[phrase] : phrase[0]);
            var out2 = new char[_out.Length];
            for (var i = 0; i < _out.Length; i++)
            {
                out2[i] = Convert.ToChar(_out[i]);
            }
            return string.Join("", out2);
        }
        return string.Empty;
    }
}