using System.CommandLine;

/// <summary>
/// A list of options entered by the user on the command line.
/// </summary>
/// <param name="script">
/// <returns></returns>
public record CommandLineOptions(string? script)
{
  /// <summary>
  /// Parses the command line args and returns a record with the parsed command line options.
  /// </summary>
  /// <param name="args"></param>
  /// <returns></returns>
  public static CommandLineOptions Create(string[] args)
  {
    var command = new RootCommand("Sample app for a database with users and posts.")
    {
      TreatUnmatchedTokensAsErrors = false
    };

    var scriptOption = new Option<string?>(
      name: "--script",
      description: "Seed the database with mock data.",
      getDefaultValue: () => null
    );

    command.AddOption(scriptOption);
    CommandLineOptions output = null!;
    command.SetHandler(
      (string? script) =>
      {
        output = new CommandLineOptions(script);
      },
      scriptOption
    );

    command.Invoke(args);

    return output;
  }
}
