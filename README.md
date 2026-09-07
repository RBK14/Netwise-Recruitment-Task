# Netwise Recruitment Task

A .NET console application that fetches random cat facts from [catfact.ninja](https://catfact.ninja/fact) and appends each result as a new line to a local text file.

This project was built as a recruitment task.

## Features

- Fetch a cat fact on demand from the public Cat Fact API
- Append each fact as a new UTF-8 line to a local `.txt` file (file and directory are created automatically if missing)
- Interactive console controls: **Enter** to fetch, **Esc** (or **q**/**Q**)to exit
- Configurable API endpoint and output path via `appsettings.json`
- Dependency Injection with Microsoft Generic Host and a typed `HttpClient`
- Unit tests for the orchestration handler

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Getting started

Clone the repository and restore dependencies:

```bash
git clone https://github.com/RBK14/Netwise-Recruitment-Task.git
cd Netwise-Recruitment-Task
dotnet restore
```

### Run the application

```bash
dotnet run --project src/Netwise.RecruitmentTask
```

### Run tests

```bash
dotnet test
```

## Usage

After starting the app you will see:

```text
=== Cat Fact Fetcher ===
Press [ENTER] to fetch a cat fact and write it to the output file.
Press [ESC] to exit.
```


| Key              | Action                                            |
| ---------------- | ------------------------------------------------- |
| **Enter**        | Fetch a cat fact and append it to the output file |
| **Esc** or **q** / **Q** | Exit the application                              |


Each successful request appends one line to the output file. By default the file is written relative to the process working directory:

```text
OutputFiles/cat_facts.txt
```

Example content:

```text
A cat's field of vision is about 185 degrees.
Cats have five toes on each front paw, but only four toes on each back paw.
```

> **Note:** Because the output path is relative, the location of `OutputFiles` depends on the current working directory from which you run the app.

## Configuration

Settings live in `[src/Netwise.RecruitmentTask/appsettings.json](src/Netwise.RecruitmentTask/appsettings.json)`:

```json
{
  "ApiSettings": {
    "CatFactEndpoint": "https://catfact.ninja/fact"
  },
  "FileSettings": {
    "OutputDirectory": "OutputFiles",
    "OutputFileName": "cat_facts.txt"
  }
}
```


| Key                            | Description                                                  |
| ------------------------------ | ------------------------------------------------------------ |
| `ApiSettings:CatFactEndpoint`  | URL of the cat fact API                                      |
| `FileSettings:OutputDirectory` | Directory for the output file (created if it does not exist) |
| `FileSettings:OutputFileName`  | Name of the `.txt` file                                      |


## Project structure

```text
Netwise-Recruitment-Task/
├── src/Netwise.RecruitmentTask/
│   ├── Configuration/          # ApiSettings, FileSettings
│   ├── Execution/              # Orchestration handler
│   ├── Models/
│   │   ├── Domain/             # Domain model (CatFact)
│   │   └── Dtos/               # API response DTO
│   ├── Services/               # HTTP client and file writer
│   ├── Program.cs              # Host setup and console loop
│   └── appsettings.json
├── tests/Netwise.RecruitmentTask.Tests/
│   └── Execution/              # Handler unit tests
└── Netwise.RecruitmentTask.slnx
```

## How it works

```text
Enter key
   │
   ▼
ProcessCatFactHandler
   │
   ├── CatFactClient  →  GET https://catfact.ninja/fact
   │
   └── FileWriter     →  append fact to OutputFiles/cat_facts.txt
```

1. The console reads a key press.
2. On **Enter**, `ProcessCatFactHandler` requests a fact through `CatFactClient`.
3. If a fact is returned, `FileWriter` appends it as a new line to the configured text file.
4. On **Esc** / **q**, the application exits.

## License

This repository was created for a recruitment assignment and is not licensed for general reuse.