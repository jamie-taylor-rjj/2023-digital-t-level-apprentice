# Glossary

The following are overly simplified (i.e lacking in nuance) descriptions of terms listed in the README:

### Repository

A repository (or repo) is a logical collection of code. A single repo usually either represents an entire solution. However, there are those who like to split a project into smaller "domain boundaries" (especially when doing )[Domain-Driven Development](https://en.wikipedia.org/wiki/Domain-driven_design)), with each of those smaller domains in repos of their own.

We may touch on this later.

### Clone

This is the act of copying a repository down to your local machine. From here, you can inspect the code, make changes, commit them to your local git history, and push them back to your remote clone.

### Commit

When you are ready to make a permanent change to a code repo, you will commit the change. This tells your local instance of git that the changes you have made should be added to the history of the repo. Usually you will provide a message with your commit, this lets other people (and you are another person, too - more on that in person) know why you made the change that you did.

### Push

git is a distributed version control system. It is built with the idea of local and remote repos in mind. When interacting with a git repo which is attached to GitHub (or GitLab, BitBucket, or any other git service) you have (at least) two copies of the repo:

- you local version; what is on your machine
- a remote; the version on GitHub (or GitLab, BitBucket, or any other git service)

When you are done with your local changes, you will push them back to the remote.

### Branch

Branching is very similar to copying a file so that you can work on it whilst having a backup: the copy of the file doesn't get edited, keeping it in the original state, and you selectively copy the edits back into the original file.

That's essentially what a branch allows you to do in a git context (remember: these descriptions are very over simplified)

### Fork

taking a fork of a repository creates a copy of it. That copy is then stored under your user in GitHub. If your username is `RobinIsGreat` and you fork this repository (called `jamie-taylor-rjj/2023-digital-t-level-apprentice`), then your copy will be called `RobinIsGreat/2023-digital-t-level-apprentice`.

Forking a repo is a good idea because it allows you to experiment with the repo and make changes without affecting anyone else who is working "upstream"

### Upstream

Whenever you fork a repo, the place where you forked it from is called upstream. If you forked this repo from jamie-taylor-rjj/2023-digital-t-level-apprentice, then jamie-taylor-rjj/2023-digital-t-level-apprentice is the upstream of this repo). When you are done making changes to your fork of the repo, you will need to submit a "pull request" for the upstream version to pull your changes into their repo.

### Pull Request

When you have completed a number of changes to a forked repo, you will likely issue a pull request to upstream. This is a request which lays out all of the changes you have made, a rationale for them, and lists any test that you have run on the repo.

This allows the maintainers of upstream to carefully consider your changes and bring them into upstream. Remember: once your changes have been brought into upstream, it becomes the responsibility of the upstream maintainers to maintain your code; as such, try to make it clear and concise by adopting any coding standards that they use, and provide enough documentation such that they can understand why your code does what it does.
