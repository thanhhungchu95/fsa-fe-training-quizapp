# Git command

## Tell Git who you are

Configure the author name and email address to be used with your commits.
Note that Git strips some characters (for example trailing periods) from user.name.

```bash
git config --global user.name "Sam Smith"
git config --global user.email sam@example.com
```

## Check out a repository

Create a working copy of a local repository:

```bash
git clone /path/to/repository
```

For a remote server, use:

```bash
git clone username@host:/path/to/repository
```

## Commit

Commit changes to head (but not yet to the remote repository):

```bash
git commit -m "Commit message"
```

Commit any files you've added with git add, and also commit any files you've changed since then:

```bash
git commit -a
```

## Push

Send changes to the master branch of your remote repository:

```bash
git push origin master
```

## Status

List the files you've changed and those you still need to add or commit:

```bash
git status
```

## Branches

Create a new branch and switch to it:

```bash
git checkout -b <branchname>
```

Switch from one branch to another:

```bash
git checkout <branchname>
```

List all the branches in your repo, and also tell you what branch you're currently in:

```bash
git branch
```

Delete the feature branch:

```bash
git branch -d <branchname>
```

Push the branch to your remote repository, so others can use it:

```bash
git push origin <branchname>
```

Push all branches to your remote repository:

```bash
git push --all origin
```

Delete a branch on your remote repository:

```bash
git push origin --delete <branchname>
```

## How to make Git “forget” about a file that was tracked but is now in .gitignore?

.gitignore will prevent untracked files from being added (without an add -f) to the set of files tracked by git, however git will continue to track any files that are already being tracked.

To stop tracking a file you need to remove it from the index. This can be achieved with this command.

```bash
git rm --cached <file>
```

If you want to remove a whole folder, you need to remove all files in it recursively.

```bash
git rm -r --cached <folder>
```

The removal of the file from the head revision will happen on the next commit.
