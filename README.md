
# jrnl

_A simple command-line journal app using a local SQLite database and EntityFrameworkCore_

## Download

[Latest Release](https://github.com/edwinchoate/jrnl/releases/latest)

## Usage

Add a new entry

```shell
$ jrnl new
Title (Optional): My Walk
Write a journal entry (Press enter to save):
I took a nice walk today
New entry saved: My Walk | 01/02/25
```

View list of all entries

```shell
$ jrnl list
Your journal has (1) entry: 
  1 | My Walk          | 01/02/25 | I took a nice walk today
```

Read an entry by ID

```shell
$ jrnl read 1
My Walk
Thursday, January 2, 2025 3:15 PM 
I took a nice walk today
```

Or, read an entry by title

```shell
$ jrnl read "My Walk"
```

Delete an entry by ID

```shell
$ jrnl delete 1
1 | My Walk | 01/02/25 | I took a nice walk today
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
