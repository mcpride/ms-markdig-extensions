#tool nuget:?package=ReportGenerator&version=4.8.13
#addin nuget:?package=Cake.Coverlet&version=3.0.2
#addin nuget:?package=Cake.Incubator&version=8.0.0

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");
var solution = "./src/mcpride-markdig-extensions.sln";

bool IsTestProject(SolutionProject project)
{
    return (project.Path.HasExtension && project.Name.ToString().EndsWith("Tests"));
} 

Task("Clean")
    .Does(() =>
    {
        Information(string.Format("Cleaning files for configuration {0}...", configuration));
        CleanDirectories(string.Format("./src/**/obj/{0}", configuration));
        CleanDirectories(string.Format("./src/**/bin/{0}", configuration));
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        Information("Restoring packages...");
        DotNetRestore(solution);
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        var buildSettings = new DotNetBuildSettings{
            Configuration = configuration,
            NoRestore = true
        };

        Information("Building solution ...");
        DotNetBuild(solution, buildSettings);
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {       
        var projectFiles = GetFiles("./src/**/*.Tests.csproj");
        foreach(var file in projectFiles) {

            var testResultsDirectory = MakeAbsolute(Directory($"./coverage/{file.GetFilenameWithoutExtension()}"));

            EnsureDirectoryExists(testResultsDirectory);
            CleanDirectory(testResultsDirectory);

            var testSettings = new DotNetTestSettings 
                {
                    Configuration = configuration,
                    NoBuild = true,
                    NoRestore = true,
                    Verbosity = DotNetVerbosity.Normal,
                    Loggers = new [] 
                        {
                            "console;verbosity=normal",
                            $"trx;LogFileName={testResultsDirectory}/results.trx"
                        },
                    ResultsDirectory = testResultsDirectory,
                    Collectors = new []
                        {
                            "XPlat Code Coverage"
                        }
                };

            var coverletSettings = new CoverletSettings
                {
                    CollectCoverage = true,
                    CoverletOutputFormat = CoverletOutputFormat.cobertura,
                    CoverletOutputDirectory = testResultsDirectory,
                    CoverletOutputName = $"coverage"
                };

            DotNetTest(file, testSettings, coverletSettings);

        }
    });

Task("Coverage")
    .IsDependentOn("Test")
    .Does(() =>
    {
        var coverageDirectory = MakeAbsolute(Directory("./coverage"));

        var reportSettings = new ReportGeneratorSettings
        {
            ReportTypes = new [] 
            { 
                ReportGeneratorReportType.Html 
            }
        };
        ReportGenerator(new GlobPattern("./coverage/**/coverage.cobertura.xml"), coverageDirectory, reportSettings);
    });


Task("Default")
  .IsDependentOn("Coverage");
RunTarget(target);

