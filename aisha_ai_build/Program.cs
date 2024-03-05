using aisha_ai_build.Services;

internal class Program
{
    public static void Main(string[] args)
    {
        var scriptGenerationService = new ScriptGenerationService();
        scriptGenerationService.GenerateBuildScript();
    }
}