# nan2tetris HACK Assembler

This is a .NET C# implementation of the nand2tetris Hack Plaform Assembler.
It takes `.asm` (Hack assembly code) file as an input and turns it into a 
`.hack` (Hack machine code).

## Projects

The solution consists of 3 projects:

- HackAssembler.Lib - all services and models that power the solution
- HackAssembler.CLI - an actual console application that makes use 
  of HackAssembler.Lib to do the assembly->machine code translation job
- HackAssembler.Lib.Tests - a small suite of tests of some of the services
  of HackAssembler.Lib project

The code is written in an object-oriented way, so various responsibilities
of the solution are enclosed in their own classes.

## Usage

In order to use it, you need to run the `HackAssembler.CLI` project, like this:

```sh
dotnet run -i path/to/input.asm -o path/to/output/to/be/created.hack
```
