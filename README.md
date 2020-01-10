# C# .NET MovableTypeParser

[![Build Status](https://dev.azure.com/duanegibbs/MovableTypeParser/_apis/build/status/d-gibbs.MovableTypeParser?branchName=master)](https://dev.azure.com/duanegibbs/MovableTypeParser/_build/latest?definitionId=1&branchName=master)

Easily parse mtif (movable type import files) into C# strongly typed objects.

# Usage
Create an instance of an `MTIFParser` providing the path to your file and call the `Parse()` method - thats it!
```
var parser = new MTIFParser("/path-to-file/export.txt");
var posts = parser.Parse();
```

### Installation

Install package from nuget.org or clone repository and build locally.

### Dependencies

- None

License
----

MIT
