
# jrnl

_A simple CLI journal_

## Usage

Add a new entry

```shell
$ jrnl new
Title (Optional): My Walk
Write a journal entry (Press enter to save):
I took a nice walk today
New entry saved: My Walk | 1/2/2025
```

View list of all entries

```shell
$ jrnl list
Your journal has (1) entry: 
  1 | My Walk                  | 1/2/2025   | I took a nice walk today
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
$ jrnl read "My Walk"
```

Delete an entry by ID

```shell
$ jrnl delete 1
1 | My Walk | 1/2/25 | I took a nice walk today
Delete this entry? (Y/N) Y
Entry was deleted.
```

Help

```shell
$ jrnl --help
jrnl
v1.0.0

Usage
    new               - Add a new entry
    list              - View list of all entries
    read [title|id]   - View full text of an entry 
    delete [id]       - Delete a specific entry
```
