# OrderedSet

[![NuGet](https://img.shields.io/nuget/v/OrderedSet.svg)](https://www.nuget.org/packages/OrderedSet)

An ordered version of HashSet.

## Example

```cs
OrderedSet<string> set = new();
set.Add("pizza");
set.Add("hotdog");
set.Add("pizza");
string setString = string.Join(", ", set); // "pizza, hotdog"
```