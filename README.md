# 2023-digital-t-level-apprentice

Welcome to the main branch of this repo.

Please see the [Glossary](./GLOSSARY.MD) for descriptions of some keywords we'll be using. Also please see the [Common commands](/COMMON-COMMANDS.MD) for some common git commands.

You may need to get permission to install the [git command line application for Windows](https://git-scm.com/download/win) before you can perform a lot of these actions. If you would prefer, you can install the [GitHub Desktop application](https://desktop.github.com/) - this will wrap all of the git CLI commands in a UI.

This repo has some GitHub specific files (in the [.github directory](./.github/)) which are designed to make things a little easier to manage from an infrastructure point of view. These files form the basis of some basic [DevOps](https://en.wikipedia.org/wiki/DevOps) practises.

## Tasks

Here are a couple tasks for you to undertake in order to get used to using git and GitHub:

1. Fork this repository using the steps outlined [here](https://docs.github.com/en/get-started/quickstart/fork-a-repo) †
1. Clone the repository using the steps outlined [here](https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository)
1. Create a local branch using the steps outlined [here](https://git-scm.com/book/en/v2/Git-Branching-Basic-Branching-and-Merging) called `feature/first-branch`
1. Add a file to the root of the repository called DONE.MD
1. Add this file to git's change tracker using `git add DONE.MD`
1. Commit this tracked file using `git commit -m "Added my first file"`
1. Push this branch to your GitHub remote using `git push -u origin feature/first-branch`
1. Create a Pull Request in the GitHub UI of the upstream of this repo using the steps outlined [here](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request)
1. Await the PR being approved

† = this page contains instructions for cloning a forked repository. For our exercise, only go that far. We don't need to cover anything after the "Configuring Git to sync your fork with the upstream repository" section.
