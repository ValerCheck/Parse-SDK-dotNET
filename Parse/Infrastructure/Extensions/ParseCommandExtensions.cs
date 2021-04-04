using System;
using System.Collections.Generic;
using System.Text;
using Parse.Infrastructure.Execution;

namespace Parse.Infrastructure.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ParseCommandExtensions
    {
        public static ParseCommand AddHeader(this ParseCommand command, string key, string value)
        {
            command.Headers.Add(new KeyValuePair<string, string>(key, value));
            return command;
        }

        public static void AddHeaderIfValueIsValid<TValue>(this ParseCommand command, string key, TValue value, Func<TValue, bool> validator)
        {
            if (validator(value))
            {
                command.AddHeader(key, value.ToString());
            }
        }

        public static ParseCommand AddHeaderWithValidation(this ParseCommand command, string key, string textValue)
        {
            command.AddHeaderIfValueIsValid(key, textValue, (v) => !String.IsNullOrEmpty(v));
            return command;
        }
    }
}