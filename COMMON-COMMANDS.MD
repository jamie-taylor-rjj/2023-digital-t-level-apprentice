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
