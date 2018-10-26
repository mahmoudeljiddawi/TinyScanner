using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Token_Class
{
    End, Else, If, Integer,Read, Then, Until, Write, Dot, Semicolon,
    Comma, LParanthesis, RParanthesis, EqualOp, LessThanOp,
    GreaterThanOp, NotEqualOp, PlusOp, MinusOp, MultiplyOp, DivideOp,
    Idenifier, LSquareBracket, RSquareBracket, Return, Repeat,
    Endl, AssignmentOp, String, Float, OrOp, AndOp, ElseIf, Comment,
    IntegerNum, FloatNum
}                       

namespace TinyScanner
{
    public class Token
    {
        public string lex;
        public Token_Class token_type;
    }
    class Scanner
    {
        public List<Token> Tokens = new List<Token>();
        Dictionary<string, Token_Class> ReservedWords = new Dictionary<string, Token_Class>();
        Dictionary<string, Token_Class> Operators = new Dictionary<string, Token_Class>();
        public List<string> Errors = new List<string>();

        public Scanner()
        {
            ReservedWords.Add("if", Token_Class.If);
            ReservedWords.Add("end", Token_Class.End);
            ReservedWords.Add("endl", Token_Class.Endl);
            ReservedWords.Add("else", Token_Class.Else);
            ReservedWords.Add("elseif", Token_Class.ElseIf);
            ReservedWords.Add("int", Token_Class.Integer);
            ReservedWords.Add("read", Token_Class.Read);
            ReservedWords.Add("then", Token_Class.Then);
            ReservedWords.Add("until", Token_Class.Until);
            ReservedWords.Add("write", Token_Class.Write);
            ReservedWords.Add("return", Token_Class.Return);
            ReservedWords.Add("repeat", Token_Class.Repeat);
            ReservedWords.Add("string", Token_Class.String);
            ReservedWords.Add("float", Token_Class.Float);

            Operators.Add(".", Token_Class.Dot);
            Operators.Add(";", Token_Class.Semicolon);
            Operators.Add(",", Token_Class.Comma);
            Operators.Add("(", Token_Class.LParanthesis);
            Operators.Add(")", Token_Class.RParanthesis);
            Operators.Add(":=", Token_Class.AssignmentOp);
            Operators.Add("=", Token_Class.EqualOp);
            Operators.Add("<", Token_Class.LessThanOp);
            Operators.Add(">", Token_Class.GreaterThanOp);
            Operators.Add("<>", Token_Class.NotEqualOp);
            Operators.Add("+", Token_Class.PlusOp);
            Operators.Add("-", Token_Class.MinusOp);
            Operators.Add("*", Token_Class.MultiplyOp);
            Operators.Add("/", Token_Class.DivideOp);
            Operators.Add("{", Token_Class.LSquareBracket);
            Operators.Add("}", Token_Class.RSquareBracket);
            Operators.Add("||", Token_Class.OrOp);
            Operators.Add("&&", Token_Class.AndOp);



        }
        public void StartScanning(string SourceCode)
        {
            for (int i = 0; i < SourceCode.Length; i++)
            {
                int j = i;
                char CurrentChar = SourceCode[i];
                string CurrentLexeme = CurrentChar.ToString();

                if (CurrentChar == ' ' || CurrentChar == '\r' || CurrentChar == '\n')
                    continue;

                if ((CurrentChar >= 'a' && CurrentChar <= 'z')) //if you read a character
                {
                    j++;
                    while((SourceCode[j] >='a' && SourceCode[j]<='z') || (SourceCode[j] >= '0' && SourceCode[j] <= '9'))
                    {
                        CurrentLexeme += SourceCode[j];
                        j++;
                    }
                    if ( ReservedWords.ContainsKey(CurrentLexeme))
                    {
                        Token T = new Token();
                        T.lex = CurrentLexeme;
                        T.token_type = ReservedWords[CurrentLexeme];
                        Tokens.Add(T);
                    }
                    else
                    {
                        Token T = new Token();
                        T.lex = CurrentLexeme;
                        T.token_type = Token_Class.Idenifier;
                        Tokens.Add(T);
                    }
                }

                else if (CurrentChar >= '0' && CurrentChar <= '9')
                {
                    int tmp = 0;
                    j++;
                    while (SourceCode[j] >= '0' && SourceCode[j] <= '9')
                    {
                        CurrentLexeme += SourceCode[j];
                        j++;
                    }
                    if (SourceCode[j]=='.')
                    {
                        tmp = 1;
                        CurrentLexeme += SourceCode[j];
                        j++;
                        while (SourceCode[j] >= '0' && SourceCode[j] <= '9')
                        {
                            CurrentLexeme += SourceCode[j];
                            j++;
                        }
                    }
                    if ( SourceCode[j] != ';' && SourceCode[j] != ' ' && SourceCode[j] == '\r' && SourceCode[j] == '\n')
                    {
                        CurrentLexeme += SourceCode[j];
                        j++;
                        while(SourceCode[j] != ';' && SourceCode[j] != ' ' && SourceCode[j] == '\r' && SourceCode[j] == '\n')
                        {
                            CurrentLexeme += SourceCode[j];
                            j++;
                        }
                        Errors.Add(System.Environment.NewLine);
                        Errors.Add("*** ");
                        Errors.Add(CurrentLexeme);
                        j++;
                    }
                    else if (tmp == 1)
                    {
                        Token T = new Token();
                        T.lex = CurrentLexeme;
                        T.token_type = Token_Class.FloatNum;
                        Tokens.Add(T);
                    }
                    else
                    {
                        Token T = new Token();
                        T.lex = CurrentLexeme;
                        T.token_type = Token_Class.IntegerNum;
                        Tokens.Add(T);
                    }

                }
                else if (SourceCode[j] == '/' && SourceCode[j+1]=='*')
                {
                    int tmp = 0;
                    j++;
                    CurrentLexeme += SourceCode[j];
                    j++;
                    while ( SourceCode[j] !='*' || SourceCode[j+1] !='/')
                    {
                        if ( j==SourceCode.Length-2)
                        {
                            tmp = 1;
                            CurrentLexeme += SourceCode[j];
                            j++;
                            CurrentLexeme += SourceCode[j];
                            j++;
                            Errors.Add(System.Environment.NewLine);
                            Errors.Add("*** ");
                            Errors.Add(CurrentLexeme);
                            j++;
                            break;
                        }
                        CurrentLexeme += SourceCode[j];
                        j++;
                    }
                    if (tmp == 0)
                    {
                        CurrentLexeme += SourceCode[j];
                        j++;
                        CurrentLexeme += SourceCode[j];
                        j++;

                        Token T = new Token();
                        T.lex = CurrentLexeme;
                        T.token_type = Token_Class.Comment;
                        Tokens.Add(T);
                    }
                }
                else if (SourceCode[j] == '"')
                {
                    j++;
                    if (j < SourceCode.Length - 1)
                    {
                        while (SourceCode[j] != '"' && SourceCode[j] != '\n')
                        {
                            CurrentLexeme += SourceCode[j];
                            j++;
                        }
                        if (SourceCode[j] == '\n')
                        {
                            Errors.Add(System.Environment.NewLine);
                            Errors.Add("*** ");
                            Errors.Add(CurrentLexeme);
                            j++;
                        }
                        else
                        {
                            CurrentLexeme += SourceCode[j];
                            Token T = new Token();
                            T.lex = CurrentLexeme;
                            T.token_type = Token_Class.String;
                            Tokens.Add(T);
                            j++;
                        }
                    }
                }
                else if (SourceCode[j] == ':' && SourceCode[j + 1] == '=')
                {
                    j++;
                    CurrentLexeme += SourceCode[j];
                    Token T = new Token();
                    T.lex = CurrentLexeme;
                    T.token_type = Token_Class.AssignmentOp;
                    Tokens.Add(T);
                    j++;
                }
                else if (SourceCode[j] == '|' && SourceCode[j + 1] == '|')
                {
                    j++;
                    CurrentLexeme += SourceCode[j];
                    Token T = new Token();
                    T.lex = CurrentLexeme;
                    T.token_type = Token_Class.OrOp;
                    Tokens.Add(T);
                    j++;
                }
                else if (SourceCode[j] == '<' && SourceCode[j + 1] == '>')
                {
                    j++;
                    CurrentLexeme += SourceCode[j];
                    Token T = new Token();
                    T.lex = CurrentLexeme;
                    T.token_type = Token_Class.NotEqualOp;
                    Tokens.Add(T);
                    j++;
                }
                else if (SourceCode[j] == '&' && SourceCode[j + 1] == '&')
                {
                    j++;
                    CurrentLexeme += SourceCode[j];
                    Token T = new Token();
                    T.lex = CurrentLexeme;
                    T.token_type = Token_Class.AndOp;
                    Tokens.Add(T);
                    j++;
                }
                else
                {
                    if (Operators.ContainsKey(CurrentLexeme))
                    {
                        Token T = new Token();
                        T.lex = CurrentLexeme;
                        T.token_type = Operators[CurrentLexeme];
                        Tokens.Add(T);
                        j++;
                    }
                    else
                    {
                        Errors.Add(System.Environment.NewLine);
                        Errors.Add("*** ");
                        Errors.Add(CurrentLexeme);
                        j++;
                    }
                }
                i = j-1;
            }

        }
    }
}
