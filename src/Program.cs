// Replace.exe replaces the specified string with another specified string for each line of input.
// Copyright (C) 2023 Timothy J. Bruce

/*
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using Icod.Helpers;

namespace Icod.Replace {

	public static class Program {

		private const System.Int32 theBufferSize = 16384;


		[System.STAThread]
		public static System.Int32 Main( System.String[] args ) {
			var len = args.Length;
			if ( 10 < len ) {
				PrintUsage();
				return 1;
			}

			var processor = new Icod.Argh.Processor(
				new Icod.Argh.Definition[] {
					new Icod.Argh.Definition( "help", new System.String[] { "-h", "--help", "/help" } ),
					new Icod.Argh.Definition( "copyright", new System.String[] { "-c", "--copyright", "/copyright" } ),
					new Icod.Argh.Definition( "original", new System.String[] { "-o", "--original", "/original" } ),
					new Icod.Argh.Definition( "with", new System.String[] { "-w", "--with", "/with" } ),
					new Icod.Argh.Definition( "input", new System.String[] { "-i", "--input", "/input" } ),
					new Icod.Argh.Definition( "output", new System.String[] { "--output", "/output" } ),
					new Icod.Argh.Definition( "compare", new System.String[] { "-cmp", "--compare", "/compare" } ),
					new Icod.Argh.Definition( "trim", new System.String[] { "-t", "--trim", "/trim" } ),
				},
				System.StringComparer.OrdinalIgnoreCase
			);
			processor.Parse( args );

			if ( processor.Contains( "help" ) ) {
				PrintUsage();
				return 1;
			} else if ( processor.Contains( "copyright" ) ) {
				PrintCopyright();
				return 1;
			}

			var trim = processor.Contains( "trim" );
			System.Func<System.String, System.String?> trimmer;
			if ( trim ) {
				trimmer = x => x.TrimToNull();
			} else {
				trimmer = x => x;
			}

			if (
				( !processor.TryGetValue( "original", false, out var original ) )
				|| System.String.IsNullOrEmpty( original )
			) {
				PrintUsage();
				return 1;
			}

			if ( !processor.TryGetValue( "with", false, out var with ) ) {
				PrintUsage();
				return 1;
			}

			if ( !processor.TryGetValue( "compare", true, out var compareStr ) ) {
				compareStr = "CurrentCulture";
			}
			if (
				System.String.IsNullOrEmpty( compareStr )
				|| ( !System.Enum.TryParse( typeof( System.StringComparison ), compareStr, out var compare ) )
			) {
				PrintUsage();
				return 1;
			}

			System.Func<System.String?, System.Collections.Generic.IEnumerable<System.String>> reader;
			if ( processor.TryGetValue( "input", true, out var inputPathName ) ) {
				if ( System.String.IsNullOrEmpty( inputPathName ) ) {
					PrintUsage();
					return 1;
				} else {
					reader = a => a!.ReadLine();
				}
			} else {
				reader = a => System.Console.In.ReadLine( System.Environment.NewLine );
			}

			System.Action<System.String?, System.Collections.Generic.IEnumerable<System.String>> writer;
			if ( processor.TryGetValue( "output", true, out var outputPathName ) ) {
				if ( System.String.IsNullOrEmpty( outputPathName ) ) {
					PrintUsage();
					return 1;
				} else {
					writer = ( a, b ) => a!.WriteLine( b );
				}
			} else {
				writer = ( a, b ) => System.Console.Out.WriteLine( lineEnding: System.Environment.NewLine, data: b );
			}

			var c = (System.StringComparison)compare;
			writer(
				outputPathName,
				reader( inputPathName ).Select(
					x => x.Replace( original, with, c )
				)
			);
			return 0;
		}

		private static void PrintUsage() {
			System.Console.Error.WriteLine( "No, no, no! Use it like this, Einstein:" );
			System.Console.Error.WriteLine( "Replace.exe (-h | --help | /help)" );
			System.Console.Error.WriteLine( "Replace.exe (-c | --copyright | /copyright)" );
			System.Console.Error.WriteLine( "Replace.exe (-o | --original | /original) theOriginal (-w | --with | /with) theReplacement [(-cmd | --compare | /compere) (CurrentCulture | CurrentCultureIgnoreCase | InvariantCulture | InvariantCultureIgnoreCase | Ordinal | OrdinalIgnoreCase)] [(-i | --input | /input) inputFilePathName] [(--output | /output) outputFilePathName] [(-t | --trim | /trim)]" );
			System.Console.Error.WriteLine( "Replace.exe replaces the specified string with another specified string for each line of input." );
			System.Console.Error.WriteLine( "The default value for the compare switch is CurrentCulture." );
			System.Console.Error.WriteLine( "inputFilePathName and outputFilePathName may be relative or absolute paths." );
			System.Console.Error.WriteLine( "If inputFilePathName is omitted then input is read from StdIn." );
			System.Console.Error.WriteLine( "If outputFilePathName is omitted then output is written to StdOut." );
			System.Console.Error.WriteLine( "If trim switch is specified, then input lines are trimmed of all surrounding whitespace and empty lines are ignored." );
		}
		private static void PrintCopyright() {
			var copy = new System.String[] {
				"Replace.exe replaces the specified string with another specified string for each line of input.",
				"Copyright (C) 2023 Timothy J. Bruce",
				"",
				"This program is free software: you can redistribute it and / or modify",
				"it under the terms of the GNU General Public License as published by",
				"the Free Software Foundation, either version 3 of the License, or",
				"( at your option ) any later version.",
				"",
				"This program is distributed in the hope that it will be useful,",
				"but WITHOUT ANY WARRANTY; without even the implied warranty of",
				"MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the",
				"GNU General Public License for more details.",
				"",
				"You should have received a copy of the GNU General Public License",
				"along with this program.If not, see <https://www.gnu.org/licenses/>."
			};
			foreach ( var line in copy ) {
				System.Console.WriteLine( line );
			}
		}

	}

}