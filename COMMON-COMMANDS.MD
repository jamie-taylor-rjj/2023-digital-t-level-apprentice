# Common Commands

This page lists a number of commonly used git commands for your convenience.

### Adding a file to the change tracker

`git add <filepath1>` will add the file at the supplied path (i.e. `<filepath>`) to gits list of tracked changes.

### Committing a tracked change to memory

`git commit -m '<message>'` will commit all tracked changes (since the last commit) using the message supplied (i.e. `<message>)`. Remember to wrap the message in single or double quotes.

### Pushing committed changes to a remote

`git push` will push all recently committed changes to a remote repo.

### Pulling changes from a remote

`git pull` will pull any changes from a remote repo. You will only need to do this if something has changed at the remote since you last cloned or pushed.

### Checking the status of the git change tracker

`git status` will display any and all changes that git detects, with options to perform. An example of the `status` command (taken when developing this repo) would be:

```bash
jamie$: git status
On branch main
Your branch is ahead of 'origin/main' by 2 commits.
  (use "git push" to publish your local commits)

Changes not staged for commit:
  (use "git add <file>..." to update what will be committed)
  (use "git restore <file>..." to discard changes in working directory)
        modified:   COMMON-COMMANDS.MD

Untracked files:
  (use "git add <file>..." to include in what will be committed)
        .github/
        GLOSSARY.MD
```
