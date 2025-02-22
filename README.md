# Icod.Replace
Replace.exe replaces a specified string with another for every line of input.

## Usage
`Replace.exe (-h | --help | /help)`
Displays this text.

`Replace.exe (-c | --copyright | /copyright)`
Displays copyright and licensing information.

`Replace.exe (-o | --original | /original) theOriginal (-w | --with | /with) theReplacement [(-cmd | --compare | /compere) (CurrentCulture | CurrentCultureIgnoreCase | InvariantCulture | InvariantCultureIgnoreCase | Ordinal | OrdinalIgnoreCase)] [(-i | --input | /input) inputFilePathName] [(--output | /output) outputFilePathName] [(-t | --trim | /trim)]`
Replace.exe replaces the specified string with another specified string for each line of input.
The default value for the compare switch is CurrentCulture.
inputFilePathName and outputFilePathName may be relative or absolute paths.
If inputFilePathName is omitted then input is read from StdIn.
If outputFilePathName is omitted then output is written to StdOut.
If trim switch is specified, then input lines are trimmed of all surrounding whitespace and empty lines are ignored.

Replace.exe does not use Regular Expressions.

## Copyright and Licensing
Replace.exe replaces a specified string with another for every line of input.
Copyright (C) 2025 Timothy J. Bruce

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published 
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.

