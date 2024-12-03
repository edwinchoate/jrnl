
# jrnl

_A simple CLI journal_

## Usage

Add a new entry

```shell
$ jrnl new
Title (Optional): My Walk
Press enter to finish entry
Write a journal entry...
I took a nice walk today
New entry saved: My Walk | 1/2/25
```

View all entries

```shell
$ jrnl list
Your journal has (1) entry: 
1 | My Walk | 1/2/25 | I took a nice walk today
```

Read an entry by ID

```shell
$ jrnl read 1
My Walk
Jan 2, 2025 at 3:15pm 
I took a nice walk today
```

Or, read an entry by title

```shell
$ jrnl read My Walk
```
