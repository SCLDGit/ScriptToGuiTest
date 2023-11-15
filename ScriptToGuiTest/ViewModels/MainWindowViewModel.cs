using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using Avalonia.Collections;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ScriptToGuiTest.Models;

namespace ScriptToGuiTest.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            this.WhenAnyValue(p_vm => p_vm.Script)
                .Throttle(TimeSpan.FromMilliseconds(500), RxApp.MainThreadScheduler)
                .Subscribe(OnScriptChanged);
        }

        public            AvaloniaList<IScriptValueStub> ScriptValues { get; }      = new();
        [Reactive] public string                         Script       { get; set; } = string.Empty;
        
        private void OnScriptChanged(string p_script)
        {
            var scriptVariables = GetScriptVariables(p_script).ToList();

            foreach (var variable in scriptVariables)
            {
                var matchingVariable = ScriptValues.FirstOrDefault(p_scriptValue => p_scriptValue.Name == variable.Name);

                if (matchingVariable is null)
                {
                    ScriptValues.Add(variable);
                }
                else
                {
                    matchingVariable.Description = variable.Description;
                }
            }

            foreach (var value in from value in ScriptValues.ToList() let matchingVariable = scriptVariables.FirstOrDefault(p_scriptValue => p_scriptValue.Name == value.Name) where matchingVariable is null select value)
            {
                ScriptValues.Remove(value);
            }
        }

        private static IEnumerable<IScriptValueStub> GetScriptVariables(string p_script)
        {
            var scriptVariables = new List<IScriptValueStub>();

            foreach (Match match in Regex.Matches(p_script,"(?<=\\${{)(.*?)(?=}})"))
            {
                var matchData = match.ToString().TrimStart('$', '{', '{').TrimEnd('}', '}');

                if (!matchData.Count(p_char => p_char == '|').Equals(2))
                {
                    continue;
                }

                try
                {
                    scriptVariables.Add(IScriptValueStub.CreateFromScript(matchData));
                }
                catch
                {
                    // Do nothing. - Comment by Matt Heimlich on 10/31/2023@14:20:52
                }
            }

            return scriptVariables;
        }
    }
}