using System;
using System.Globalization;
using System.Text;

namespace MorseCodeTranslator
{
    public static class Translator
    {
        public static string TranslateToMorse(string? message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            string array = string.Empty;
            for (int i = 0; i < message.Length; i++)
            {
                for (int j = 0; j < MorseCodes.CodeTable.GetLength(0); j++)
                {
                    if (char.ToUpper(message[i], CultureInfo.CurrentCulture) == MorseCodes.CodeTable[j][0])
                    {
                        string temp = string.Empty;
                        for (int k = 1; k < MorseCodes.CodeTable[j].Length; k++)
                        {
                            temp = new string(temp + MorseCodes.CodeTable[j][k]);
                        }

                        if (i < message.Length - 1)
                        {
                            temp = new string(temp + " ");
                        }

                        array = new string(array + temp);
                        break;
                    }
                }
            }

            if (array.Length > 0 && array[array.Length - 1] == ' ')
            {
                array = array.Substring(0, array.Length - 1);
            }

            return array;
        }

        public static string TranslateToText(string? morseMessage)
        {
            if (morseMessage is null)
            {
                throw new ArgumentNullException(nameof(morseMessage));
            }

            string array = string.Empty;
            string[] numbers = morseMessage.Split(' ');
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < MorseCodes.CodeTable.GetLength(0); j++)
                {
                    bool b = true;
                    for (int k = 1; k < MorseCodes.CodeTable[j].Length; k++)
                    {
                        if (numbers[i].Length != MorseCodes.CodeTable[j].Length - 1)
                        {
                            b = false;
                            break;
                        }

                        if (numbers[i][k - 1] != MorseCodes.CodeTable[j][k])
                        {
                            b = false;
                            break;
                        }
                    }

                    if (b)
                    {
                        array = new string(array + MorseCodes.CodeTable[j][0]);
                        break;
                    }
                }
            }

            return array;
        }

        public static void WriteMorse(char[][]? codeTable, string message, StringBuilder? morseMessageBuilder, char dot = '.', char dash = '-', char separator = ' ')
        {
            if (message is null || codeTable is null || morseMessageBuilder is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            string array = string.Empty;
            for (int i = 0; i < message.Length; i++)
            {
                for (int j = 0; j < codeTable.GetLength(0); j++)
                {
                    if (char.ToUpper(message[i], CultureInfo.CurrentCulture) == codeTable[j][0])
                    {
                        string temp = string.Empty;
                        for (int k = 1; k < MorseCodes.CodeTable[j].Length; k++)
                        {
                            if (codeTable[j][k] == '.')
                            {
                                temp = new string(temp + dot);
                            }
                            else
                            {
                                temp = new string(temp + dash);
                            }
                        }

                        if (i < message.Length - 1)
                        {
                            temp = new string(temp + separator);
                        }

                        array = new string(array + temp);
                        break;
                    }
                }
            }

            if (array.Length > 0 && array[array.Length - 1] == separator)
            {
                array = array.Substring(0, array.Length - 1);
            }

            morseMessageBuilder.Append(array);
        }

        public static void WriteText(char[][]? codeTable, string? morseMessage, StringBuilder? messageBuilder, char dot = '.', char dash = '-', char separator = ' ')
        {
            if (morseMessage is null || codeTable is null || messageBuilder is null)
            {
                throw new ArgumentNullException(nameof(morseMessage));
            }

            string array = string.Empty;
            string[] numbers = morseMessage.Split(separator);
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < codeTable.GetLength(0); j++)
                {
                    bool b = true;
                    for (int k = 1; k < codeTable[j].Length; k++)
                    {
                        if (numbers[i].Length != codeTable[j].Length - 1)
                        {
                            b = false;
                            break;
                        }

                        if ((numbers[i][k - 1] == dot && codeTable[j][k] != '.') || (numbers[i][k - 1] == dash && codeTable[j][k] != '-'))
                        {
                            b = false;
                            break;
                        }
                    }

                    if (b)
                    {
                        array = new string(array + codeTable[j][0]);
                        break;
                    }
                }
            }

            messageBuilder.Append(array);
        }
    }
}
