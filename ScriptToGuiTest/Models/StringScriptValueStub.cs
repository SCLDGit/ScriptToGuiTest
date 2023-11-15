using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ScriptToGuiTest.Models
{
    public class StringScriptValueStub : ReactiveObject, IScriptValueStub
    {
        [Reactive] public string Name        { get; set; } = string.Empty;
        [Reactive] public string Description { get; set; } = string.Empty;

        [Reactive] public string Value { get; set; } = string.Empty;

        public static StringScriptValueStub CreateFromScript(string p_script)
        {
            var splitString = p_script.Split('|');

            return new StringScriptValueStub
                   {
                       Name        = splitString[1].Trim(),
                       Description = splitString[2].Trim(),
                       Value       = string.Empty
                   };
        }
    }
}