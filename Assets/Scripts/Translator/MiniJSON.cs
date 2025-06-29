// MiniJSON.cs - còpia lleugera per a Unity
// Crèdit: https://gist.github.com/darktable/1411710

using System;
using System.Collections.Generic;
using System.Text;

public static class MiniJSON
{
    public static object Deserialize(string json)
    {
        if (json == null) return null;
        return Parser.Parse(json);
    }

    sealed class Parser
    {
        const string WORD_BREAK = "{}[],:\"";

        enum TOKEN
        {
            NONE, CURLY_OPEN, CURLY_CLOSE, SQUARE_OPEN, SQUARE_CLOSE,
            COLON, COMMA, STRING, NUMBER, TRUE, FALSE, NULL
        };

        StringReader json;

        Parser(string jsonString)
        {
            json = new StringReader(jsonString);
        }

        public static object Parse(string jsonString)
        {
            var instance = new Parser(jsonString);
            return instance.ParseValue();
        }

        object ParseValue()
        {
            TOKEN nextToken = NextToken;
            switch (nextToken)
            {
                case TOKEN.STRING: return ParseString();
                case TOKEN.NUMBER: return ParseNumber();
                case TOKEN.CURLY_OPEN: return ParseObject();
                case TOKEN.SQUARE_OPEN: return ParseArray();
                case TOKEN.TRUE: return true;
                case TOKEN.FALSE: return false;
                case TOKEN.NULL: return null;
                default: return null;
            }
        }

        Dictionary<string, object> ParseObject()
        {
            var table = new Dictionary<string, object>();
            json.Read(); // skip {

            while (true)
            {
                switch (NextToken)
                {
                    case TOKEN.NONE: return null;
                    case TOKEN.CURLY_CLOSE: json.Read(); return table;
                    default:
                        string name = ParseString();
                        if (NextToken != TOKEN.COLON) return null;
                        json.Read(); // skip :
                        table[name] = ParseValue();
                        break;
                }

                if (NextToken == TOKEN.COMMA)
                {
                    json.Read(); // skip ,
                }
                else if (NextToken == TOKEN.CURLY_CLOSE)
                {
                    json.Read(); // skip }
                    return table;
                }
            }
        }

        List<object> ParseArray()
        {
            var array = new List<object>();
            json.Read(); // skip [

            var parsing = true;
            while (parsing)
            {
                TOKEN nextToken = NextToken;
                if (nextToken == TOKEN.NONE)
                    return null;
                else if (nextToken == TOKEN.SQUARE_CLOSE)
                {
                    json.Read(); // skip ]
                    break;
                }
                else
                {
                    array.Add(ParseValue());
                }

                if (NextToken == TOKEN.COMMA)
                {
                    json.Read();
                }
                else if (NextToken == TOKEN.SQUARE_CLOSE)
                {
                    json.Read();
                    break;
                }
            }

            return array;
        }

        string ParseString()
        {
            var sb = new StringBuilder();
            char c;
            json.Read(); // skip "

            while (true)
            {
                if (json.Peek() == -1) break;

                c = NextChar;
                if (c == '"') break;
                else sb.Append(c);
            }

            return sb.ToString();
        }

        object ParseNumber()
        {
            string number = NextWord;
            if (number.Contains(".")) return double.Parse(number);
            return int.Parse(number);
        }

        string NextWord
        {
            get
            {
                var sb = new StringBuilder();
                while (!IsWordBreak(PeekChar))
                {
                    sb.Append(NextChar);
                    if (json.Peek() == -1) break;
                }
                return sb.ToString();
            }
        }

        char PeekChar => Convert.ToChar(json.Peek());
        char NextChar => Convert.ToChar(json.Read());

        TOKEN NextToken
        {
            get
            {
                EatWhitespace();
                if (json.Peek() == -1) return TOKEN.NONE;

                char c = PeekChar;
                switch (c)
                {
                    case '{': return TOKEN.CURLY_OPEN;
                    case '}': return TOKEN.CURLY_CLOSE;
                    case '[': return TOKEN.SQUARE_OPEN;
                    case ']': return TOKEN.SQUARE_CLOSE;
                    case ',': return TOKEN.COMMA;
                    case '"': return TOKEN.STRING;
                    case ':': return TOKEN.COLON;
                    case 't': return TOKEN.TRUE;
                    case 'f': return TOKEN.FALSE;
                    case 'n': return TOKEN.NULL;
                    default:
                        if (char.IsDigit(c) || c == '-') return TOKEN.NUMBER;
                        return TOKEN.NONE;
                }
            }
        }

        void EatWhitespace()
        {
            while (char.IsWhiteSpace(PeekChar))
            {
                json.Read();
                if (json.Peek() == -1) break;
            }
        }

        bool IsWordBreak(char c) => WORD_BREAK.IndexOf(c) != -1;
    }

    class StringReader
    {
        readonly string s;
        int pos;

        public StringReader(string s) { this.s = s; }

        public int Peek() => (pos < s.Length) ? s[pos] : -1;

        public int Read() => (pos < s.Length) ? s[pos++] : -1;
    }
}
