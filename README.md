# NUnitTestResultSummary

Just a "quick" implementation without asking for good code for your own needs.

## Output

### Markdown Example
> ![Total Tests](https://img.shields.io/badge/Total_Tests-4-white)
> ![Passed Tests](https://img.shields.io/badge/Passed_Tests-3-green)
> ![Failed Tests](https://img.shields.io/badge/Failed_Tests-1-red)
> ![Skipped Tests](https://img.shields.io/badge/Skipped_Tests-0-blue)
> ![Warning Tests](https://img.shields.io/badge/Warning_Tests-0-orange)
> ![Inconclusive Tests](https://img.shields.io/badge/Inconclusive_Tests-0-white)
>
> <details>
> <summary>Failed Tests</summary>
>
> Name | Result | Reason 
> --- | --- | --- 
> ```Your Test Name``` | Failed :red_circle: | <details><summary>Details</summary> Actual: 3; Expected: 5</details>
>
> </details>

> <details>
> <summary>Passed Tests</summary>
>
> Name |Result |Duration |
> --- | --- | --- | 
> ```Your Test Name 1``` |Passed :green_circle: |00:00:01.4840840 |
> ```Your Test Name 2``` |Passed :green_circle: |00:00:00.1617480 |
> ```Your Test Name 3``` |Passed :green_circle: |00:00:00.2888900 |
>
> </details> 


## Usage

```yaml
name: NUnit Tests Report
uses: daazarov/NUnitTestResultSummary@v1
with:
  filename: nunit.test.results.xml
```


### .Net Workflow Example

```yaml
name: .Net 6 CI Build

on:
  push:
    branches: [ master ]
  pull_request:
    branches: [ master ]

jobs:
  build:
    runs-on: ubuntu-latest
    name: CI Build
    steps:
    - name: Checkout
      uses: actions/checkout@v3

    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 6.0.x

    - name: Test
      run: dotnet test ${{*.csproj file}} -- NUnit.TestOutputXml=TestResults
    
    - name: NUnit Tests Report
      uses: daazarov/NUnitTestResultSummary@v1
      with:
        filename: ..../TestResults/{filename}.xml
        badge: true
        format: markdown
        output: file
        passed: true
        
    - name: Commit Tests Results
      uses: EndBug/add-and-commit@v9
      with:
        default_author: github_actions
        message: 'Add/Replace NUnit results'
        add: 'nunit-testresult-summary.md --force'
```
