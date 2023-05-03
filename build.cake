#tool nuget:?package=ReportGenerator&version=4.8.13

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");
var solution = "./src/mcpride-markdig-extensions.sln";

Task("Clean")
    .Does(() =>
    {
        Information("Cleaning files...");
        CleanDirectories("./src/**/obj");
        CleanDirectories("./src/**/bin");
        CleanDirectories("./test-results");
        CleanDirectories("./coverage");
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

            var testResultsDirectory = MakeAbsolute(Directory($"./test-results/{file.GetFilenameWithoutExtension()}"));

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
                            "console;verbosity=normal"
                        },
                    ResultsDirectory = testResultsDirectory,
                    Collectors = new []
                        {
                            "XPlat Code Coverage"
                        }
                };

            DotNetTest(file.FullPath, testSettings);

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
                ReportGeneratorReportType.Html, ReportGeneratorReportType.TextSummary
            }
        };
        ReportGenerator(new GlobPattern("./test-results/**/coverage.cobertura.xml"), coverageDirectory, reportSettings);
        var summary = System.IO.File.ReadAllText("./coverage/Summary.txt");
        Information(summary);
    });


Task("Default")
  .IsDependentOn("Coverage");
RunTarget(target);

