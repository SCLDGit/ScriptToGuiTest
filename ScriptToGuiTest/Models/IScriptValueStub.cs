namespace ScriptToGuiTest.Models
{
    public interface IScriptValueStub
    {
        string Name { get; set; }
        string Description { get; set; }
        
        public static IScriptValueStub CreateFromScript(string p_script)
        {
            return p_script.Split('|')[0].ToUpper().Trim() switch
                   {
                       "STRING" => StringScriptValueStub.CreateFromScript(p_script),
                       "INT" => IntScriptValueStub.CreateFromScript(p_script)
                   };
        }
    }
}