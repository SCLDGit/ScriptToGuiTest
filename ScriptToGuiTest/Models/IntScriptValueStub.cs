using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ScriptToGuiTest.Models
{
    public class IntScriptValueStub : ReactiveObject, IScriptValueStub
    {
        [Reactive] public string Name        { get; set; } = string.Empty;
        [Reactive] public string Description { get; set; } = string.Empty;
        
        [Reactive] public int Value { get; set; }
        
        public static IntScriptValueStub CreateFromScript(string p_script)
        {
            var splitString = p_script.Split('|');

            return new IntScriptValueStub
                   {
                       Name        = splitString[1].Trim(),
                       Description = splitString[2].Trim(),
                       Value       = 0
                   };
        }
    }
}