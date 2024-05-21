<br>
<h3 align="center">SpotLights | Developer Community Blog Portal </h3>
<p align="center">
    SpotLights is a self-hosted open source publishing platform written in ASP.NET and Blazor WebAssembly. 
    It can be used to quickly and easily set up a lightweight, but fully functional personal or group blog.
</p>
<br>

English 

## Installation
 

#### native build
1. [Download](https://dotnet.microsoft.com/zh-cn/download) .NET 8.0 SDK Choose to install the system version on your host. [Download](https://nodejs.org/) Nodejs 14 and above and install it on your host. For linux you can use the package management tool
2. Navigate to the project root directory, run ./publish.cmd on the command line in widnows, run sh ./publish.sh on the command line in linux.
3. When the command execution is complete and there are no errors, you will see the dist folder in the project root directory, which is the application after publishing. You can copy it to run anywhere. In windows, you can directly click to run the dist folder SpotLights.exe , in linux, please authorize the executable permission of the SpotLights binary file first and then click or run it on the command line. [note] Because the app_data directory does not exist in the release, an error may occur when the program starts. Just start it again.
4. Then you can open `localhost:5000` with your browser
3. Done, enjoy.

 

## Development
If you want to customize the SpotLights, or contribute:

1. [Download](https://dotnet.microsoft.com/download/dotnet) and Install .NET SDK.
2. [Download](https://nodejs.org/) and Install NodeJs.
2. Download, fork, or clone the repository.
3. Open the project with your favorite IDE (VS Code, Visual Studio, Atom, etc).
4. Run the app with your IDE or these commands:
```
$ cd /your-local-path/SpotLights/src/SpotLights/
$ dotnet run
```
Then you can open `localhost:5000` with your browser
 
