# Introduction
A simple program that interpreters bytebeat written in C#.

Example:
```csharp
static void Main(string[] args)
{
    var data = new byte[12000]; // duration
    for(var t = 0; t < data.Length; t++) data[t] = (byte)(t * (42 & t >> 10)); // our sequence, t current character
    ByteBeat r1 = new ByteBeat(data);
    r1.CreateByteBeat();
}
```
