# 2023-digital-t-level-apprentice

Welcome to the main branch of this repo.

## WebAPI Badges

| Test coverage | Build status |
|---------|---------|
| ![Code Coverage](https://img.shields.io/badge/Code%20Coverage-100%25-green?style=flat) | [![(WebApi) Build and deploy ASP.Net Core app to Azure](https://github.com/jamie-taylor-rjj/2023-digital-t-level-apprentice/actions/workflows/main_2023-invoice-gen.yml/badge.svg?branch=main)](https://github.com/jamie-taylor-rjj/2023-digital-t-level-apprentice/actions/workflows/main_2023-invoice-gen.yml) |

Please see the [Glossary](./GLOSSARY.MD) for descriptions of some keywords we'll be using. Also please see the [Common commands](/COMMON-COMMANDS.MD) for some common git commands.

You may need to get permission to install the [git command line application for Windows](https://git-scm.com/download/win) before you can perform a lot of these actions. If you would prefer, you can install the [GitHub Desktop application](https://desktop.github.com/) - this will wrap all of the git CLI commands in a UI.

This repo has some GitHub specific files (in the [.github directory](./.github/)) which are designed to make things a little easier to manage from an infrastructure point of view. These files form the basis of some basic [DevOps](https://en.wikipedia.org/wiki/DevOps) practises. Please note that by default this directory might be hidden from view, as it is known as a "dot directory" and by default these folders are hidden from view.

## Directories

A number of directories have been created in this repo. These directories represent the standard layout for RJJ-based projects.

## Tasks

Prior to this version of the repo, there was a "Tasks" section of the readme. This section has been deleted (though recoverable in source control if we need it), as the tasks have been completed.

## Database

You will require the EF Core tools: `dotnet tool install --global dotnet-ef`

## Creating the Database

Populating the database is a little janky at the moment (as of June 3rd, 2023):

1. open a terminal at `src/Invoice_Gen.Domain`
1. ` dotnet ef database update` to apply any migrations
1. an `invoiceDatabase.db` file will be created
1. copy this to `src/Invoice_Gen.WebApi`
1. run the application

## Migrating the Database

1. open a terminal at `src/Invoice_Gen.Domain`
1. run `dotnet ef migrations add {name}` where "{name}" is the name of your migration
1. a Migrations folder will be present and will contain the DB migration
