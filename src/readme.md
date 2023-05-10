# src Directory

This directory is where we'll store projects, with a directory per project. An example might be:

```bash
├── src
│   ├── App.UI
│   │   ├── App.UI.csproj
│   │   ├── program.cs
│   │   ├── etc.
│   ├── App.WebApi
│   │   ├── App.WebApi.csproj
│   │   ├── program.cs
│   │   ├── etc.
│   ├── App.Services
│   │   ├── App.Services.csproj
│   │   ├── program.cs
│   │   ├── etc.
├── App.UI.sln
├── App.WebApi.sln
└── .gitignore
```

As the src directory will contain all of the source files for our applications, we'll need to have two solution files:

1. for the UI application
1. for the WebAPI application

This will help us by allowing us to only load the solution that we want to edit, keeping the IDE footprint low. These two solution files will be kept outside of the src directory, but are mentioned here for clarity.
