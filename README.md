# NoName
To run this shit you need:
<!-- 1. Build Cpp.CompilerProxy.Core.
2. Open NoName\x64\{Debug/Release}
3. Rename Cpp.CompilerProxy.Core.exe to CompilerProxy.exe and copy it to NoName\WorkerService.Stager\bin\{Debug/Release}\{.netversion} -->
4. Ask to add your IP to the mongo cluster or create a local one.
5. If creating a local one connection string should be changed in CodeRunManager.Api and WorkerService.Stager appsettings.json

Test cases source.
Right now test cases are loaded from a mongo cluster. But also you can load them from \NoName\TestCases\TestCases\ in a json format or
Use a stub to define test cases manually. All the implementations are located in \NoName\TestCases\Providers\ and to use one replace appropriate
interface implementation in the NoName\TestCases\Extensions\ServiceCollectionExtensions.cs

If you need some data, run CodeProblemPatcher.
It will load some real problems into the database.
Although it would not load test cases so if they are absent in mongo and locally, you should take care of this.

