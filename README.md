# C# .NET MovableTypeParser
[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Easily parse .mtif (movable type import files) into C# strongly typed objects.

# Usage
Create an instance of an `MTIFParser` and provide the path to your .mtif - thats it!
```
var parser = new MTIFParser("/path-to-file/export.txt");
var posts = parser.Parse();
```

### Installation

Install package from nuget.org or clone repository and build locally.

### Dependencies

- None

### Todos

 - Add tests

License
----

MIT